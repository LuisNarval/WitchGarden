using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Kind", menuName = "Plant Kind")]
public class Kind : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite[] sprites; 

}