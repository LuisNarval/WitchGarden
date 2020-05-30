using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CHARACTERTYPE { GIRL, WITCH, CLIENT }

[System.Serializable]
public class chat
{
    public string name;
    public CHARACTERTYPE charactherType;

    [TextArea(3, 10)]
    public string[] sentence;
}