using UnityEngine;
using System.Collections.Generic;
public class LoadObstacles : MonoBehaviour
{
    [SerializeField] public BossesData _bossesData;
    public GameObject boss;
    GameObject animal;
    int lastValue;
    int val;
    int i;
    private void Start()
    {
    }
    public void LoadLevelObstacles()
    {
        List<GameObject> obstacles = _bossesData._totalBossesLevel[LevelSelection._clickedLevelNo].
           _eachLevelBosses[_bossesData._totalBossesLevel[LevelSelection._clickedLevelNo]._UnlockedBossNo].
           each_Level_Obstacles;
        //List<GameObject> obstacles = _bossesData._totalBossesLevel[LevelSelection._clickedLevelNo].
        //   _eachLevelBosses[boss.GetComponent<LoadBossData>()._Boss_number].each_Level_Obstacles;

//        Debug.Log(LevelSelection._clickedLevelNo + "Click");
        val = Random.Range(0, obstacles.Count);
        while (lastValue == val)
        {
            val = Random.Range(0, obstacles.Count);
        }
        lastValue = val;
//        Debug.Log(lastValue + "Last value");
        //Debug.Log(_bossesData._totalBossesLevel[LevelSelection._clickedLevelNo].
        //   _eachLevelBosses[boss.GetComponent<LoadBossData>()._Boss_number].each_Level_Obstacles[boss.GetComponent<LoadBossData>()._Boss_number].name + "Name");
        if (obstacles.Count != 0)
        {
            animal = Instantiate(obstacles[lastValue],
            transform.position, Quaternion.identity);
            animal.transform.SetParent(LevelSelection.instance._allScenes[LevelSelection._clickedLevelNo].transform.GetChild(0).transform);
            animal.transform.localPosition = new Vector3(0f, 0f, 0f);
//            Debug.Log(LevelSelection._clickedLevelNo + "no");
            if (LevelSelection._clickedLevelNo == 8)
            {
                Debug.Log("Ja ja turr ja");
                //animal.GetComponent<Patrolling>().enabled = false;
            }
        }
        else
        {
            return;
        }
    }
    public void DestroyObstacle()
    {
        Destroy(LevelSelection.instance._allScenes[LevelSelection._clickedLevelNo].transform.GetChild(0).transform.GetChild(0).gameObject);
    }
}
