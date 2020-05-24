using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSystem : MonoBehaviour
{
    bool isPlanting = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            if (isPlanting)
                plant();
        }
    }

    public void StartPlanting(){
        isPlanting = true;
        CursorSystem.SetCursor(CURSORS.SEMILLAS);
    }

    public void StopPlanting(){
        isPlanting = false;
        CursorSystem.ReleaseCursor();
    }

    void plant() {
        StopPlanting();
    }

}