using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CoinSystem : MonoBehaviour
{
    [Header("CONFIG")]
    public int CoinsGoal;

    [Header("REFERENCE")]
    public Transform arrowTrans;
    public Image fillImage;
    public TextMeshProUGUI txtCoinCount;

    [Header("QUERY")]
    public static int currentCoins;

    static int staticCG;
    static Transform staticArrowTrans;
    static Image staticFillImage;
    static TextMeshProUGUI staticTxtCoinCount;

    void Start()
    {
        staticCG = CoinsGoal;
        staticArrowTrans = arrowTrans;
        staticFillImage = fillImage;
        staticTxtCoinCount = txtCoinCount;
        initCount();   
    }

    void initCount()
    {
        currentCoins = 0;
        updateUI();
    }
    
    public static void addCoins(int add)
    {
        currentCoins += add;
        Debug.Log(currentCoins.ToString());
        updateUI();
    }

    static void updateUI()
    {
        staticTxtCoinCount.text = currentCoins.ToString();
        staticFillImage.fillAmount = (float) currentCoins / staticCG;
        staticArrowTrans.localPosition = new Vector3( staticFillImage.fillAmount * 620.0f-310.0f, staticArrowTrans.localPosition.y, staticArrowTrans.localPosition.z);
    }

}