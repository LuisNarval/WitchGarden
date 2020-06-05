using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSystem : MonoBehaviour
{
    
    bool isDigging = false;

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            if (isDigging)
                dig();
        }
    }

    public void StartDigging()
    {
        isDigging = true;
        CursorSystem.SetCursor(CURSORS.SHOVEL);
    }

    public void StopDigging()
    {
        isDigging = false;
        CursorSystem.ReleaseCursor();
        ActionSystem.setNoneAction();
    }


    void dig()
    {
        FarmSystem.makeAHole();
        //StopDigging();
    }


}