using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon;
using Photon.Realtime;
public class QMQuickMatch : MonoBehaviourPunCallbacks
{
    public GameObject[] levels;
    public static QMQuickMatch instance;
    public int currentTurnID = 1;
    public int myPosition;
    public int roundNo=0;
    public bool gameFinished;
    private void Awake()
    {
        instance = this;
        //myPosition = int.Parse(PhotonNetwork.LocalPlayer.TagObject.ToString());
    }
    public Dictionary<int,float> ScoreBoard=new Dictionary<int, float>();

    private void Start()
    {
        myPosition=int.Parse(PhotonNetwork.LocalPlayer.TagObject.ToString());
        for (int i = 1; i <= 5; i++)
        {
          ScoreBoard.Add(i,0);
        }
        if(PhotonNetwork.IsMasterClient)
        {
            SetLevel();
            QMEventManager.instance.SetEnvironmentEventSender(GameManager.instance.SelectedLevel);
        }
        currentTurnID = 1;
        //SetUIData();
    }
    public void SetLevel()
    {
        levels[GameManager.instance.SelectedLevel - 1].SetActive(true);
    }
    public GameObject Penny;
    public Transform spawnerPos;
    public bool Updated;
    public async void TurnUpdate(float score)
    {
        await System.Threading.Tasks.Task.Delay(2000);
        if (!Updated)
        {
            print("score is "+score);
            if (currentTurnID >= PhotonNetwork.CurrentRoom.PlayerCount)
            {
                currentTurnID = 1;
            }
            else
            {
                currentTurnID++;
            }
            if (!gameFinished)
            {
               //QM_UIController.instance.SetTurnUI();
                QMSpawner.instance.SpawnerObjects();
            }
            QM_UIController.instance.SetTurnUI(currentTurnID);
            QMEventManager.instance.SetTurnUIEventSender(currentTurnID);
            Updated = false;
            print("turn updated ID is " + currentTurnID + " your ID is " + PhotonNetwork.LocalPlayer.TagObject);
        }
    }
    public void UpdateScoreSlots(float score)
    {
        ScoreBoard[currentTurnID]+=score;
        if(IsMyTurn())
        {
        print("my score updated");
            QM_UIController.instance.MyUIData.avatar.transform.GetChild(1).GetComponent<Text>().text=ScoreBoard[currentTurnID].ToString("0.00")+"m";
        }
        else if(currentTurnID<myPosition)
        {
        print("my gunior score updated");
            QM_UIController.instance.playersUIDatas[currentTurnID-1].avatar.transform.GetChild(1).GetComponent<Text>().text=ScoreBoard[currentTurnID].ToString("0.00")+"m";
        }
        else if(currentTurnID>myPosition)
        {
        print("my senior score updated");
            QM_UIController.instance.playersUIDatas[currentTurnID-2].avatar.transform.GetChild(1).GetComponent<Text>().text=ScoreBoard[currentTurnID].ToString("0.00")+"m";
        }
        print("updated score of player "+ScoreBoard[currentTurnID]);
    }
    public bool WinnerChecker()
    {
      float max=ScoreBoard[1];
      int winnerTag=1;
      for (int i = 1; i <= ScoreBoard.Count; i++)
      {
        if(ScoreBoard[i]>max)
        {
            max=ScoreBoard[i];
            winnerTag=i;
        }    
      }   
      print("winner tag = "+winnerTag);
      if(winnerTag==myPosition)
      {
          return true;
      }
      return false;
    }
    public bool IsMyTurn()
    {
        return currentTurnID == int.Parse(PhotonNetwork.LocalPlayer.TagObject.ToString());
    }

}
