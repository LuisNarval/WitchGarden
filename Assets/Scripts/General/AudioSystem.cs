using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioSystem : MonoBehaviour
{
    [Header("CONFIG")]
    public float fadeInTime;
    public float fadeOutTime;
    public float minDecibels;

    [Header("REFERENCE TO SCENE")]
    public AudioMixer audioMixer;

    [Header("BGM")]
    public AudioSource BGM_mainTheme;
    public AudioSource BGM_dialogueTheme;

    [Header("Gameplay")]
    [Header("SFX")]
    public AudioSource SFX_Plow;
    public AudioSource SFX_Dig;
    public AudioSource SFX_Pour;
    public AudioSource SFX_Seeds;
    public AudioSource SFX_Cut;
    public AudioSource SFX_Grow;
    public AudioSource SFX_Cat;
    public AudioSource SFX_Owl;
    public AudioSource SFX_Basket;

    [Header("HUD")]
    public AudioSource SFX_Bell1;
    public AudioSource SFX_Bell2;
    public AudioSource SFX_Coin;
    public AudioSource SFX_Correct;
    public AudioSource SFX_Wrong;
    public AudioSource SFX_Ring;
    public AudioSource SFX_TimeOut;

    [Header("Screens")]
    public AudioSource SFX_Star1;
    public AudioSource SFX_Star2;
    public AudioSource SFX_Star3;
    public AudioSource SFX_WinFanfare;
    public AudioSource SFX_FailureFanfare;

    public static AudioSystem Instance {get; private set;}

    // Start is called before the first frame update
    private void Awake(){
        if(Instance != null){
            Instance.StopAllCoroutines();
            Destroy(Instance);
        }

        Instance = this;
        Instance.StopAllCoroutines();
    }


    public static void playPlow(){
        Instance.SFX_Plow.Play();
    }

    public static void playDig(){
        Instance.SFX_Dig.Play();
    }

    public static void playSeeds(){
        Instance.SFX_Seeds.Play();
    }

    public static void playPour(){
        Instance.SFX_Pour.Play();
    }

    public static void playCut(){
        Instance.SFX_Cut.Play();
    }


    public static void playCoin(){
        Instance.SFX_Coin.Play();
    }

    public static void playGrow(){
        Instance.SFX_Grow.Play();
    }

    public static void playCorrect(){
        Instance.SFX_Correct.Play();
    }

    public static void playBasket(){
        Instance.SFX_Basket.Play();
    }

    public static void playCat(){
        Instance.SFX_Cat.Play();
    }

    public static void playOwl(){
        Instance.SFX_Owl.Play();
    }


    public static void playMainTheme(){
        Instance.BGM_mainTheme.Play();
    }

    public static void playDialogueTheme(){
        Instance.BGM_dialogueTheme.Play();
    }



    public void TurnOnAudio()
    {
        StopAllCoroutines();
        StartCoroutine(coroutine_TurnOnAudio());
    }
    IEnumerator coroutine_TurnOnAudio()
    {

        float volume = minDecibels;

        audioMixer.SetFloat("Volume", volume);

        while (volume < 0){
            volume += Time.deltaTime * Mathf.Abs(minDecibels) / fadeInTime;
            audioMixer.SetFloat("Volume", volume);
            yield return new WaitForEndOfFrame();
        }

        audioMixer.SetFloat("Volume", 0);
        
    }



    public void TurnOffAudio()
    {
        StopAllCoroutines();
        StartCoroutine(coroutine_TurnOffAudio());
    }
    IEnumerator coroutine_TurnOffAudio(){

        float volume = 0;

        audioMixer.SetFloat("Volume", volume);

        while (volume > minDecibels)
        {
            volume -= Time.deltaTime * Mathf.Abs(minDecibels) / fadeOutTime;
            audioMixer.SetFloat("Volume", volume);
            yield return new WaitForEndOfFrame();
        }

        audioMixer.SetFloat("Volume", minDecibels);
    }


    public void fadeOutSong(AudioSource _song, float _time)
    {
        StartCoroutine(coroutineFadeOutSong(_song, _time));
    }

    IEnumerator coroutineFadeOutSong(AudioSource song, float time)
    {
        while (song.volume > 0){
            song.volume -= Time.deltaTime/time;
            yield return new WaitForEndOfFrame();
        }

        song.Stop();
        song.volume = 1;
    }




}