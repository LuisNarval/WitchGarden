using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourSystem : MonoBehaviour
{
    bool isPouring = false;

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            if (isPouring)
                pour();
        }
    }

    public void StartPouring()
    {
        isPouring = true;
        CursorSystem.SetCursor(CURSORS.SPRINKLER);
    }

    public void StopPouring()
    {
        isPouring = false;
        CursorSystem.ReleaseCursor();
        ActionSystem.setNoneAction();
    }

    void pour()
    {
        FarmSystem.pourWater();
        //StopPouring();
    }

}