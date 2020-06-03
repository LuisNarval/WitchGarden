using UnityEngine;

public enum ACTION {NONE,DIGHOLE, PLANTSEED, POURWATER, CUTPLANT, PLANTINHAND}
public class ActionSystem : MonoBehaviour
{
    
    [Header("REFERENCE")]
    public DigSystem digSystem;
    public PlantSystem plantSystem;
    public PourSystem pourSystem;
    public CutSystem cutSystem;

    [Header("QUERY")]
    public static ACTION currentAction;

    
    public void DigAction()
    {
        stopCurrentAction();
        digSystem.StartDigging();
        currentAction = ACTION.DIGHOLE;
    }

    public void PlantAction(int seedType)
    {
        stopCurrentAction();
        plantSystem.StartPlanting((PLANTKIND)seedType);
        currentAction = ACTION.PLANTSEED;
    }

    public void PourAction()
    {
        stopCurrentAction();
        pourSystem.StartPouring();
        currentAction = ACTION.POURWATER;
    }


    public void CutAction()
    {
        stopCurrentAction();
        cutSystem.StartCutting();
        currentAction = ACTION.CUTPLANT;

    }

    public void PlantInHand()
    {
        stopCurrentAction();
        currentAction = ACTION.PLANTINHAND;
        CursorSystem.SetCursor(CURSORS.PLANT);
    }

    void stopCurrentAction()
    {
        switch (currentAction){
            case ACTION.DIGHOLE: digSystem.StopDigging();       break;
            case ACTION.PLANTSEED: plantSystem.StopPlanting();  break;
            case ACTION.POURWATER: pourSystem.StopPouring();    break;
            case ACTION.CUTPLANT: cutSystem.StopCutting();      break;
        }
    }

    public static void setNoneAction()
    {
        currentAction = ACTION.NONE;
    }

}