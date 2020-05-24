using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourSystem : MonoBehaviour
{
    bool isPouring = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            if (isPouring)
                pour();
        }
    }

    public void StartPouring()
    {
        isPouring = true;
        CursorSystem.SetCursor(CURSORS.REGADERA);
    }

    public void StopPouring()
    {
        isPouring = false;
        CursorSystem.ReleaseCursor();
    }

    void pour()
    {
        StopPouring();
    }

}