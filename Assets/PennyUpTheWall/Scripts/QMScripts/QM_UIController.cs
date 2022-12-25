using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class QM_UIController : MonoBehaviour
{
    public static QM_UIController instance;
    public GameObject winPanel, losePanel;
    public GameObject _speedoMeter;
    public GameObject _DragSpeed;
    public Text playerTagTxt;
    public Image _swipeHand = null;
    public void Awake()
    {
        print("My player Tag is " + PhotonNetwork.LocalPlayer.TagObject);
            instance = this;
        
    }
    private void Start()
    {
        SetTurnUI();
        if (PhotonNetwork.IsMasterClient)
        {
            //SetTurnUI();
        }
        SetTurnUI(1);
        // playerTagTxt.text=PhotonNetwork.LocalPlayer.TagObject.ToString();
    }
    public void WinLevel()
    {
        winPanel.SetActive(true);
        DeletePhotonObject();
    }
    public void LoseLevel()
    {
        losePanel.SetActive(true);
        DeletePhotonObject();
    }
    public void DeletePhotonObject()
    {
        // warning Destroy(FindObjectOfType<PhotonHandler>().gameObject);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        Destroy(FindObjectOfType<PhotonManager>().gameObject);
        Destroy(FindObjectOfType<PlayfabManager>().gameObject);
        // Destroy(FindObjectOfType<Facebook.Unity.CodelessUIInteractEvent>().gameObject);
    }
    public void ContinueButton()
    {
        //SceneManager.UnloadSceneAsync("DontDestroyOnLoad");
        // DestroyImmediate(FindObjectOfType<FacebookManager>().gameObject);
        //  DestroyImmediate(FindObjectOfType<AppleManager>().gameObject);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    public void SetUIData()
    {
        MyUIData.position.text = PhotonNetwork.LocalPlayer.NickName;
       // var Roomplayers = PhotonNetwork.CurrentRoom.Players;
        print("room player length is " + PhotonNetwork.CurrentRoom.Players.Count);
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            playersUIDatas[i].avatar.gameObject.SetActive(false);
            print("get player which index " + i);
            if (int.Parse(PhotonNetwork.CurrentRoom.Players[i].TagObject.ToString()) == int.Parse(PhotonNetwork.LocalPlayer.TagObject.ToString()))
            {
                continue;
            }
            else
            {
                if (int.Parse(PhotonNetwork.CurrentRoom.Players[i].TagObject.ToString()) > int.Parse(PhotonNetwork.LocalPlayer.TagObject.ToString()))
                {
                    playersUIDatas[i - 1].avatar.SetActive(true);
                    playersUIDatas[i - 1].position.text = PhotonNetwork.CurrentRoom.GetPlayer(i).UserId;
                }
                else
                {
                    playersUIDatas[i].avatar.SetActive(true);
                    playersUIDatas[i].position.text = PhotonNetwork.CurrentRoom.GetPlayer(i).UserId;
                }
            }
        }
    }
    public void SetTurnUI()
    {
      for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount-1; i++)
      {
          playersUIDatas[i].avatar.SetActive(true);
      }
    }
    public void SetTurnUI(int tag)
    {
        MyUIData.avatar.transform.GetChild(0).gameObject.SetActive(false);
        foreach (var item in playersUIDatas)
        {
            item.avatar.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (tag == int.Parse(PhotonNetwork.LocalPlayer.TagObject.ToString()))
        {
            MyUIData.avatar.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            if (tag > int.Parse(PhotonNetwork.LocalPlayer.TagObject.ToString()))
            {
                playersUIDatas[tag - 2].avatar.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                playersUIDatas[tag - 1].avatar.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        print("tag player turn ui updated " + tag);
    }
    public PlayersUIData MyUIData;
    public List<PlayersUIData> playersUIDatas = new List<PlayersUIData>();
    [System.Serializable]
    public class PlayersUIData
    {
        public GameObject avatar;
        public Text position;
    }
}
