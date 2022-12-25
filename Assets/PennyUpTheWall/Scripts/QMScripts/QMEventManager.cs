using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class QMEventManager : MonoBehaviourPun
{
    public static QMEventManager instance; private void Awake() { instance = this; }
    public Dictionary<int, int> score = new Dictionary<int, int>();
    public const byte WinnerCheckEventCode = 1;
    public const byte SetTurnUIEventCode = 2;
    public const byte SetEnvironmentEventCode = 3;
    public const byte UpdateScoreEventCode = 4;
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += EventDataReciver;
    }
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= EventDataReciver;
    }
    public void EventDataReciver(EventData obj)
    {
        if (obj.Code == WinnerCheckEventCode)
        {
            //score.Add(int.Parse(PhotonNetwork.CurrentRoom.GetPlayer(obj.Sender).TagObject.ToString()),obj.CustomDataKey);
        }
        if (obj.Code == SetTurnUIEventCode)
        {
            QM_UIController.instance.SetTurnUI((int)obj.CustomData);
        }
        if (obj.Code == SetEnvironmentEventCode)
        {
            //QM_UIController.instance.SetTurnUI((int)obj.CustomData);
            GameManager.instance.SelectedLevel = (int)obj.CustomData;
            QMQuickMatch.instance.SetLevel();
        }
        if (obj.Code == UpdateScoreEventCode)
        {
            QMQuickMatch.instance.UpdateScoreSlots((float)obj.CustomData);
        }
    }
    public void ScoreEventSender(float distance)
    {
        RaiseEventOptions eventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(WinnerCheckEventCode, distance, eventOptions, SendOptions.SendUnreliable);
    }
    public void SetTurnUIEventSender(int playerTag)
    {
        PhotonNetwork.RaiseEvent(SetTurnUIEventCode, playerTag, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendUnreliable);
    }
    public void SetEnvironmentEventSender(int levelNO)
    {
        PhotonNetwork.RaiseEvent(SetEnvironmentEventCode, levelNO, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendUnreliable);
    }
    public void UpdateScoreEventSender(float score)
    {
        PhotonNetwork.RaiseEvent(UpdateScoreEventCode, score, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendUnreliable);
    }
}