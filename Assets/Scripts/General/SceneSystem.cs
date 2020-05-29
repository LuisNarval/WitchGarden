using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSystem : MonoBehaviour
{
    [Header("CONFIG")]
    public string nextSceneName;
    public float enterSpeed;
    public float exitSpeed;

    [Header("REFERENCE TO SCENE")]
    public Image imgCurtain;

    [Header("REFERENCE TO PROJECT")]
    public Sprite enterSprite;
    public Sprite exitSprite;
    public Animator animCurtain;
    public Canvas curtainCanvas;

    AsyncOperation aOp;

    // Start is called before the first frame update
    void Start()
    {
        enterScene();
    }

    void enterScene()
    {
        imgCurtain.sprite = enterSprite;
        animCurtain.SetTrigger("enter");
        animCurtain.speed=enterSpeed;
        Invoke("disableCurtain", 4.0f);
    }

    void disableCurtain()
    {
        animCurtain.enabled = false;
        curtainCanvas.enabled = false;
    }

    public void nextScene()
    {
        aOp = SceneManager.LoadSceneAsync(nextSceneName);
        aOp.allowSceneActivation = false;
        closeCurtain();
    }



    public void reloadScene(){
        aOp = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        aOp.allowSceneActivation = false;
        closeCurtain();
    }


    void closeCurtain()
    {
        imgCurtain.sprite = exitSprite;
        animCurtain.enabled = true;
        curtainCanvas.enabled = true;
        animCurtain.SetTrigger("exit");
        animCurtain.speed = exitSpeed;
        Invoke("goToNextScene", 4.0f);
    }

    void goToNextScene()
    {
        aOp.allowSceneActivation = true;
    }

}
