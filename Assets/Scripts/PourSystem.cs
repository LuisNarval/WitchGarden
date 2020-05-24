using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourSystem : MonoBehaviour
{
    [Header("REFERENCE TO SCENE")]
    public Transform Regadera;

    bool isPouring = false;
    Vector3 mPosition;
    Vector2 minPourArea;
    Vector2 maxPourArea;

    // Start is called before the first frame update
    void Start()
    {
        Regadera.GetComponent<SpriteRenderer>().enabled = false;
        minPourArea = CoordenateSystem.minArea;
        maxPourArea = CoordenateSystem.maxArea;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPouring)
            showPourArea();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isPouring)
                pour();
        }
    }


    public void StartPouring()
    {
        isPouring = true;
        Regadera.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void StopPouring()
    {
        isPouring = false;
        Regadera.GetComponent<SpriteRenderer>().enabled = false;
        Cursor.visible = true;
    }


    void showPourArea()
    {
        mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mPosition.x > minPourArea.x && mPosition.x < maxPourArea.x && mPosition.y > minPourArea.y && mPosition.y < maxPourArea.y)
        {
            if (Cursor.visible)
                Cursor.visible = false;
            Regadera.position = new Vector3(mPosition.x, mPosition.y, 0);
        }
        else
        {
            if (!Cursor.visible)
                Cursor.visible = true;
            Regadera.position = new Vector3(1000, 1000, 0);
        }
    }



    void pour()
    {
        StopPouring();
        Regadera.GetComponent<Regadera>().Pour();
    }



}