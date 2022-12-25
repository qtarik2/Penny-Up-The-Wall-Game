using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWeahterController : MonoBehaviour
{
    [SerializeField]
    public LevelsWeatherCollection[] enviroWeathers;

    [Serializable]
    public class LevelsWeatherCollection
    {
        public EnviroWeatherPreset[] enviroWeatherPresets;
    }
    public EnviroZone enviroZone;
    private void Start() 
    {
        
    }
}
