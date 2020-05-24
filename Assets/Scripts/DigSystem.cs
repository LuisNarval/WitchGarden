﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSystem : MonoBehaviour
{
    [Header("REFERENCE TO SCENE")]
    public Transform Pala;
    public Transform vLine;
    public Transform hLine;
    public GameObject snapCursor;
    
    [Header("REFERENCE TO PROYECT")]
    public GameObject Plant;

    Vector3 snapPoint;
    Vector3 mPosition;
    bool isDigging = false;

    Vector2 minDigArea;
    Vector2 maxDigArea;

    // Start is called before the first frame update
    void Start()
    {
        Pala.GetComponent<SpriteRenderer>().enabled = false;
        snapCursor.SetActive(false);
        minDigArea = ActionSystem.minArea;
        maxDigArea = ActionSystem.maxArea;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDigging)
            showDigArea();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isDigging)
                dig();
        }
    }


    public void StartDigging()
    {
        isDigging = true;
        Pala.GetComponent<SpriteRenderer>().enabled = true;
        snapCursor.SetActive(true);
    }

    public void StopDigging()
    {
        isDigging = false;
        Pala.GetComponent<SpriteRenderer>().enabled = false;
        snapCursor.SetActive(false);
        Cursor.visible = true;
    }




    float snap(float x)
    {
        if (x > 0)
            return (int)x + 0.5f;
        else
            return (int)x - 0.5f;
    }



    void showDigArea()
    {
        mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        snapPoint = new Vector3(snap(mPosition.x), snap(mPosition.y), 0);
        
        if (isInArea()){
            Pala.position = new Vector3(mPosition.x, mPosition.y, 0);
            vLine.position = new Vector3(snapPoint.x, snapPoint.y, 0);
            hLine.position = new Vector3(snapPoint.x, snapPoint.y, 0);
        }
        else{
            Pala.position = new Vector3(1000, 1000, 0);
            vLine.position = new Vector3(1000, 1000, 0);
            hLine.position = new Vector3(1000, 1000, 0);
        }
    }

    bool isInArea()
    {
        if (mPosition.x > minDigArea.x && mPosition.x < maxDigArea.x && mPosition.y > minDigArea.y && mPosition.y < maxDigArea.y){
            if (Cursor.visible)
                Cursor.visible = false;
            return true;
        }
            
        else{
            if (!Cursor.visible)
                Cursor.visible = true;
            return false;
        }
            
    }

    void dig()
    {
        if (isInArea())
            Instantiate(Plant, snapPoint, Quaternion.identity);

        StopDigging();
    }


}