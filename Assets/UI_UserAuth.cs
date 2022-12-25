using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UI_UserAuth : MonoBehaviour
{
    public static UI_UserAuth instance; private void Awake() {if(instance==null)instance=this;}
    public UnityEvent onMultiplayerEvent,onPrivateEvent;
    private void Start()
    {
        //SetupObjects();
    }
    public void OnLevelSelected(int _levelNo)
    {
        GameManager.instance.SelectedLevel=_levelNo;
    }
    public void SetupObjects()
    {
        if (GameManager.instance.selectedMatchMode == MatchMode.Multiplayer)
        {
            onMultiplayerEvent.Invoke();
        }
        else if (GameManager.instance.selectedMatchMode == MatchMode.Private)
        {
            onPrivateEvent.Invoke();
        }
    }
}
