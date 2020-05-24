using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoordenateSystem : MonoBehaviour
{
    [Header("CONFIG")]
    public Vector2 minActionArea;
    public Vector2 maxActionArea;
    public bool showInfo;

    [Header("REFERENCE")]
    public Canvas infoCanvas;
    public Text txt_coor;
    public Text txt_coorState;

    public Text txt_MousePosition;
    public Text txt_ScreenToViewport;
    public Text txt_ScreenToWorld;


    static int minAreaX;
    static int minAreaY;
    static int maxAreaX;
    static int maxAreaY;

    static bool[,] coordenates;

    static int coorX;
    static int coorY;

    static Vector3 mPosition;

    private void Awake()
    {
        minAreaX = (int)minActionArea.x;
        minAreaY = (int)minActionArea.y;
        maxAreaX = (int)maxActionArea.x;
        maxAreaY = (int)maxActionArea.y;

        if (showInfo)
            infoCanvas.enabled = true;
        else
            infoCanvas.enabled = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        initCoordenates();
    }

    // Update is called once per frame
    void Update()
    {
        readMouse();
        if(showInfo)
            showCoorInUI();
    }


    void initCoordenates()
    {
        coordenates = new bool[maxAreaX, maxAreaY];
        for (int x = 0; x < maxAreaX; x++) {
            for (int y = 0; y < maxAreaY; y++) {
                coordenates[x, y] = false;
            }
        }
    }


    void readMouse()
    {
       mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       coorX = (int) mPosition.x;
       coorY = (int) mPosition.y; 
    }

    void showCoorInUI()
    {
        if (isInArea()){
            txt_coor.text = "(" + coorX + "," + coorY + ")";
            txt_coorState.text = coordenates[coorX, coorY].ToString();
        }

        txt_MousePosition.text = Input.mousePosition.ToString();
        txt_ScreenToViewport.text = Camera.main.ScreenToViewportPoint(Input.mousePosition).ToString();
        txt_ScreenToWorld.text = mPosition.ToString();
    }

    public static bool isInArea()
    {
        if (mPosition.x > minAreaX && mPosition.x < maxAreaX && mPosition.y > minAreaY && mPosition.y < maxAreaY)
            return true;
        else
            return false;
    }











    public static void setCoordenate()
    {
        if(isInArea())
            coordenates[coorX, coorY] = true;
    }

    public static bool isCoordenateSet()
    {
        if (isInArea())
            return coordenates[coorX, coorY];
        else
            return true;
    }



    


}