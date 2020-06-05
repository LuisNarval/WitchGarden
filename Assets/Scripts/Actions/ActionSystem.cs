using UnityEngine;

public enum ACTION {NONE, PLOW, DIGHOLE, PLANTSEED, POURWATER, CUTPLANT, PLANTINHAND}
public class ActionSystem : MonoBehaviour
{

    [Header("REFERENCE")]
    public PlowSystem plowSystem;
    public DigSystem digSystem;
    public PlantSystem plantSystem;
    public PourSystem pourSystem;
    public CutSystem cutSystem;

    [Header("QUERY")]
    public static ACTION currentAction;

    
    public void PlowAction()
    {
        stopCurrentAction();
        plowSystem.StartPlowing();
        currentAction = ACTION.PLOW;
    }

    public void DigAction()
    {
        stopCurrentAction();
        digSystem.StartDigging();
        currentAction = ACTION.DIGHOLE;
    }

    public void PlantAction(int seedType)
    {
        stopCurrentAction();
        plantSystem.StartPlanting((SPECIES)seedType);
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
    }

    void stopCurrentAction()
    {
        switch (currentAction){
            case ACTION.PLOW: plowSystem.StopPlowing();         break;
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