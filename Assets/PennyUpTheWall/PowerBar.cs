using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PowerBar : MonoBehaviour
{
    public GameObject powerCircle;
    [SerializeField]
    public AccuracyRange accuracyRange = new AccuracyRange();
    public TextMeshProUGUI accuracyTxt;
    public Image handleImg;
    public Color32 red, green, yellow;
    public static PowerBar instance;
    public bool isConstructor;
    public Slider powerBarSlider;
    public float SetSpeed;

    private void Awake()
    {
        if (instance == null)
        {
        }
        instance = this;
    }
    public Animator anim;
    public enum Point { upper, lower };
    public Point handlePoint;
    public Transform handle, upperPoint, lowerPoint;
    public float handleSpeed;
    // private void Update()
    // {
    //     if (Vector3.Distance(upperPoint.position, handle.position) == 0)
    //     {
    //         handlePoint = Point.lower;
    //     }
    //     else if (Vector3.Distance(lowerPoint.position, handle.position) == 0)
    //     {
    //         handlePoint = Point.upper;
    //     }
    // }
    // private void LateUpdate()
    // {

    //     if (handlePoint == Point.upper)
    //         handle.position = Vector3.Lerp(lowerPoint.position, upperPoint.position, handleSpeed * Time.deltaTime);

    //     else
    //         handle.position = Vector3.Lerp(upperPoint.position, lowerPoint.position, handleSpeed * Time.deltaTime);
    // }

    private void Update()
    {
        if(LevelSelection._clickedLevelNo > 5)
        {
            GetComponent<Image>().enabled = false;
            handle.gameObject.SetActive(false);
            powerCircle.SetActive(true);
        }
        else
        {
            GetComponent<Image>().enabled = true;
            handle.gameObject.SetActive(true);
            powerCircle.SetActive(false);
        }
    }

    public void AccuracyChecker()
    {
        float point = handleImg.rectTransform.localPosition.y;
        accuracyTxt.gameObject.SetActive(true);
        //        print("11 y pos is " + point);
        if (point > accuracyRange.Perfact.min && point < accuracyRange.Perfact.max)
        {
            print("11Perfect");
            GameControlBridge.powerMulti = accuracyRange.perfactPower;
            accuracyTxt.text = "Perfect";
            accuracyTxt.color = UnityEngine.Color.blue;
            SetSpeed = 2f;
        }
        else if ((point > accuracyRange.best.min && point < accuracyRange.best.max) || (point > accuracyRange.best2.min && point < accuracyRange.best2.max))
        {
            accuracyTxt.text = "Best";
            //            print("11best");
            GameControlBridge.powerMulti = accuracyRange.bestPower;
            accuracyTxt.color = UnityEngine.Color.green;
            SetSpeed = 1.7f;
        }
        else if ((point > accuracyRange.good.min && point < accuracyRange.good.max) || (point > accuracyRange.good2.min && point < accuracyRange.good2.max))
        {
            accuracyTxt.text = "Good";
            GameControlBridge.powerMulti = accuracyRange.goodPower;
            accuracyTxt.color = UnityEngine.Color.yellow;
            print("11good");
            SetSpeed = 1.4f;
        }
        else if ((point > accuracyRange.ok.min && point < accuracyRange.ok.max) || (point > accuracyRange.ok2.min && point < accuracyRange.ok2.max))
        {
            accuracyTxt.text = "OK";
            GameControlBridge.powerMulti = accuracyRange.okPower;
            print("11ok");
            accuracyTxt.color = UnityEngine.Color.red;
            SetSpeed = 1.1f;
        }
        else
        {
            accuracyTxt.text = "Bad";
            GameControlBridge.powerMulti = accuracyRange.badPower;
            print("11bad");
            accuracyTxt.color = UnityEngine.Color.black;
            SetSpeed = 0.8f;
        }
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        accuracyTxt.gameObject.SetActive(false);
    }
    public enum Color { red, yellow, green }
    public Color pointColor;

    public float redPower, yellowPower, greenPower;

}
[System.Serializable]
public class AccuracyRange
{
    public MinMax Perfact, best, best2, good, good2, ok, ok2, bad;
    public float perfactPower, bestPower, goodPower, okPower, badPower;
    [System.Serializable]
    public struct MinMax
    {
        public float min, max;
    }
}
