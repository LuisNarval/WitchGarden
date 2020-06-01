using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Manager : MonoBehaviour
{
    [Header("CONFIG")]
    public int Seconds;
    public int Goal;
    public bool gameEndIfIsGoalAchived;

    [Header("REFERENCE")]
    public SceneSystem sceneSystem;
    public OrderSystem orderSystem;
    public TimeSystem timeSystem;
    public CoinSystem coinSystem;
    public ResultSystem resultSystem;
    public DialogueSystem dialogueSystem;

    public Canvas dialogueCanvas;
    public Canvas prepareCanvas;
    public Animator animPrepare;
    public Animator animBackground;
    public TextMeshProUGUI txtPrepare;

    bool isGameOver;
    bool isLevelPassed;

    private void Start()
    {
        isGameOver = false;
        isLevelPassed = false;
        animPrepare.enabled = false;
        prepareCanvas.enabled = false;

        StartCoroutine(coroutineStartDialogue());
    }


    IEnumerator coroutineStartDialogue()
    {
        dialogueCanvas.enabled = true;
        yield return new WaitForSeconds(1.5f /sceneSystem.enterSpeed);
        dialogueCanvas.enabled = true;
        animBackground.SetTrigger("enter");
        dialogueSystem.readDialogue(0);
    }

   
    public void dialogueFinished()
    {
        if (!isGameOver)
            StartCoroutine(coroutinePrepare());
        else
            showResults();
    }



    IEnumerator coroutinePrepare()
    {
        yield return new WaitForSeconds(1.0f);
        animBackground.SetTrigger("exit");

        prepareCanvas.enabled = true;
        animPrepare.enabled = true;

        txtPrepare.text = "3";
        animPrepare.SetTrigger("bullet");
        yield return new WaitForSeconds(1.0f);
        txtPrepare.text = "2";
        animPrepare.SetTrigger("bullet");
        yield return new WaitForSeconds(1.0f);
        txtPrepare.text = "1";
        animPrepare.SetTrigger("bullet");
        yield return new WaitForSeconds(1.0f);
        txtPrepare.text = "COMIENZA !";
        animPrepare.SetTrigger("bullet");
        yield return new WaitForSeconds(0.5f);
        animPrepare.speed = 0;
        yield return new WaitForSeconds(0.5f);
        animPrepare.speed = 1;
        yield return new WaitForSeconds(0.5f);

        animPrepare.enabled = false;
        prepareCanvas.enabled = false;

        begginGame();
    }
    

    public void begginGame()
    {
        dialogueCanvas.enabled = false;
        orderSystem.sendOrders();
        timeSystem.StartTime();
    }



    public void timesUp()
    {
        StopAllCoroutines();
        orderSystem.storeAllOrders();
        StartCoroutine(coroutineshowEndResult("TIEMPO TERMINADO!"));
    }


    public void goalAchived()
    {
        isLevelPassed = true;
        
        if (gameEndIfIsGoalAchived){
            orderSystem.storeAllOrders();
            StopAllCoroutines();
            StartCoroutine(coroutineshowEndResult("META LOGRADA!"));
        }

        Debug.Log("Goal Achived, Level Passed : " + isLevelPassed);

    }




    IEnumerator coroutineshowEndResult(string message)
    {
        timeSystem.StopTime();
        CursorSystem.ReleaseCursor();
        ActionSystem.setNoneAction();

        isGameOver = true;
        dialogueCanvas.enabled = true;
        animBackground.SetTrigger("enter");

        yield return new WaitForSeconds(1.0f);

        prepareCanvas.enabled = true;
        animPrepare.enabled = true;

        txtPrepare.text = message;
        animPrepare.SetTrigger("bullet");
        yield return new WaitForSeconds(0.5f);
        animPrepare.speed = 0;
        yield return new WaitForSeconds(3.0f);
        animPrepare.speed = 1;
        yield return new WaitForSeconds(0.5f);

        animPrepare.enabled = false;
        prepareCanvas.enabled = false;

        if(isLevelPassed)
            dialogueSystem.readDialogue(1);
        else
            dialogueSystem.readDialogue(2);

    }


    void showResults(){

        if (isLevelPassed){
            if (gameEndIfIsGoalAchived){
                resultSystem.victoryResult(timeSystem.txt_Timer.text, "TIEMPO");
            }else{
                resultSystem.victoryResult(coinSystem.currentCoins.ToString(), "PUNTOS");
            }
        }
        else{
            resultSystem.defeatResult();
        }

    }


}