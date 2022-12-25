using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
/// <summary>
/// This script Attached to LevelSelectionPanel/ScrollView/ViewPort/Content in Canvas
/// To get LevelData Asset file go to PennyUpTheWall/ScriptableData/LevelData
/// If you want to change Level Data For example LevelImages, Level Number, UnlockImages,LockedImages than just change through LevelData asset files which i mentioned above
/// </summary>
public class MenuSwipe : MonoBehaviour
{

    [SerializeField] public GameObject ScrollBar;//Reference to horizontal slider bar
    float scroll_pos = 0;
    float[] pos;//


    #region Singleton Region
    public static MenuSwipe instance;

    private void Awake()
    {
        if(instance == null) { instance = this; }
    }
    #endregion


    void Update()
    {

        #region Level Panel Scrolling Functionality

        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for(int i = 0; i < pos.Length;i++)
        {
            pos[i] = distance * i;
        }
        if(Input.GetMouseButton(0))
        {
            scroll_pos = ScrollBar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if(scroll_pos < pos[i] + (distance/2)&& scroll_pos > pos[i] - (distance/2))
                {
                    ScrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(ScrollBar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector3.Lerp(transform.GetChild(i).localScale, new Vector3(1f, 1f,1f),0.1f);
                for (int a = 0; a < pos.Length; a++)
                {
                    if(a!= i)
                    {
                        transform.GetChild(a).localScale = Vector3.Lerp(transform.GetChild(a).localScale, new Vector3(0.8f, 0.8f,0.8f), 0.1f);
                    }
                }
            }
        }
        #endregion
    }




}


