using System;
using System.Collections.Generic;
using UnityEngine;


//Create a list of serializable class for 15 levels Bosses data

[CreateAssetMenu(fileName = "BossesData", menuName = "ScriptableObjects/BossesData", order = 2)]
public class BossesData : ScriptableObject
{
  //List of All Levels
  public List<TotalBossesLevel> _totalBossesLevel = new List<TotalBossesLevel>();
   
}

[Serializable]
public class TotalBossesLevel
{
    //List of each level bosses
    public List<EachLevelBosses> _eachLevelBosses = new List<EachLevelBosses>();
    public int _UnlockedBossNo;
}

[System.Serializable]
public class EachLevelBosses
{
    public string BossName;
    public Sprite BossImage;
    public string  []BossDialogue;
    public Material []Hand_material_of_boss;
    public bool gander;
    public string AnimationSide;
    public int bet;
    public GameObject BosseModel;
    public List<AudioClip> bossAudioClips;
    public List<GameObject> each_Level_Obstacles;
}
