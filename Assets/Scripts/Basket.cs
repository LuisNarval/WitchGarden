using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Basket : MonoBehaviour
{
    [Header("CONFIG")]
    public int maxPlantNumber;
    public CoinSystem coinSystem;

    [Header("REFERENCE")]
    public Animator animator;
    public TextMeshProUGUI txtPlantNumber;
    public Image[] plants;
    public Image palomita;
    public Button button;

    int plantNumber;
    int plantActive;

    int currentSlot;
    int currentBasket;


    public void sendBasket(int maxPlant, int slot, int basket)
    {
        maxPlantNumber = maxPlant;
        currentSlot = slot;
        currentBasket = basket;

        initBasket();
        animator.SetTrigger("enter");
    }

    void initBasket()
    {
        plantNumber = (int)Random.Range(1, maxPlantNumber);
        for (int i = 0; i < plants.Length; i++){
            plants[i].enabled = false;
        }
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
            plantNumber--;
            plants[plantActive].enabled = true;
            plantActive++;
            updateUI();
            CursorSystem.SetCursor(CURSORS.MANO);
            ActionSystem.setNoneAction();
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
        Invoke("informOrderSystem", 1f);
    }


    void informOrderSystem() {
        OrderSystem.releaseBasket(currentSlot, currentBasket);
    }


}