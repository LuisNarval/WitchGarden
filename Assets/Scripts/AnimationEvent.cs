using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    public UnityEvent animEvent1;
    public UnityEvent animEvent2;
    public UnityEvent animEvent3;

    public void callEvent1(){
        animEvent1.Invoke();
    }

    public void callEvent2(){
        animEvent2.Invoke();
    }

    public void callEvent3(){
        animEvent3.Invoke();
    }

}