using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour
{
    [Header("REFERENCE")]
    public Manager manager;
    public Text txt_Timer;

    public static int time;

    // Start is called before the first frame update
    void Start()
    {
        time = manager.Seconds;
    }

    public void StartTime()
    {
        StartCoroutine(coroutineCountDown());
    }

    IEnumerator coroutineCountDown()
    {
        time = manager.Seconds;
        int segundero;
        int minutero;

        while (time > 0){
            minutero = time / 60;
            segundero = time - (minutero * 60);

            if (minutero >= 10){
                if (segundero >= 10)
                    txt_Timer.text =  minutero + ":" + segundero;
                else                
                    txt_Timer.text =  minutero + ":0" + segundero;
            }else{
                if (segundero >= 10)
                    txt_Timer.text = "0" + minutero + ":" + segundero;
                else
                    txt_Timer.text = "0" + minutero + ":0" + segundero;
            }

            time -= 1;

            yield return new WaitForSeconds(1.0f);
        }

        txt_Timer.text = "00:00";


        TimesUp();
    }

    void TimesUp()
    {
        manager.timesUp();
    }


    public void StopTime()
    {
        StopAllCoroutines();
    }


}