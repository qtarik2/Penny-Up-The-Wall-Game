using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
    public static ResultController instance;
    public int resultNumber=0;
    public Sprite winImg,loseImg,currentImg,defultImg;
    public Sprite red, green;
    public List<GameObject> mettreShow;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public object myObj;
    public void ResultSetter(bool result)
    {
        myObj=1;
        myObj="2";
        print("result is "+result);
        //transform.GetChild(resultNumber+1).gameObject.SetActive(true);
        transform.GetChild(resultNumber+1).GetComponent<Image>().sprite=currentImg;
        if(result)
        {
            transform.GetChild(resultNumber).GetComponent<Image>().sprite=winImg;
            mettreShow[resultNumber].SetActive(true);
            mettreShow[resultNumber].GetComponent<Image>().sprite = green;
            mettreShow[resultNumber].transform.GetChild(0).GetComponent<Text>().text = PosBar.instance.playerMettre.ToString("F2") + " Metre";
        }
        else
        {
            transform.GetChild(resultNumber).GetComponent<Image>().sprite=loseImg;
            mettreShow[resultNumber].SetActive(true);
            mettreShow[resultNumber].GetComponent<Image>().sprite = red;
            mettreShow[resultNumber].transform.GetChild(0).GetComponent<Text>().text = PosBar.instance.bossMettre.ToString("F2") + " Metre";
        }
        resultNumber++;
    }
    public void Reset()
    {
        resultNumber=0;
        transform.GetChild(0).GetComponent<Image>().sprite=currentImg;
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().sprite=defultImg;
        }
        mettreShow.ForEach(g => g.SetActive(false));
    }
}
