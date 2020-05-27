using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [Header("CONFIG")]
    public float minRangeToGrowUp;
    public float maxRangeToGrowUp;

    [Header("REFERENCE TO SCENE")]
    public SpriteRenderer spriteRenderer;
    public Material normalMaterial;
    public Material rightMaterial;
    public Material wrongMaterial;
    public Canvas canvas;
    public Image fillBar;

    [Header("REFERENCE TO PROYECT")]
    public Sprite[] spriteArray;

    [Header("QUERY")]
    public float timeToGrowUp;
    public float time;
    public Vector2 gridPos;
    
   
    void Start(){
        timeToGrowUp = Random.Range(minRangeToGrowUp, maxRangeToGrowUp);
        setNormalColor();
        canvas.enabled = false;
    }


    bool isOutlined;

    public void unSetOutline(){
        if (isOutlined)
            setNormalColor();
    }

    public void setOutline()
    {
        switch (ActionSystem.currentAction)
        {
            case ACTION.NONE:
                break;

            case ACTION.DIGHOLE:
                if (getState() == STATE.PLOW)   setRightColor();
                else                            setWrongColor();
            break;

            case ACTION.PLANTSEED:
                if (getState() == STATE.HOLE)   setRightColor();
                else                            setWrongColor();
            break;
            
            case ACTION.POURWATER:
                if (getState() == STATE.SEED)   setRightColor();
                else                            setWrongColor();
            break;

            case ACTION.CUTPLANT:
                if (getState() == STATE.MATURE) setRightColor();
                else setWrongColor();
            break;
        }
    }

    public void setRightColor(){
        spriteRenderer.material = rightMaterial;
        isOutlined = true;
    }
    public void setWrongColor(){
        spriteRenderer.material = wrongMaterial;
        isOutlined = true;
    }
    public void setNormalColor(){
        spriteRenderer.material = normalMaterial;
        isOutlined = false;
    }

    public void setPosicion(float x, float y){
        gridPos = new Vector2(x,y);
    }



    public void holeInLand()
    {
        updateState(STATE.HOLE);
    }

    public void seedsInLand()
    {
        updateState(STATE.SEED);
    }

    public void pourWater()
    {
        StartCoroutine(coroutine_GrowUp());
    }



    IEnumerator coroutine_GrowUp()
    {
        time = 0;
        canvas.enabled = true;

        FarmSystem.SetState(gridPos, STATE.FASE1);

        while (time<timeToGrowUp/3){
            time += Time.deltaTime;
            fillBar.fillAmount = time / timeToGrowUp;
            yield return new WaitForEndOfFrame();
        }

        updateState(STATE.FASE1);

        while (time < (timeToGrowUp / 3) *2 ){
            time += Time.deltaTime;
            fillBar.fillAmount = time / timeToGrowUp;
            yield return new WaitForEndOfFrame();
        }

        updateState(STATE.FASE2);

        while (time < timeToGrowUp){
            time += Time.deltaTime;
            fillBar.fillAmount = time / timeToGrowUp;
            yield return new WaitForEndOfFrame();
        }

        updateState(STATE.MATURE);

        canvas.enabled = false;
    }


    public void cutPlant()
    {
        updateState(STATE.PLOW);
    }





    void updateState(STATE state)
    {
        FarmSystem.SetState(gridPos, state);
        spriteRenderer.sprite = spriteArray[(int)FarmSystem.GetState(gridPos)];
    }

    STATE getState()
    {
        return FarmSystem.GetState(gridPos);
    }
}