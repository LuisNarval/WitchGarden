using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semillas : MonoBehaviour
{

    public bool canPlant;
    Plant currentPlant;

    public void PlantInLand()
    {
        if (canPlant)
            currentPlant.PlantInLand();

        if (currentPlant != null){
            currentPlant.setNormalColor();
            currentPlant = null;
        }

        canPlant = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plant"))
        {
            currentPlant = collision.GetComponent<Plant>();

            if (currentPlant.state == Plant.STATE.HOLE){
                currentPlant.setRightColor();
                canPlant = true;
            }
            else
                currentPlant.setWrongColor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        Plant exitPlant = null;

        if (collision.CompareTag("Plant")){
            exitPlant = collision.GetComponent<Plant>();
            exitPlant.setNormalColor();
        }

        if(exitPlant.gameObject == currentPlant.gameObject){
            canPlant = false;
        }

    }

}