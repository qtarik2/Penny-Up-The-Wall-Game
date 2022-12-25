using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    public bool IsUp;
    public bool IsDown;
    public bool IsDrag;

    public Spawner spawner;
    public PennyController pennyController;
    public PennyShoot pennyShoot;

    bool isEnabled;

    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    private void Update()
    {
        if(GameManager.instance.gameStates == GameStates.Playing) { isEnabled = true; GetComponent<Image>().enabled = true; }
        else { isEnabled = false; GetComponent<Image>().enabled = false; }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isEnabled)
            return;
        /*IsDrag = true;
        IsDown = false;
        IsUp = false;*/
        pennyController.OnDrag();
        pennyShoot.OnDrag();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isEnabled)
            return;
        /*IsDown = true;
        IsUp = false;*/
        pennyController.OnDown();
        pennyShoot.OnDown();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isEnabled)
            return;

        /*IsUp = true;
        IsDown = false;
        IsDrag = false;*/
        pennyController.OnUp();
        pennyShoot.OnUp();
    }

}
