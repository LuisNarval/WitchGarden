using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    [Header("REFERENCE TO SCENE")]
    [Header("BGM")]
    public AudioSource BGM_playTheme;

    [Header("SFX")]
    public AudioSource SFX_Dig;
    public AudioSource SFX_Pour;
    public AudioSource SFX_Seeds;
    public AudioSource SFX_Cut;
    public AudioSource SFX_Coin;
    public AudioSource SFX_Correct;
    public AudioSource SFX_Basket;
    public AudioSource SFX_Grow;
    public AudioSource SFX_Cat;
    public AudioSource SFX_Owl;

    static AudioSource staticSFX_Dig;
    static AudioSource staticSFX_Pour;
    static AudioSource staticSFX_Seeds;
    static AudioSource staticSFX_Cut;
    static AudioSource staticSFX_Coin;
    static AudioSource staticSFX_Correct;
    static AudioSource staticSFX_Basket;
    static AudioSource staticSFX_Grow;
    static AudioSource staticSFX_Cat;
    static AudioSource staticSFX_Owl;

    // Start is called before the first frame update
    void Start(){
        staticSFX_Dig = SFX_Dig;
        staticSFX_Seeds = SFX_Seeds;
        staticSFX_Pour = SFX_Pour;
        staticSFX_Cut = SFX_Cut;
        staticSFX_Coin = SFX_Coin;
        staticSFX_Correct = SFX_Correct;
        staticSFX_Basket = SFX_Basket;
        staticSFX_Grow = SFX_Grow;
        staticSFX_Cat = SFX_Cat;
        staticSFX_Owl = SFX_Owl;
    }

 


    public static void playDig(){
        staticSFX_Dig.Play();
    }

    public static void playSeeds(){
        staticSFX_Seeds.Play();
    }

    public static void playPour(){
        staticSFX_Pour.Play();
    }

    public static void playCut(){
        staticSFX_Cut.Play();
    }


    public static void playCoin(){
        staticSFX_Coin.Play();
    }

    public static void playGrow(){
        staticSFX_Grow.Play();
    }

    public static void playCorrect(){
        staticSFX_Correct.Play();
    }

    public static void playBasket(){
        staticSFX_Basket.Play();
    }

    public static void playCat(){
        staticSFX_Cat.Play();
    }

    public static void playOwl(){
        staticSFX_Owl.Play();
    }
}
