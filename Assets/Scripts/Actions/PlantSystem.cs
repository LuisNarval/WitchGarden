using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PLANTKIND {BELLADONA,DRAGONMOUTH,LAVANDA,MANDRAGORA,ORKILLER,STRONIUM}
public class PlantSystem : MonoBehaviour
{
    [Header("REFERENCE TO PROYECT")]
    public Kind[] kinds;

    [Header("REFERENCE TO SCENE")]
    public Image[] blackImage;

    [Header("QUERY")]
    public PLANTKIND currentKind;

    bool isPlanting = false;


    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            if (isPlanting)
                plant();
        }
    }

    public void StartPlanting(PLANTKIND newPlantKind){
        isPlanting = true;

        currentKind = newPlantKind;
        updateSeedsUI();
        
        CursorSystem.SetCursor(CURSORS.SEMILLAS);
    }

    public void StopPlanting(){
        isPlanting = false;
        releaseSeedsUI();
        CursorSystem.ReleaseCursor();
        ActionSystem.setNoneAction();
    }

    void plant() {
        FarmSystem.plantSeeds( kinds[(int)currentKind] );
        //StopPlanting();
    }


    void updateSeedsUI()
    {
        for (int i = 0; i < blackImage.Length; i++)
            blackImage[i].enabled = true;

        blackImage[(int)currentKind].enabled = false;
    }

    void releaseSeedsUI()
    {
        for (int i = 0; i < blackImage.Length; i++)
            blackImage[i].enabled = false;
    }


}