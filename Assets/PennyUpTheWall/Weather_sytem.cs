using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weather_sytem : MonoBehaviour
{
    public static Weather_sytem instance;
    public GameObject snow, rain, fog;
    public Material night, day, darkstorm, moon, clouds, eveningclouds, dayevening, sun, nightskyebox, skybox;
    public Dictionary<string,Material> mat;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        if (PlayerPrefs.GetInt("currentlevelno") == 0)
        {
            rain.SetActive(true);
            RenderSettings.skybox = clouds;
        }
        else if (PlayerPrefs.GetInt("currentlevelno") == 1)
        {
            //snow.SetActive(true);
            RenderSettings.skybox = eveningclouds;
        }
        else if (PlayerPrefs.GetInt("currentlevelno") == 2)
        {
            RenderSettings.skybox = night;
            fog.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("currentlevelno") == 3)
        {
            RenderSettings.skybox = day;
        }
        else if (PlayerPrefs.GetInt("currentlevelno") == 4)
        {
            RenderSettings.skybox = darkstorm;
        }
        else if (PlayerPrefs.GetInt("currentlevelno") == 5)
        {
            RenderSettings.skybox = moon;
        }
        else if (PlayerPrefs.GetInt("currentlevelno") == 6)
        {
            RenderSettings.skybox = dayevening;
        }
        else if (PlayerPrefs.GetInt("currentlevelno") == 7)
        {
            RenderSettings.skybox = sun;
        }
        else if (PlayerPrefs.GetInt("currentlevelno") == 10)
        {
            RenderSettings.skybox = nightskyebox;
        }
        else if (PlayerPrefs.GetInt("currentlevelno") == 11)
        {
            RenderSettings.skybox = skybox;
        }
    }
}
