using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{

    public enum STATE{HOLE, SEED, FASE1, FASE2, MATURE}

    [Header("CONFIG")]
    public float minRangeToGrowUp;
    public float maxRangeToGrowUp;


    [Header("REFERENCE")]
    public Material normalMaterial;
    public Material rightMaterial;
    public Material wrongMaterial;

    public Canvas canvas;
    public Image fillBar;

    public Sprite[] spriteArray;

    [Header("QUERY")]
    public STATE state;
    public float timeToGrowUp;
    public float time;
    
        SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        state = STATE.HOLE;
        setNormalColor();
        
        timeToGrowUp = Random.Range(minRangeToGrowUp, maxRangeToGrowUp);
    }

    public void setRightColor()
    {
        spriteRenderer.material = rightMaterial;
    }

    public void setWrongColor()
    {
        spriteRenderer.material = wrongMaterial;
    }

    public void setNormalColor()
    {
        spriteRenderer.material = normalMaterial;
    }


    public void PlantInLand()
    {
        state = STATE.SEED;
        spriteRenderer.sprite = spriteArray[(int)state];
    }

    public void PourWater()
    {

        Debug.Log("POUR WATER Pre Coroutine");

        StartCoroutine(coroutine_GrowUp());
    }


    IEnumerator coroutine_GrowUp()
    {
        time = 0;
        canvas.enabled = true;

        while (time<timeToGrowUp/3){
            time += Time.deltaTime;
            fillBar.fillAmount = time / timeToGrowUp;
            yield return new WaitForEndOfFrame();
        }
        state = STATE.FASE1;
        spriteRenderer.sprite = spriteArray[(int)state];


        while (time < (timeToGrowUp / 3) *2 ){
            time += Time.deltaTime;
            fillBar.fillAmount = time / timeToGrowUp;
            yield return new WaitForEndOfFrame();
        }
        state = STATE.FASE2;
        spriteRenderer.sprite = spriteArray[(int)state];


        while (time < timeToGrowUp){
            time += Time.deltaTime;
            fillBar.fillAmount = time / timeToGrowUp;
            yield return new WaitForEndOfFrame();
        }

        state = STATE.MATURE;
        spriteRenderer.sprite = spriteArray[(int)state];

        canvas.enabled = false;
    }

}