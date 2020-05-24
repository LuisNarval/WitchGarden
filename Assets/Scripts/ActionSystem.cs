using UnityEngine;

enum ACTION { DIG, PLANT, POURWATER }
public class ActionSystem : MonoBehaviour
{
    [Header("CONFIG")]
    public Vector2 minActionArea;
    public Vector2 maxActionArea;

    public static Vector2 minArea;
    public static Vector2 maxArea;

    [Header("REFERENCE")]
    public DigSystem digSystem;
    public PlantSystem plantSystem;
    public PourSystem pourSystem;

    [Header("QUERY")]
    ACTION currentAction;

    private void Awake()
    {
        minArea = minActionArea;
        maxArea = maxActionArea;
    }

    public void DigAction()
    {
        stopCurrentAction();
        digSystem.StartDigging();
        currentAction = ACTION.DIG;
    }

    public void PlantAction()
    {
        stopCurrentAction();
        plantSystem.StartPlanting();
        currentAction = ACTION.PLANT;
    }

    public void PourAction()
    {
        stopCurrentAction();
        pourSystem.StartPouring();
        currentAction = ACTION.POURWATER;
    }

    void stopCurrentAction()
    {
        switch (currentAction){
            case ACTION.DIG: digSystem.StopDigging();        break;
            case ACTION.PLANT: plantSystem.StopPlanting();   break;
            case ACTION.POURWATER: pourSystem.StopPouring(); break;
        }
    }

    void stopAllActions()
    {
        digSystem.StopDigging();
        plantSystem.StopPlanting();
        pourSystem.StopPouring();
    }

}