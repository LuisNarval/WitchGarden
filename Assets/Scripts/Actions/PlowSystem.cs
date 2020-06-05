using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlowSystem : MonoBehaviour
{
    [Header("CONFIG")]
    public float respawnTime;

    [Header("REFERENCE TO SCENE")]
    public Transform vLine;
    public Transform hLine;
    public GameObject snapCursor;
    public Image rakeFill;
    public Button rakeButton;

    [Header("REFERENCE TO PROYECT")]
    public GameObject Plant;

    Vector3 snapPoint;
    Vector3 mPosition;
    bool isPlowing = false;
    bool isRespawning = false;

    // Start is called before the first frame update
    void Start()
    {
        rakeFill.fillAmount = 0;
        rakeButton.interactable = true;
        snapCursor.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isPlowing)
            showDigArea();

        if (Input.GetKeyDown(KeyCode.Mouse0)){
            if (isPlowing)
                plow();
        }
    }


    public void StartPlowing()
    {
        if (isRespawning){
            StopPlowing();
        }
        else{
            isPlowing = true;
            snapCursor.SetActive(true);
            CursorSystem.SetCursor(CURSORS.RAKE);
        }

        
    }

    public void StopPlowing()
    {
        isPlowing = false;
        snapCursor.SetActive(false);
        CursorSystem.ReleaseCursor();
        ActionSystem.setNoneAction();
    }



    float snap(float x)
    {
        if (x > 0)
            return (int)x + 0.5f;
        else
            return (int)x - 0.5f;
    }



    void showDigArea()
    {
        mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        snapPoint = new Vector3(snap(mPosition.x), snap(mPosition.y), 0);

        if (FarmSystem.isInFarmArea()){
            vLine.position = new Vector3(snapPoint.x, snapPoint.y, 0);
            hLine.position = new Vector3(snapPoint.x, snapPoint.y, 0);
        }else{
            vLine.position = new Vector3(1000, 1000, 0);
            hLine.position = new Vector3(1000, 1000, 0);
        }

    }



    void plow(){
        FarmSystem.plowGarden(snapPoint);
        StopPlowing();
    }


    public void startRespawn()
    {
        isRespawning = true;
        StopAllCoroutines();
        StartCoroutine(coroutine_Respawn());
    }

    IEnumerator coroutine_Respawn(){
        //rakeButton.interactable = false;
        rakeFill.fillAmount = 0.0f;

        float time = 0.0f;
        while (time<respawnTime){
            rakeFill.fillAmount = 1 - time / respawnTime;
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        rakeFill.fillAmount = 0;
        //rakeButton.interactable = true;
        isRespawning = false;
    }



}