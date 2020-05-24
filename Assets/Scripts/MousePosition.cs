using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePosition : MonoBehaviour
{
    [Header("REFERENCE")]
    public Transform redPoint;
    
    public Text txt_MousePosition;
    public Text txt_ScreenToViewport;
    public Text txt_ScreenToWorld;


    [Header("QUERY")]
    public Vector3 mPosition;
    public Vector3 mSV;
    public Vector3 mSW;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mPosition = Input.mousePosition;
        mSV = Camera.main.ScreenToViewportPoint(mPosition);
        mSW = Camera.main.ScreenToWorldPoint(mPosition);

        txt_MousePosition.text = mPosition.ToString();
        txt_ScreenToViewport.text = mSV.ToString();
        txt_ScreenToWorld.text = mSW.ToString();

        redPoint.position = new Vector3(snap(mSW.x), snap(mSW.y), redPoint.position.z );
    }


    float snap(float x)
    {        
        if(x>0)
            return (int)x + 0.5f;
        else
            return (int)x - 0.5f;

    }



}