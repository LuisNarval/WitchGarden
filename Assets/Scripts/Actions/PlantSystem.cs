using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlantSystem : MonoBehaviour
{
    [Header("REFERENCE TO PROYECT")]
    public Kind[] kinds;

    [Header("REFERENCE TO SCENE")]
    public Image[] blackImage;

    [Header("QUERY")]
    public SPECIES currentSpecies;

    bool isPlanting = false;


    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            if (isPlanting)
                plant();
        }
    }

    public void StartPlanting(SPECIES newSpecies){
        isPlanting = true;

        currentSpecies = newSpecies;
        updateSeedsUI();
        
        CursorSystem.SetCursor(CURSORS.SEMILLAS);
        CursorSystem.staticSeedImage.sprite = kinds[(int)newSpecies].plantCursor;
    }

    public void StopPlanting(){
        isPlanting = false;
        releaseSeedsUI();
        CursorSystem.ReleaseCursor();
        ActionSystem.setNoneAction();
    }

    void plant() {
        FarmSystem.plantSeeds( kinds[(int)currentSpecies] );
        //StopPlanting();
    }


    void updateSeedsUI()
    {
        for (int i = 0; i < blackImage.Length; i++)
            blackImage[i].enabled = true;

        blackImage[(int)currentSpecies].enabled = false;
    }

    void releaseSeedsUI()
    {
        for (int i = 0; i < blackImage.Length; i++)
            blackImage[i].enabled = false;
    }


}