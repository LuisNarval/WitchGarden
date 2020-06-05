using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSystem : MonoBehaviour
{
    bool isCutting = false;

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            if (isCutting)
                cut();
        }
    }

    public void StartCutting()
    {
        isCutting = true;
        CursorSystem.SetCursor(CURSORS.SCISSORS);
    }

    public void StopCutting()
    {
        isCutting = false;
        CursorSystem.ReleaseCursor();
        ActionSystem.setNoneAction();
    }

    void cut()
    {
        StopCutting();
        FarmSystem.cutPlant();   
    }

}
