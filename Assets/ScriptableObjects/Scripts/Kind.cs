using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SPECIES { BELLADONA, DRAGONMOUTH, LAVANDA, MANDRAGORA, ORKILLER, STRONIUM }

[CreateAssetMenu(fileName = "New Kind", menuName = "Plant Kind")]
public class Kind : ScriptableObject
{
    public new string name;
    public SPECIES species; 
    public string description;

    public Sprite plantCursor;

    public Sprite[] sprites; 



}