using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


//Create a list of Hand data

[CreateAssetMenu(fileName = "HandData", menuName = "ScriptableObjects/Hand", order = 1)]
public class HandData : ScriptableObject
{
    public List<Hands> _HandData = new List<Hands>();
}
[Serializable]
public class Hands
{
    public Sprite HandImage;
    public string Hand_Type;
    public Material []_handMaterial;
   
}