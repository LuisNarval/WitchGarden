using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum STATE {EMPTY, PLOW, HOLE, SEED, FASE1, FASE2, MATURE}
public class FarmSystem : MonoBehaviour
{
    [Header("CONFIG")]
    public Vector2 minActionArea;
    public Vector2 maxActionArea;


    [Header("REFERENCE TO PROYECT")]
    public GameObject Plant;
    public ActionSystem actionSystem;

    static GameObject PlantPrefab;
    static ActionSystem staticActionSystem;
    static int minAreaX;
    static int minAreaY;
    static int maxAreaX;
    static int maxAreaY;

    public static Vector3 mPosition;

    public struct SECTION {public STATE state; public Plant plant;}
    public static SECTION[,] GRID;

    public static int coorX;
    public static int coorY;
    int lastCoorX = 0;
    int lastCoorY = 0;

    private void Awake()
    {
        minAreaX = (int)minActionArea.x;
        minAreaY = (int)minActionArea.y;
        maxAreaX = (int)maxActionArea.x;
        maxAreaY = (int)maxActionArea.y;
        PlantPrefab = Plant;
        staticActionSystem = actionSystem;
    }

    void Start()
    {
        GRID = new SECTION[maxAreaX, maxAreaY];
        for (int x = 0; x < maxAreaX; x++){
            for (int y = 0; y < maxAreaY; y++){
                GRID[x, y].state = STATE.EMPTY;
            }
        }
    }

  
    void Update()
    {
        readMouse();
        if(isInFarmArea())
            checkTileChange();
    }

    void readMouse()
    {
       mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       coorX = (int) mPosition.x;
       coorY = (int) mPosition.y; 
    }

    public static bool isInFarmArea()
    {
        if (mPosition.x > minAreaX && mPosition.x < maxAreaX && mPosition.y > minAreaY && mPosition.y < maxAreaY)
            return true;
        else
            return false;
    }

    public static bool isInDeliverArea(){
        if (mPosition.x > minAreaX && mPosition.x < maxAreaX && mPosition.y > maxAreaY)
            return true;
        else
            return false;
    }



    public static void SetState(Vector2 pos, STATE state){
        GRID[(int)pos.x, (int)pos.y].state = state;
    }
    public static STATE GetState(Vector2 pos){
        return GRID[(int)pos.x, (int)pos.y].state;
    }



    

    void checkTileChange()
    {    
        if (lastCoorX != coorX || lastCoorY != coorY){
            exitTile();

            if (GRID[coorX, coorY].state != STATE.EMPTY)
                GRID[coorX, coorY].plant.setOutline();
        }
    }


    void exitTile(){
        if (GRID[lastCoorX, lastCoorY].state != STATE.EMPTY)
            GRID[lastCoorX, lastCoorY].plant.unSetOutline();

        if (isInFarmArea()){
            lastCoorX = coorX;
            lastCoorY = coorY;
        }
    }








    public static void makeAHole(Vector3 holePosition)
    {
        
        if (isInFarmArea()){

            if(GRID[coorX, coorY].state == STATE.EMPTY){
                GameObject instance = Instantiate(PlantPrefab, holePosition, Quaternion.identity) as GameObject;
                GRID[coorX, coorY].plant = instance.GetComponent<Plant>();
                GRID[coorX, coorY].plant.setPosicion(coorX, coorY);
                GRID[coorX, coorY].plant.holeInLand();
            }else
                GRID[coorX, coorY].plant.unSetOutline();

            if (GRID[coorX, coorY].state == STATE.PLOW){
                GRID[coorX, coorY].plant.holeInLand();
            }

        }
    }

    public static void plantSeeds()
    {
        if(isInFarmArea() && GRID[coorX, coorY].state != STATE.EMPTY)
            GRID[coorX, coorY].plant.unSetOutline();
        
        if (isInFarmArea() && GRID[coorX, coorY].state == STATE.HOLE)
            GRID[coorX, coorY].plant.seedsInLand();
    }


    public static void pourWater()
    {
        if (isInFarmArea()&&GRID[coorX, coorY].state != STATE.EMPTY)
            GRID[coorX, coorY].plant.unSetOutline();

        if (isInFarmArea() && GRID[coorX, coorY].state == STATE.SEED)
            GRID[coorX, coorY].plant.pourWater();
    }




    public static void cutPlant()
    {
        if (isInFarmArea() && GRID[coorX, coorY].state != STATE.EMPTY)
            GRID[coorX, coorY].plant.unSetOutline();

        if (isInFarmArea() && GRID[coorX, coorY].state == STATE.MATURE){
            GRID[coorX, coorY].plant.cutPlant();
            staticActionSystem.PlantInHand();
        }
    }



    public static void deliverPlant()
    {
        if (isInFarmArea() && GRID[coorX, coorY].state != STATE.EMPTY)
            GRID[coorX, coorY].plant.unSetOutline();

        if (isInFarmArea() && GRID[coorX, coorY].state == STATE.MATURE)
            GRID[coorX, coorY].plant.cutPlant();
    }

}