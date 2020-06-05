using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum FAMILIAR {CAT, OWL}
public class Basket : MonoBehaviour
{
    [Header("CONFIG")]
    public int maxPlantNumber;
    public CoinSystem coinSystem;
    public FAMILIAR familiar;

    [Header("REFERENCE")]
    public Animator animator;
    public TextMeshProUGUI txtPlantNumber;
    public Image imgFamiliar;
    public Image basket;
    public Image icon;
    public Image[] plants;
    public Image palomita;
    public Button button;

    [Header("REFERENCE TO PROYECT")]
    public Material wrongMaterial;


    int plantNumber;
    int plantActive;

    int currentSlot;
    int currentBasket;
    Kind currentKind;

    public void sendBasket(int maxPlant, int slot, int basket, Kind newkind)
    {
        maxPlantNumber = maxPlant;
        currentSlot = slot;
        currentBasket = basket;
        currentKind = newkind;

        initBasket();
        animator.SetTrigger("enter");
    }

    void initBasket()
    {
        plantNumber = (int)Random.Range(1, maxPlantNumber);
        for (int i = 0; i < plants.Length; i++){
            plants[i].sprite = currentKind.plantCursor;
            plants[i].enabled = false;
        }
        icon.sprite = currentKind.plantCursor;
        palomita.enabled = false;
        txtPlantNumber.enabled = true;
        plantActive = 0;
        button.interactable = true;
        updateUI();
    }


    void updateUI()
    {
        txtPlantNumber.text = plantNumber.ToString();
    }


    public void recievePlant()
    {
        if(ActionSystem.currentAction == ACTION.PLANTINHAND){

            if(CursorSystem.currentPlantSpecies == currentKind.species){
                plantNumber--;
                plants[plantActive].enabled = true;
                plantActive++;
                updateUI();
                CursorSystem.SetCursor(CURSORS.HAND);
                ActionSystem.setNoneAction();
                AudioSystem.playBasket();
            }else{
                StartCoroutine(coroutineWrongPlant());
            }

        }

        if (plantNumber == 0)
            fullBasket();
    }



    public void fullBasket()
    {
        button.interactable = false;
        palomita.enabled = true;
        txtPlantNumber.enabled = false;
        animator.SetTrigger("exit");
        coinSystem.addCoins(plantActive);
        AudioSystem.playCorrect();
        Invoke("informOrderSystem", 1f);
    }


    void informOrderSystem() {
        OrderSystem.releaseBasket(currentSlot, currentBasket);
    }




    IEnumerator coroutineWrongPlant(){
        imgFamiliar.material = wrongMaterial;
        basket.material = wrongMaterial;
        yield return new WaitForSeconds(1.0f);
        imgFamiliar.material = null;
        basket.material = null;
    }


    public void playFamiliarSound()
    {
        switch (familiar){
            case FAMILIAR.CAT:
                AudioSystem.playCat();
                break;
            case FAMILIAR.OWL:
                AudioSystem.playOwl();
                break;
        }
    }


}