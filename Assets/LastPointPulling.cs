using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LastPointPulling : MonoBehaviour
{
    public static LastPointPulling instance;
    public Texture playerIMG, bossIMG;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public Vector3 lastPosition;
    public Transform[] objects;
    public Transform NextPoint(string distance,bool myTurn)
    {
        foreach (var item in objects)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                item.GetComponent<Renderer>().material.SetTexture("_MainTex", !myTurn ? bossIMG : playerIMG);
//                item.GetChild(0).transform.GetComponent<TextMesh>().text = distance;
                item.gameObject.SetActive(true);
                lastPosition=item.position;
                return item;
            }
        }
        lastPosition=transform.position;
        return transform;
    }
    public void DeleteOldPoints()
    {
        foreach (var item in objects)
        {
            item.gameObject.SetActive(false);
        }
    }
}
