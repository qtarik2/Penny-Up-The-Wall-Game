using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Testing : MonoBehaviour
{

    public RectTransform mainMenu,settingsPanel;
    Vector3 startPos;
    private void Start()
    {
        startPos = settingsPanel.transform.position;
        MoveTheObj();
    }

    public void MoveTheObj()
    {
        mainMenu.DOAnchorPos(Vector3.zero, 0.25f);
        //transform.DOMove()
    }
    public void CloseButton()
    {
        settingsPanel.transform.position = startPos;
    }
}
