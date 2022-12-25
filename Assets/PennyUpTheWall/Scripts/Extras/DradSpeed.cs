using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This script attached to speedometer
/// </summary>
public class DradSpeed : MonoBehaviour
{
    [SerializeField] public Slider SliderSpeed;
    //float SliderSpeed =0f;
    [SerializeField] float SliderMaxValue;
    [SerializeField] float NeedleStartZValue;
    [SerializeField] Image FillImage;
    float currentSpeed = 0f;
    float targetspeed = 0f;
    float NeedleSpeed = 100f;
    public GameObject fire_obj_on_Drag;

    public static DradSpeed _instance;

    private void Awake()
    {
        Time.timeScale = 1;
        if (_instance == null)
            _instance = this;
    }

    void Update()
    {

        if (targetspeed != currentSpeed)
        {
            //Debug.Log("Update speed ");
            UpdateSpeed();
        }
    }


    public void SetSpeed(float Value)
    {
      //  Debug.Log("Speed in speedo function " + Value);
        //targetspeed = SliderSpeed.value;
        SliderSpeed.value = Value;
        targetspeed = SliderSpeed.value;
        FillImage.fillAmount = targetspeed / SliderMaxValue;
        Vector3 v1 = FillImage.gameObject.transform.position;
        v1.y = -(FillImage.fillAmount)/4f;

        // fire_obj_on_Drag.transform.position.y.Equals(FillImage.fillAmount/4f);
        //v1
        v1 = fire_obj_on_Drag.transform.position;
        fire_obj_on_Drag.transform.position = new Vector3(fire_obj_on_Drag.transform.position.x, (fire_obj_on_Drag.transform.position.y-FillImage.fillAmount / 4f), fire_obj_on_Drag.transform.position.z);

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
