using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System;
using Photon.Pun;
using Photon;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using PlayFab;
using PlayFab.ClientModels;
public class PhotonManager : MonoBehaviourPunCallbacks
{
    public UnityEvent OnRoomJoined, OnRoomJoinFailed, OnMatchCancel;
    public static PhotonManager instance;
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
    public Text IDtxt;
    public Text[] userNameTxt;
    public Button quickMatchButton, stopMatchButton;
    public override void OnConnectedToMaster()
    {
        //        IDtxt.text = (string)PhotonNetwork.LocalPlayer.TagObject;
        print("connected to master success");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
        // quickMatchButton.gameObject.SetActive(true);
    }
    public Button joinButton, createButton;
    public override void OnJoinedLobby()
    {
        joinButton.interactable = true;
        createButton.interactable = true;
        PhotonNetwork.AuthValues.UserId = PlayFab.PlayFabSettings.staticPlayer.PlayFabId;
        PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("Email");
        if (GameManager.instance.selectedMatchMode == MatchMode.Multiplayer)
        {
            QuickMatch();
        }
    }
    public void SetName(string id)
    {
        PhotonNetwork.LocalPlayer.NickName = id;
        //  IDtxt.text = (string)PhotonNetwork.LocalPlayer.TagObject;
    }
    public InputField roomNameField;
    public void CreatPrivateRoom()
    {
        print("server name is " + PhotonNetwork.ServerAddress);
        if (roomNameField.text.Length < 4)
        {
            PlayfabManager.instance.logtxt.gameObject.SetActive(true);
            PlayfabManager.instance.logtxt.text = "RoomName must have at least 4 character !";
            return;
        }
        RoomOptions roomOptions = new RoomOptions { IsOpen = true, IsVisible = false, MaxPlayers = GameManager.instance.maxPlayers };
        PhotonNetwork.CreateRoom(roomNameField.text, roomOptions);
    }
    public void JoinPrivateRoom()
    {
        //StartCoroutine(nameof(WaitPlayers));
        PhotonNetwork.JoinRoom(roomNameField.text);
    }
    public IEnumerator WaitPlayers()
    {
        print("current room name " + PhotonNetwork.CurrentRoom.Name);
        yield return new WaitForSeconds(1);
        //object _roomPlayer=PhotonNetwork.CurrentRoom.PlayerCount;
        yield return new WaitUntil(() => CheckRoomPlayers());
        timer = 0;
        StartMatching();
    }
    public UnityEvent OnCreateButtonClick;
    public void CheckRoomInputValidation()
    {
        if (roomNameField.text.Length < 4)
        {
            PlayfabManager.instance.logtxt.gameObject.SetActive(true);
            PlayfabManager.instance.logtxt.text = "RoomName must have at least 4 character !";
            return;
        }
        OnCreateButtonClick.Invoke();

    }
    public bool CheckRoomPlayers()
    {
        if (PhotonNetwork.InRoom)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
            {
                return true;
            }
            return false;
        }
        return false;
    }
    public void QuickMatch()
    {
        print("auth paremeters " + PhotonNetwork.AuthValues.AuthGetParameters);
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinedRoom()
    {
        StartCoroutine(nameof(WaitPlayers));
        PhotonNetwork.LocalPlayer.TagObject = PhotonNetwork.CurrentRoom.PlayerCount;
        OnRoomJoined.Invoke();

        UpdateMatchPanelUI();
        if (GameManager.instance.isSinglePlayer)
        {
            PhotonNetwork.LoadLevel("QuickMatch");
        }
    }
    public override void OnCreatedRoom()
    {
        PhotonNetwork.LocalPlayer.TagObject = PhotonNetwork.CurrentRoom.PlayerCount;
        //  roomPlayers.Add(PhotonNetwork.LocalPlayer);
        print("room created");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("join room failed now create new room");
        if (GameManager.instance.selectedMatchMode == MatchMode.Private)
        {
            OnRoomJoinFailed.Invoke();
            return;
        }
        MakeRoom();
    }
    public void MakeRoom()
    {
        var RandomRoomName = UnityEngine.Random.Range(0, 5000);
        RoomOptions roomOptions = new RoomOptions { IsVisible = true, IsOpen = true, MaxPlayers = GameManager.instance.maxPlayers };
        PhotonNetwork.CreateRoom("RoomName" + RandomRoomName, roomOptions);
        print("room created waiting for another player");
    }
    public void UpdateMatchPanelUI()
    {
        int i = 0;
        foreach (var item in PhotonNetwork.CurrentRoom.Players)
        {
            userAvatars[i].SetActive(true);
            userAvatars[i].transform.GetChild(0).GetComponent<Text>().text = item.Value.NickName;
            print("set nick name :" + item.Value.NickName);
            // userAvatars[i].transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("Email");
            i++;
        }
    }
    public GameObject[] userAvatars;
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

        userAvatars[PhotonNetwork.CurrentRoom.PlayerCount - 2].SetActive(true);
        userAvatars[PhotonNetwork.CurrentRoom.PlayerCount - 2].transform.GetChild(0).GetComponent<Text>().text = newPlayer.NickName;
        newPlayer.TagObject = PhotonNetwork.CurrentRoom.PlayerCount;

        UpdateMatchPanelUI();
        if (PhotonNetwork.CurrentRoom.PlayerCount == GameManager.instance.maxPlayers && PhotonNetwork.IsMasterClient)
        {
            print("new player joined");
            PhotonNetwork.LoadLevel("QuickMatch");
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount >= GameManager.instance.requiredPlayers)
        {
            print("new player joined");
            timer = 0;
            //StartMatching();
        }
    }
    public Text matchTimertxt;
    int timer = 0;
    async void StartMatching()
    {
        while (timer < GameManager.instance.matchStartTime)
        {
            timer++;
            matchTimertxt.gameObject.SetActive(true);
            matchTimertxt.text = timer.ToString();
            await System.Threading.Tasks.Task.Delay(1000);
            if (matchTimertxt == null) break;
            matchTimertxt.gameObject.SetActive(false);
        }
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("QuickMatch");
        }
    }
    public override void OnLeftRoom()
    {
        Debug.Log("room Left");
    }
    public void StopMatching()
    {
        quickMatchButton.gameObject.SetActive(true);
        stopMatchButton.gameObject.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // print("player left the room");
        // PhotonNetwork.Disconnect();
        // GameManager.instance.LoadSceneByName("UserAuth",false,false);
        // GameManager.instance.LoadSceneByName("GamePlay",false,true);
        // DestroyImmediate(gameObject);
        // //StopCoroutine(WaitPlayers());
        // base.OnPlayerLeftRoom(otherPlayer);
        if (!QMQuickMatch.instance.gameFinished)
        {
            QM_UIController.instance.DeletePhotonObject();
            QM_UIController.instance.ContinueButton();
        }
    }
    public void CancelMatcing()
    {
        //StopCoroutine(WaitPlayers());
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.Disconnect();
            //PhotonNetwork.LeaveRoom();
            // PhotonNetwork.LeaveLobby();
        }
        OnMatchCancel?.Invoke();
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("UserAuth");
        UIManager.instance.logText.gameObject.SetActive(true);
        UIManager.instance.logText.text = "other player left the room!";
        DestroyImmediate(gameObject);
    }
    public void OnApplicationQuit()
    {
        print("quit game");
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
    }
}