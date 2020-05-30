using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingSystem : MonoBehaviour
{
    [Header("REFERENCE")]
    public Image imgFill;
    public TextMeshProUGUI txtNumber;

    AsyncOperation aOP;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(coroutineLoad());
    }

    IEnumerator coroutineLoad()
    {
        aOP = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("NEXTSCENE"));
        aOP.allowSceneActivation = false;

        while (aOP.progress<0.89){
            imgFill.fillAmount = aOP.progress;
            txtNumber.text = ((int)(aOP.progress * 100)).ToString();
            yield return new WaitForEndOfFrame();
        }

        imgFill.fillAmount = 1;
        txtNumber.text = "100";

        yield return new WaitForSeconds(1.0f);

        aOP.allowSceneActivation = true;
    }

}