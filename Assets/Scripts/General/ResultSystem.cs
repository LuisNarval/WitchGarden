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
    public Animator animScore;

    public Canvas resultCanvas;
    public CanvasGroup starsGroup;
    public CanvasGroup scoreGroup;
    public CanvasGroup buttonGroup;

    public TextMeshProUGUI txtState;
    public TextMeshProUGUI txtValue;
    public TextMeshProUGUI txtValueType;

    public TMP_ColorGradient victoryTextColor;
    public TMP_ColorGradient defeatTextColor;

    public Image defeatImage;
    public GameObject nextButton;

    // Start is called before the first frame update
    void Start()
    {
        defeatImage.enabled = false;
        animStars.enabled = false;
        starsGroup.alpha = 0;
        starsGroup.blocksRaycasts = false;

        animScore.enabled = false;
        scoreGroup.alpha = 0;
        scoreGroup.blocksRaycasts = false;

        animResult.enabled = false;
        resultCanvas.enabled = false;

        buttonGroup.blocksRaycasts = false;
        buttonGroup.alpha = 0.0f;
    }

   
    public void victoryResult(string _value, string _valueType)
    {
        txtValue.text = _value;
        txtValueType.text = _valueType;
        StartCoroutine(coroutineVictoryResults());
    }

    public void defeatResult()
    {
        StartCoroutine(coroutineDefeatResults());
    }


    IEnumerator coroutineVictoryResults()
    {
        txtState.text = "VICTORIA";
        txtState.colorGradientPreset = victoryTextColor;

        resultCanvas.enabled = true;
        animResult.enabled = true;
        defeatImage.enabled = false;

        

        yield return new WaitForSeconds(1.25f);
        AudioSystem.Instance.SFX_WinFanfare.Play();
        yield return new WaitForSeconds(1.25f);

        starsGroup.alpha = 1;
        starsGroup.blocksRaycasts = true;
        animStars.enabled = true;

        yield return new WaitForSeconds(1.5f);

        animStars.Play("showStars");

        yield return new WaitForSeconds(3.0f);

        scoreGroup.alpha = 1;
        scoreGroup.blocksRaycasts = true;
        animScore.enabled = true;

        yield return new WaitForSeconds(1.5f);

        while (buttonGroup.alpha < 1){
            buttonGroup.alpha += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        buttonGroup.blocksRaycasts = true;

    }




    IEnumerator coroutineDefeatResults()
    {
        txtState.text = "DERROTA";
        txtState.colorGradientPreset = defeatTextColor;
        nextButton.SetActive (false);

        resultCanvas.enabled = true;
        animResult.enabled = true;
        defeatImage.enabled = true;

        

        yield return new WaitForSeconds(1.25f);
        AudioSystem.Instance.SFX_FailureFanfare.Play();
        yield return new WaitForSeconds(1.25f);

        while (buttonGroup.alpha < 1){
            buttonGroup.alpha += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        buttonGroup.blocksRaycasts = true;
    }



}