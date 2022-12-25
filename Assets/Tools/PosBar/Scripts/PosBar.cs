using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PosBar : MonoBehaviour
{
    public static PosBar instance;

    public float percentage;

    public Transform minPosUI;
    public Transform maxPosUI;
    public Transform VSPosUI;
    public Transform coinDroppedUI;
    public Text percentageText;

    public Transform minPos;
    public Transform maxPos;
    public Transform coinDropped;

    public GameObject iconIns;

    public Sprite player, boss;
    
    [HideInInspector]
    public List<GameObject> coinIns = new List<GameObject>();

    public bool stopFollow;
    bool isPlayer;

    public float playerMettre;
    public float bossMettre;


    private void Awake()
    {
        if (instance == null)
        {
        }
        instance = this;
    }

    public void InstatitateIcon(Transform follow, bool isPlayerI)
    {
        GameObject ins = null;
        if (isPlayerI) { ins = Instantiate(iconIns, minPosUI); }
        if (!isPlayerI){ ins = Instantiate(iconIns, VSPosUI); }
        coinDropped = follow;
        coinDroppedUI = ins.transform;
        //percentageText = coinDroppedUI.transform.GetChild(0).GetComponent<Text>();
        if (!isPlayerI) { coinDroppedUI.GetComponent<Image>().sprite = boss; }
        if (isPlayerI) { coinDroppedUI.GetComponent<Image>().sprite = player; }
        coinIns.Add(ins);
        isPlayer = isPlayerI;
        float percentageCalc = minPos.localPosition.z - maxPos.localPosition.z;
        float coinCalc = minPos.localPosition.z - coinDropped.localPosition.z;
        percentage = (coinCalc / percentageCalc) * 100;

        float percentageCalcUI = minPosUI.localPosition.x - maxPosUI.localPosition.x;
        float coinCalcUI = minPosUI.localPosition.x - coinDroppedUI.localPosition.x;
        float percentageToUI = (percentage / 100) * percentageCalcUI;
        float pos = Mathf.Clamp((percentageToUI), 0f, 960f);
        if (isPlayer) { coinDroppedUI.localPosition = new Vector3(-pos, coinDroppedUI.localPosition.y, coinDroppedUI.localPosition.z); playerMettre = (percentage / 100) * 7; }
        if (!isPlayer) { coinDroppedUI.localPosition = new Vector3(pos, coinDroppedUI.localPosition.y, coinDroppedUI.localPosition.z); bossMettre = (percentage / 100) * 7; }
    }

    public void removeAllIns()
    {
        coinIns.ForEach(i => Destroy(i));
        playerMettre = 0f;
        bossMettre = 0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(minPos.position, .6f);
        Gizmos.DrawSphere(maxPos.position, .6f);
    }

}
