using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class WeatherController : MonoBehaviour
{
    public enum WeatherType { sunny, rainy, snowfall };
    public GameObject[] weatherObject;
    public WeatherType weatherType;
    public Material skybox;
    public float skyMoveTime;
    private void Start()
    {
        ChangeWeather();
        skybox = RenderSettings.skybox;
    }
    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", RenderSettings.skybox.GetFloat("_Rotation") + Time.deltaTime * skyMoveTime);
    }
    // IEnumerator Timer(int time)
    // {
    //     yield return new WaitForSeconds(time);
    //     weatherType=WeatherType.sunny;
    //     ChangeWeather();
    // }
    async void ChangeWeather()
    {
        while (true)
        {
           Array weatherArray=Enum.GetValues(typeof(WeatherType));
           weatherType=(WeatherType) weatherArray.GetValue(1);
            switch (weatherType)
            {
                case WeatherType.sunny:
                    foreach (var item in weatherObject)
                    {
                        item.SetActive(false);
                    }
                    weatherObject[0].SetActive(true);
                    break;
                case WeatherType.rainy:
                    foreach (var item in weatherObject)
                    {
                        item.SetActive(false);
                    }
                    weatherObject[1].SetActive(true);
                    break;
                case WeatherType.snowfall:
                    foreach (var item in weatherObject)
                    {
                        item.SetActive(false);
                    }
                    weatherObject[2].SetActive(true);
                    break;
            }
            await Task.Delay(5 * 100 * 60);
        }
    }
}
