using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
/// <summary>
/// This script attached to speedometer
/// </summary>
public class SpeedoMeter : MonoBehaviour
{
    //[SerializeField] RectTransform _rectTransform; //RectTransform of the moving speed pin
    public Transform handPvt;
    [SerializeField] public Slider SliderSpeed;
    //float SliderSpeed =0f;
    public static Animator anim;
    public static DOTweenAnimation doAnim;
    public GameObject powerBar;
    [SerializeField] float SliderMaxValue;
    [SerializeField] float NeedleStartZValue;
    [SerializeField] Image FillImage_right, FillImage_left;
    float currentSpeed = 0f;
    float targetspeed = 0f;
    float NeedleSpeed = 100f;
    public GameObject Lf, Ri;

    public static SpeedoMeter _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    public void OnEnable()
    {
        if (GameManager.instance)
            if (GameManager.instance.selectedMatchMode == MatchMode.Multiplayer && QMQuickMatch.instance.IsMyTurn())
            {
                doAnim = anim.GetComponent<DOTweenAnimation>();
            }
    }
    public void Start()
    {
        if (GameManager.instance.selectedMatchMode == MatchMode.Campaign)
        {
            anim = powerBar.GetComponent<Animator>();
            doAnim = powerBar.GetComponent<DOTweenAnimation>();
        }
    }
    void Update()
    {
        if (targetspeed != currentSpeed)
        {
            UpdateSpeed();
        }
    }
    public void SetSpeed(float Value)
    {
        //   Debug.Log("Speed in speedo function " + Value);
        //targetspeed = SliderSpeed.value;
            var c = Mathf.Clamp(Value * 5, 1, 10);
            handPvt.eulerAngles = Vector3.right * c;
        if (GameManager.instance.selectedMatchMode == MatchMode.Campaign)
        {
        }
        else
        {
            //var c = Mathf.Clamp(Value * 5, 1, 30);
           // QMHandControlScript.instance.gameObject.transform.GetChild(0).eulerAngles = Vector3.right * c;
        }
        if (PlayerPrefs.GetString("Handed").Equals("Right"))
        {
            Ri.SetActive(true);
            Lf.SetActive(false);
            SliderSpeed.value = Value;
            targetspeed = SliderSpeed.value;
            FillImage_right.fillAmount = targetspeed / SliderMaxValue;
            anim.speed = 1 + FillImage_right.fillAmount;
        }
        else
        if (PlayerPrefs.GetString("Handed").Equals("Left"))
        {
            Ri.SetActive(false);
            Lf.SetActive(true);
            SliderSpeed.value = Value;
            targetspeed = SliderSpeed.value;
            FillImage_left.fillAmount = targetspeed / SliderMaxValue;
            anim.speed = 1 + FillImage_left.fillAmount;
        }
        if (FillImage_right.fillAmount >= 0.7f)
        {
            if (!doAnim.hasOnPlay)
            {
                doAnim.DOPlay();
            }
            if (FillImage_right.fillAmount >= 0.99f)
            {
                doAnim.duration = 10f;
            }
            else
            {
                doAnim.duration = 2f;
            }
        }
        else
        {
            doAnim.DOPause();
        }
    }
    public void UpdateSpeed()
    {
        if (targetspeed > currentSpeed)
        {
            currentSpeed += Time.deltaTime * NeedleSpeed;
            currentSpeed = Mathf.Clamp(currentSpeed, 0.0f, targetspeed);
        }
        else if (targetspeed < currentSpeed)
        {
            currentSpeed -= Time.deltaTime * NeedleSpeed;
            currentSpeed = Mathf.Clamp(currentSpeed, targetspeed, SliderMaxValue);
        }

        //SetNeedle();
    }

    private void SetNeedle()
    {
        //1.3 is slider speed max value
        //120 is needle speed minimum angle
        //_rectTransform.transform.localEulerAngles = new Vector3(0, 0, (currentSpeed / SliderMaxValue * (NeedleStartZValue*2) - NeedleStartZValue) * -1.0f);
    }
}
