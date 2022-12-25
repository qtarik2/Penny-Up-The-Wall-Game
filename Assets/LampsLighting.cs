using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampsLighting : MonoBehaviour
{
    public Transform[] lamps;
    public void TurnOnLight(bool on)
    {
        foreach (var item in lamps)
        {
            if(item.gameObject.activeInHierarchy)
            {
                item.GetChild(0).gameObject.SetActive(on);
                if(on)item.GetChild(0).GetComponent<Light>().intensity=15;
                break;
            }
        }
    }

}
