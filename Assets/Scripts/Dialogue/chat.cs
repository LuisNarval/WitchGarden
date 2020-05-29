using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CHATSIDE {LEFT, RIGHT}

[System.Serializable]
public class chat
{
    public string name;
    public CHATSIDE chatSide;

    [TextArea(3, 10)]
    public string[] sentence;
}