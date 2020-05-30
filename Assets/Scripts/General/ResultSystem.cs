using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultSystem : MonoBehaviour
{

    [Header("REFERENCE")]
    public Animator animResult;
    public Animator animStars;
    public Animator animDown;

    public Canvas resultCanvas;
    public CanvasGroup starsGroup;
    public CanvasGroup downGroup;

    public TextMeshProUGUI txtState;
    public TextMeshProUGUI txtPoints;


    // Start is called before the first frame update
    void Start()
    {
        animStars.enabled = false;
        starsGroup.alpha = 0;
        starsGroup.blocksRaycasts = false;

        animDown.enabled = false;
        downGroup.alpha = 0;
        downGroup.blocksRaycasts = false;

        animResult.enabled = false;
        resultCanvas.enabled = false;
    }

   
    public void showResults()
    {
        StartCoroutine(coroutineShowResults());
    }


    IEnumerator coroutineShowResults()
    {
        resultCanvas.enabled = true;
        animResult.enabled = true;

        yield return new WaitForSeconds(2.5f);

        starsGroup.alpha = 1;
        starsGroup.blocksRaycasts = true;
        animStars.enabled = true;

        yield return new WaitForSeconds(1.5f);

        animStars.Play("showStars");

        yield return new WaitForSeconds(3.0f);

        downGroup.alpha = 1;
        downGroup.blocksRaycasts = true;
        animDown.enabled = true;
    }





}