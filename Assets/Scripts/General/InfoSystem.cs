using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoSystem : MonoBehaviour
{
    [Header("CONFIG")]
    public bool showInfo;

    [Header("REFERENCE")]
    public Canvas infoCanvas;
    public Text txt_coor;
    public Text txt_coorState;

    public Text txt_MousePosition;
    public Text txt_ScreenToViewport;
    public Text txt_ScreenToWorld;

    private void Awake()
    {
        if (showInfo)
            infoCanvas.enabled = true;
        else
            infoCanvas.enabled = false;
    }

    private void Update()
    {
        if (showInfo)
            showCoorInUI();       
    }

    void showCoorInUI()
    {
        if (FarmSystem.isInFarmArea()) {
            txt_coor.text = "(" + FarmSystem.coorX + "," + FarmSystem.coorY + ")";
            txt_coorState.text = FarmSystem.GRID[FarmSystem.coorX, FarmSystem.coorY].state.ToString();
        }

        txt_MousePosition.text = Input.mousePosition.ToString();
        txt_ScreenToViewport.text = Camera.main.ScreenToViewportPoint(Input.mousePosition).ToString();
        txt_ScreenToWorld.text = FarmSystem.mPosition.ToString();
    }

}
