using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSystem : MonoBehaviour
{
    [Header("REFERENCE TO SCENE")]
    public Transform Semillas;

    bool isPlanting = false;
    Vector3 mPosition;
    Vector2 minPlantArea;
    Vector2 maxPlantArea;

    // Start is called before the first frame update
    void Start()
    {
        Semillas.GetComponent<SpriteRenderer>().enabled = false;
        minPlantArea = CoordenateSystem.minArea;
        maxPlantArea = CoordenateSystem.maxArea;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlanting)
            showPlantArea();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isPlanting)
                plant();
        }
    }


    public void StartPlanting(){
        isPlanting = true;
        Semillas.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void StopPlanting(){
        isPlanting = false;
        Semillas.GetComponent<SpriteRenderer>().enabled = false;
        Cursor.visible = true;
    }


    void showPlantArea(){
        mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mPosition.x > minPlantArea.x && mPosition.x < maxPlantArea.x && mPosition.y > minPlantArea.y && mPosition.y < maxPlantArea.y){
            if (Cursor.visible)
                Cursor.visible = false;
            Semillas.position = new Vector3(mPosition.x, mPosition.y, 0);
        }
        else{
            if (!Cursor.visible)
                Cursor.visible = true;
            Semillas.position = new Vector3(1000, 1000, 0);
        }
    }



    void plant()
    {
        StopPlanting();
        Semillas.GetComponent<Semillas>().PlantInLand();
    }



}