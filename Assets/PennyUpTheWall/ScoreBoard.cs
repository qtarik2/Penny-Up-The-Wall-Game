using UnityEngine;
using UnityEngine.UI;
public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public GameObject winPng, losePng;
    public GameObject[] scoreObjects;
    public void OnUpdateScore(float distance)
    {
        foreach (var item in scoreObjects)
        {
            if (!item.activeInHierarchy)
            {
                item.transform.GetChild(0).GetComponent<Text>().text = distance.ToString("0.00") + "Meter";
                item.SetActive(true);
                break;
            }
        }
    }
    public void ResetScore()
    {
        winPng.SetActive(false);
        losePng.SetActive(false);
        foreach (var item in scoreObjects)
        {
            item.SetActive(false);
        }
    }
}
