using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CoinSystem : MonoBehaviour
{
    [Header("REFERENCE")]
    public Manager manager;

    public Transform arrowTrans;
    public Image fillImage;
    public TextMeshProUGUI txtCoinCount;

    [Header("QUERY")]
    public int currentCoins;
    public int coinsGoal;
    public bool goalFinished;

    void Start()
    {
        goalFinished = false;
        coinsGoal = manager.Goal;
        initCount();   
    }

    void initCount()
    {
        currentCoins = 0;
        updateUI();
    }

    public void addCoins(int add)
    {
        currentCoins += add;
        updateUI();
        AudioSystem.playCoin();

        if (currentCoins >= coinsGoal && !goalFinished){
            goalFinished = true;
            manager.goalAchived();
        }
    }

    void updateUI()
    {
        txtCoinCount.text = currentCoins.ToString();
        fillImage.fillAmount = (float) currentCoins / coinsGoal;
        arrowTrans.localPosition = new Vector3( fillImage.fillAmount * 620.0f-310.0f, arrowTrans.localPosition.y, arrowTrans.localPosition.z);
    }


}