using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("Dialogues")]
    [Header("REFERENCE TO SCENE")]
    public Manager manager;
    public Dialogue entryDialogue;
    public Dialogue victoryDialogue;
    public Dialogue defeatDialogue;

    [Header("Animations")]
    public Animator background;
    public Animator chatBox;
    public Animator animWitch;
    public Animator animGirl;
    public Animator animClient;

    [Header("UI")]
    public Canvas DialogueCanvas;
    public Image imgDialogueBox;
    public TextMeshProUGUI txtDialogue;

    [Header("REFERENCE TO PROJECT")]
    public TMP_ColorGradient witchTextColor;
    public TMP_ColorGradient girlTextColor;
    public TMP_ColorGradient clientTextColor;

    public Sprite imgLeftBox;
    public Sprite imgRightBox;

    [Header("QUERY")]
    public int interventions;
    public int currentIntervention;
    public Dialogue currentDialogue;
    public Queue<string> sentences;

    bool canNext;

    bool isWitchInScene;
    bool isGirlInScene;
    bool isClientInScene;


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        DialogueCanvas.enabled = true;
        animWitch.SetBool("isLeft", true);
        animClient.SetBool("isLeft", true);
        animGirl.SetBool("isRight", true);
        isWitchInScene = false;
        isGirlInScene = false;
        isClientInScene = false;
    }

   

    public void readDialogue(int index)
    {
        switch (index){
            case 0:
                currentDialogue = entryDialogue;
                break;
            case 1:
                currentDialogue = victoryDialogue;
                break;
            case 2:
                currentDialogue = defeatDialogue;
                break;
        }

        interventions = currentDialogue.dialogue.Length - 1;
        currentIntervention = 0;
        initSentences();
    }

    






    public void initSentences()
    {
        sentences.Clear();

        foreach (string newSentence in currentDialogue.dialogue[currentIntervention].sentence){
            sentences.Enqueue(newSentence);
        }

        if (currentDialogue.dialogue[currentIntervention].charactherType == CHARACTERTYPE.GIRL)
            StartCoroutine(coroutine_PrepareRight());
        else
            StartCoroutine(coroutine_PrepareLeft());
    }


    IEnumerator coroutine_PrepareLeft()
    {


        if(currentDialogue.dialogue[currentIntervention].charactherType == CHARACTERTYPE.WITCH){

            if (isClientInScene){
                animClient.SetTrigger("exit");
                animClient.speed = 2.0f;
                yield return StartCoroutine(coroutine_chatBoxOff());
                isClientInScene = false;
                animClient.speed = 1.0f;
                animWitch.speed = 2.0f;
            }

            if (isWitchInScene){
                yield return StartCoroutine(coroutine_chatBoxOff());
            }else{

                //if (isGirlInScene)
                    //yield return StartCoroutine(coroutine_chatBoxOff());

                animWitch.SetTrigger("enter");
                yield return new WaitForSeconds(1/animWitch.speed);
                isWitchInScene = true;
            }

            yield return StartCoroutine(coroutine_chatBoxOn(witchTextColor, imgLeftBox));

        }
        else if(currentDialogue.dialogue[currentIntervention].charactherType == CHARACTERTYPE.CLIENT){

            if (isWitchInScene){
                animWitch.SetTrigger("exit");
                animWitch.speed = 2.0f;
                yield return StartCoroutine(coroutine_chatBoxOff());
                isWitchInScene = false;
                animWitch.speed = 1.0f;
                animClient.speed = 2.0f;
            }

            if (isClientInScene){
                yield return StartCoroutine(coroutine_chatBoxOff());
            }else{

                //if(isGirlInScene&&!isWitchInScene)
                    //yield return StartCoroutine(coroutine_chatBoxOff());

                animClient.SetTrigger("enter");
                yield return new WaitForSeconds(1/animClient.speed);
                isClientInScene = true;
            }

            yield return StartCoroutine(coroutine_chatBoxOn(clientTextColor, imgLeftBox));
        }

        canNext = true;
        Next();
    }


    IEnumerator coroutine_PrepareRight(){

        if (isGirlInScene){
            yield return StartCoroutine(coroutine_chatBoxOff());
        }
        else{

            if (isWitchInScene || isClientInScene)
                yield return StartCoroutine(coroutine_chatBoxOff());

            animGirl.SetTrigger("enter");
            yield return new WaitForSeconds(1.5f);
            isGirlInScene = true;
        }

        yield return StartCoroutine(coroutine_chatBoxOn(girlTextColor, imgRightBox));
        canNext = true;
        Next();
    }





    IEnumerator coroutine_chatBoxOn(TMP_ColorGradient gradient, Sprite boxImage){
        txtDialogue.text = "";
        txtDialogue.colorGradientPreset = gradient;
        imgDialogueBox.sprite = boxImage;
        chatBox.SetTrigger("enter");
        yield return new WaitForSeconds(0.5f);
    }

    
    IEnumerator coroutine_chatBoxOff(){
        chatBox.SetTrigger("exit");
        yield return new WaitForSeconds(0.5f);
    }



    public void Next()
    {
        
        if (canNext){

            if (sentences.Count == 0){
                currentIntervention++;
                canNext = false;

                if (currentIntervention <= interventions)
                    initSentences();
                else
                    endDialogue();

                return;
            }
            
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
            
        }
       
    }




    IEnumerator TypeSentence(string sentence)
    {
        txtDialogue.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            txtDialogue.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }


    void endDialogue()
    {
        canNext = false;
        sentences.Clear();
        StartCoroutine(coroutine_chatBoxOff());
        manager.dialogueFinished();
        hideDialogueCharacters();
    }

    public void hideDialogueCharacters(){
        isGirlInScene = false;
        animGirl.SetTrigger("exit");
        animGirl.speed = 1.0f;

        if (isWitchInScene){
            animWitch.SetTrigger("exit");
            animWitch.speed = 1.0f;
            isWitchInScene = false;
        }

        if (isClientInScene){
            animClient.SetTrigger("exit");
            animClient.speed = 1.0f;
            isClientInScene = false;
        }
    }


}