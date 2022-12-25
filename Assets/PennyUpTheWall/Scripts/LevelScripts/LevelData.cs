using System;
using System.Collections.Generic;
using UnityEngine;
//Create a list of serializable class for Level data
[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public List<Levels> _LevelData = new List<Levels>();
}
[Serializable]
public class Levels
{
    public string LevelName;
    public int LevelNumber;
    public Sprite LevelImage;
    public Sprite LockImage;
    public Sprite UnlockImage;
    public Sprite LevelNameImage;
    public int Price;
}
