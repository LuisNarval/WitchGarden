using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum CURSORS { MANO, PALA, SEMILLAS, REGADERA, TIJERAS, PLANT }
public class CursorSystem : MonoBehaviour
{

    [Header("REFERENCE TO SCENE")]
    public GameObject cursor;
    
    [Header("REFERENCE TO PROYECT")]
    public Sprite[] allCursors;

    float screenWidth;
    float screenHeight;

    static Image cursorImage;
    static Sprite[] cursorArray;
    static RectTransform cursorTrans;

    static CURSORS currentCursor;

    static bool specialCursor;

    // Start is called before the first frame update
    void Start()
    {
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
        cursorTrans.position = new Vector3(viewPosition.x*screenWidth,viewPosition.y*screenHeight,0); 
    }

    static bool inAreaState;

    void verifyArea()
    {
        if (FarmSystem.isInFarmArea()!= inAreaState){
            inAreaState = FarmSystem.isInFarmArea();

            if (inAreaState)
                changeImage(currentCursor);
            else
                changeImage(CURSORS.MANO);


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

    public static void ReleaseCursor()
    {
        changeImage((int)CURSORS.MANO);
        specialCursor = false;
        currentCursor = CURSORS.MANO;
    }


    static void changeImage(CURSORS cursorType){
        cursorImage.sprite = cursorArray[(int)cursorType];
        cursorTrans.pivot = cursorImage.sprite.pivot / 128;
    }

}