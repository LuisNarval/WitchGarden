using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverSystem : MonoBehaviour
{
    bool isDelivering = false;

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isDelivering)
                deliver();
        }
    }

    public void StartDelivering()
    {
        isDelivering = true;
        CursorSystem.SetCursor(CURSORS.PLANT);
    }

    public void StopDelivering()
    {
        isDelivering = false;
        CursorSystem.ReleaseCursor();
        ActionSystem.setNoneAction();
    }

    void deliver()
    {
        FarmSystem.deliverPlant();
        StopDelivering();
    }







}