using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour
{
    [Header("CONFIG")]
    public int Segundos;
    [Header("REFERENCE")]
    public Text txt_Timer;

    public static int time;

    // Start is called before the first frame update
    void Start()
    {
        time = Segundos;
        StartTime();
    }

    public void StartTime()
    {
        StartCoroutine(coroutineCountDown());
    }

    IEnumerator coroutineCountDown()
    {
        time = Segundos;
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
        Debug.Log("Se termino el tiempo"); 
    }

}