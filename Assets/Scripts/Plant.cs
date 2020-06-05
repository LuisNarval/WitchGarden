using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [Header("CONFIG")]
    public float minRangeToGrowUp;
    public float maxRangeToGrowUp;
    public Kind kind;

    [Header("REFERENCE TO ITSELF")]
    public GameObject Sign;
    public SpriteRenderer signIcon;

    [Header("REFERENCE TO SCENE")]
    public SpriteRenderer spriteRenderer;
    public Material normalMaterial;
    public Material rightMaterial;
    public Material wrongMaterial;
    public Canvas canvas;
    public Image fillBar;

    [Header("QUERY")]
    public float timeToGrowUp;
    public float time;
    public Vector2 gridPos;
    public Sprite[] spriteArray;

    bool isOutlined;

    void Start(){
        timeToGrowUp = Random.Range(minRangeToGrowUp, maxRangeToGrowUp);
        setNormalColor();
        canvas.enabled = false;
        updateKind(kind);
    }


    void updateKind(Kind newkKind)
    {
        kind = newkKind;
        spriteArray = kind.sprites;
        signIcon.sprite = kind.plantCursor;
    }


    
    
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
        AudioSystem.playDig();
        updateState(STATE.HOLE);
    }

    public void seedsInLand(Kind newKind)
    {
        AudioSystem.playSeeds();
        updateKind(newKind);
        updateState(STATE.SEED);
    }

    public void pourWater()
    {
        AudioSystem.playPour();
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

        AudioSystem.playGrow();
        canvas.enabled = false;
    }


    public void cutPlant()
    {
        AudioSystem.playCut();
        updateState(STATE.PLOW);
    }





    void updateState(STATE state)
    {
        FarmSystem.SetState(gridPos, state);
        spriteRenderer.sprite = spriteArray[(int)FarmSystem.GetState(gridPos)];
        
        if (state == STATE.PLOW || state == STATE.HOLE || state == STATE.MATURE)
            Sign.SetActive(false);
        if (state == STATE.SEED)
            Sign.SetActive(true);
    }

    STATE getState()
    {
        return FarmSystem.GetState(gridPos);
    }
}