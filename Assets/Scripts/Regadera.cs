using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regadera : MonoBehaviour
{

    public bool canPour;
    Plant currentPlant;


    public void Pour()
    {

        if (canPour)
            currentPlant.PourWater();
         
        if (currentPlant != null){
            currentPlant.setNormalColor();
            currentPlant = null;
        }

        canPour = false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plant")){
            currentPlant = collision.GetComponent<Plant>();

            if (currentPlant.state == Plant.STATE.SEED){
                currentPlant.setRightColor();
                canPour = true;
            }
            else
                currentPlant.setWrongColor();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Plant exitPlant = null;

        if (collision.CompareTag("Plant")){
            exitPlant = collision.GetComponent<Plant>();
            exitPlant.setNormalColor();
        }

        if (exitPlant.gameObject == currentPlant.gameObject){
            canPour = false;
        }

    }
}
