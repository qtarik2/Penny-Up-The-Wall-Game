using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This script attached to Level Bosses Panel
/// </summary>
public class LoadLevelBosses : MonoBehaviour
{


    [SerializeField] public BossesData _bossesData;
    [SerializeField] public Button[] _bossesButtons;


    // Start is called before the first frame update
    void Start()
    {
        LoadBossesData();
    }
    public void LoadBossesData()
    {
        //Save temporary current level number for later use 
        var CurrentLevelNo = (PlayerPrefs.GetInt("LevelNumber"));
        Debug.Log("buttons length " + _bossesButtons.Length);
        for (int i = 0; i < _bossesButtons.Length; i++)
        {
            Debug.Log(i);
        //Get boss image from boss asset data file and show on boss popup panel
        _bossesButtons[i].GetComponent<Image>().sprite = _bossesData._totalBossesLevel[0]._eachLevelBosses[i].BossImage;
            //Debug.Log("get boss name: " + _bossesData._totalBossesLevel[0]._eachLevelBosses[i].BossName);
        }


    }
}
