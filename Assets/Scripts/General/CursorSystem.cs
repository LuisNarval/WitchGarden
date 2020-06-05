using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum CURSORS { HAND, RAKE, SHOVEL, SEEDS, SPRINKLER, SCISSORS, PLANT }
public class CursorSystem : MonoBehaviour
{

    [Header("REFERENCE TO SCENE")]
    public GameObject cursor;
    public Image seedImage;

    [Header("REFERENCE TO PROYECT")]
    public Sprite[] allCursors;

    float screenWidth;
    float screenHeight;

    static Image cursorImage;
    static Sprite[] cursorArray;
    static RectTransform cursorTrans;

    static CURSORS currentCursor;

    static bool specialCursor;
    
    public static SPECIES currentPlantSpecies;
    public static Image staticSeedImage;

    // Start is called before the first frame update
    void Start()
    {
        specialCursor = false;
        staticSeedImage = seedImage;
        staticSeedImage.enabled = false;

        screenWidth = Screen.width;
        screenHeight = Screen.height;

        cursorTrans = cursor.GetComponent<RectTransform>();
        cursorImage = cursor.GetComponent<Image>();
        cursorArray = allCursors;

        Cursor.visible = false;
        ReleaseCursor();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        followMouse();

        if (specialCursor)
            verifyArea();
    }

    void followMouse()
    {
        Vector3 viewPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        cursorTrans.position = new Vector3(viewPosition.x* Screen.width, viewPosition.y* Screen.height, 0); 
    }

    static bool inAreaState;

    void verifyArea()
    {
        if (FarmSystem.isInFarmArea()!= inAreaState){
            inAreaState = FarmSystem.isInFarmArea();

            if (inAreaState)
                changeImage(currentCursor);
            else
                changeImage(CURSORS.HAND);


            if(FarmSystem.isInDeliverArea()&&ActionSystem.currentAction==ACTION.PLANTINHAND)
                changeImage(CURSORS.PLANT);

        }
    }


    public static void SetCursor(CURSORS cursorType)
    {
        changeImage(cursorType);
        specialCursor = true;
        currentCursor = cursorType;

        inAreaState = FarmSystem.isInFarmArea();
    }

    public static void SetPlantCursor(Kind plantKind)
    {
        currentPlantSpecies = plantKind.species;
        cursorArray[(int)CURSORS.PLANT] = plantKind.plantCursor;
        SetCursor(CURSORS.PLANT);
    }




    public static void ReleaseCursor()
    {
        changeImage((int)CURSORS.HAND);
        specialCursor = false;
        currentCursor = CURSORS.HAND;
    }


    static void changeImage(CURSORS cursorType){
        cursorImage.sprite = cursorArray[(int)cursorType];
        cursorTrans.pivot = cursorImage.sprite.pivot / 128;

        if (cursorType == CURSORS.SEEDS)
            staticSeedImage.enabled = true;
        else
            staticSeedImage.enabled = false;
    }

}