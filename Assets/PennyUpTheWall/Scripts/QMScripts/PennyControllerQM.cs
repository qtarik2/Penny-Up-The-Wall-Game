using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Photon.Pun;
public class PennyControllerQM : MonoBehaviour
{
    public PhotonView photonView;
    public GameObject PennyCamera;
    public GameObject LastCoinPoint;
    //public LineRenderer _LineRendrer;
    Vector3 mousedownPosition, mouseDragPosition, mouseRelesePosition;
    private Vector3 force;
    //For Male And Female
    public GameObject Player_Gander;
    //Penny rigidbody reference
    public Rigidbody rb;
    public float playerPower;
    //isShoot bool is using to check is penny released or not 
    private bool isShoot;
    bool collisonOccured = false;

    //GameObject text;
    public HandData hand_textur;
    //Singleton reference to access this script
    #region Singleton
    public static PennyControllerQM instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public int myTag;
    private void Start()
    {
        print("my tag is " + PhotonNetwork.LocalPlayer.TagObject);
        myTag = int.Parse(PhotonNetwork.LocalPlayer.TagObject.ToString());
        photonView = GetComponent<PhotonView>();
        Player_Gander = GameObject.FindGameObjectWithTag("All_Players");
        handFixPoint = QMHandControlScript.instance.HandfixPoint;
        pennyFunctionality = InHandFunctionality;
        if (PlayerPrefs.GetString("gander") == "male")
        {
            //For hand Active And Deactivetion 
            // GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(0).gameObject.SetActive(true);
            //  GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(1).gameObject.SetActive(false);
            //  Hand = Player_Gander.transform.GetChild(0).transform.GetChild(1).gameObject;
            // Hand.GetComponent<SkinnedMeshRenderer>().materials = hand_textur._HandData[PlayerPrefs.GetInt(PlayerPrefs.GetString("gander"))]._handMaterial;
        }
        //     else
        //    if (PlayerPrefs.GetString("gander") == "female")
        //     {
        //         GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(0).gameObject.SetActive(false);
        //         GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(1).gameObject.SetActive(true);
        //     }
        //     if (Player_Gander.transform.GetChild(0).gameObject.activeInHierarchy == true)
        //     {
        //         HandTransform = Player_Gander.transform.GetChild(0).transform;
        //     }
        //     if (Player_Gander.transform.GetChild(1).gameObject.activeInHierarchy == true)
        //     {
        //         HandTransform = Player_Gander.transform.GetChild(1).transform;
        //     }
        //     Hand.GetComponent<SkinnedMeshRenderer>().material = hand_textur._HandData[PlayerPrefs.GetInt(PlayerPrefs.GetString("gander"))]._handMaterial[0];
    }
    private void Update()
    {
        if (pennyFunctionality != null && photonView.IsMine)
        {
            InHandFunctionality();
            //pennyFunctionality.Invoke();
        }
    }
    public delegate void PennyFunctionality();
    PennyFunctionality pennyFunctionality;
    public Transform handFixPoint;
    public void InHandFunctionality()
    {
        //if(photonView.IsMine)
        transform.position = handFixPoint.position;
    }
    private void LateUpdate()
    {
        //Check if penny is released and not grounded yet rotate the penny 
        if (isShoot && !collisonOccured)
            transform.RotateAround(transform.position, Vector3.right, 540 * Time.deltaTime);
    }
    //for coin click only one time 
    public bool mouse_down = true;
    private void OnMouseDown()
    {
        if (mouse_down == false || !photonView.IsMine)
            return;
        // getting touch position when you touch the screen
        mousedownPosition = Input.mousePosition;
        //Set trail false when penny is not released
        Camera.main.ScreenToWorldPoint(new Vector3(0f, (Random.Range(0, Screen.width)), 0f));
    }
    private void OnMouseDrag()
    {
        if (mouse_down == false || !photonView.IsMine)
            return;
        if (!isShoot)//Call DrawTrajectory script function to draw trajectory and pass derived force,Rigidbody and penny clone current position
        {
            mouseRelesePosition = Input.mousePosition;
            force = mousedownPosition - mouseRelesePosition;
            Vector3 forceV = (new Vector3(force.x, force.y, force.y) * (force.magnitude / 1200));
            DrawTrajectory.instance.UpdateTrajectory(forceV, rb, this.transform.position);
            DrawTrajectory.instance.lineSegments = (int)force.y * 2;
            QM_UIController.instance._speedoMeter.SetActive(true);// Force Panel 
            QM_UIController.instance._DragSpeed.SetActive(true);//drag panel
            Vector3 velocity = (force / rb.mass) * Time.fixedDeltaTime;
            float FlightDuration = (2 * velocity.y / Physics.gravity.y);
            SpeedoMeter._instance.SetSpeed(-FlightDuration);
            DradSpeed._instance.SetSpeed(-FlightDuration);
        }
    }
    private void OnMouseUp()
    {
        if (mouse_down == false || !photonView.IsMine)
            return;
        rb.isKinematic = false;
        QM_UIController.instance._speedoMeter.SetActive(false);
        QM_UIController.instance._DragSpeed.SetActive(false);
        DrawTrajectory.instance.HideLine();
        if (mousedownPosition.y > mouseRelesePosition.y)
        {
            PowerBar.instance.anim.speed = 0;
            PowerBar.instance.AccuracyChecker();
            PowerBar.instance.gameObject.GetComponent<DOTweenAnimation>().DOPause();
            mouseRelesePosition = Input.mousePosition;
            force = mousedownPosition - mouseRelesePosition;
            QMHandControlScript.instance.GetComponent<Animator>().SetBool("HandAnimbool", true);
            mouse_down = false;
        }
        else if (mousedownPosition.y < mouseRelesePosition.y)
        {
            return;
        }
    }
    void Shoot()
    {
        if (isShoot) return;
        rb.isKinematic = false;
        rb.freezeRotation = false;
        rb.AddForce(new Vector3(force.x, force.y, force.y) * GameControlBridge.powerMulti * (force.magnitude / 400) * playerPower);
        AudioHandler.instance.ArmMovementSound();
        isShoot = true;
    }
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(3f);
    }
    public float distance;
    public LayerMask ignoreSoundLayer;
    private void OnCollisionEnter(Collision collision)
    {
        //CoinSoundController.instance.PlayCoinSound(collision.gameObject.layer);
        CoinSoundController.instance.PlayCoinSound(collision.transform.tag);
        if (collisonOccured) return;
        if (collision.transform.CompareTag("Ground"))
        {
            print("collision occured with ground");
            mouse_down = true;
            StartCoroutine(Fade());
            collisonOccured = true;
            distance = Mathf.Round(Vector3.Distance(new Vector3(466.651f,1.6689f,-120),this.transform.position));
            LastPointPulling.instance.NextPoint(distance.ToString("0.0"),true).position=transform.position;
            QMEventManager.instance.UpdateScoreEventSender(distance);
            ScoreBoard.instance.OnUpdateScore(distance);
            photonView.RPC(nameof(UpdateCoinPoint),RpcTarget.Others);
            photonView.RPC(nameof(UpdateTurn), RpcTarget.All);
            QMSpawner.instance.OffPenny();
            ///Update score
            if (photonView.IsMine)
            {
                //  photonView.RPC(nameof(UpdateScore),RpcTarget.All);
                QMEventManager.instance.ScoreEventSender(distance);
            }
            if (PhotonNetwork.CurrentRoom.PlayerCount == byte.Parse(PhotonNetwork.LocalPlayer.TagObject.ToString()))
            {
                photonView.RPC(nameof(GameResult), RpcTarget.All);
            }
            photonView.RPC(nameof(TurnComplete), RpcTarget.All);
        }
    }
    [PunRPC]
    public void UpdateCoinPoint()
    {
        LastPointPulling.instance.NextPoint(distance.ToString("0.0"),false).position=transform.position;
    }
    [PunRPC]
    public void TurnComplete()
    {   
        var cam=FindObjectOfType<CameraController>().gameObject;
        if(cam!=null)
        {
          Destroy(cam,2);
        }
    }
    //public int roundNo=0;
    [PunRPC]
    public async void GameResult()
    {
        LastPointPulling.instance.DeleteOldPoints();
        QMQuickMatch.instance.roundNo++;
        if (QMQuickMatch.instance.roundNo < GameManager.instance.totalRound) return;
        QMQuickMatch.instance.gameFinished=true;
        await System.Threading.Tasks.Task.Delay(2000);
      //  if (WinnerTagChecker() == PhotonNetwork.LocalPlayer.TagObject.ToString())
        if(QMQuickMatch.instance.WinnerChecker())
        {
            QM_UIController.instance.WinLevel();
            print("you won !");
        }
        else
        {
            QM_UIController.instance.LoseLevel();
            print("you lose !");
        }
    }
    [PunRPC]
    public void UpdateTurn()
    {
        QMQuickMatch.instance.TurnUpdate(distance);
        UpdateScore();
    }
    public string WinnerTagChecker()
    {
        // float biggest = playerScoreData[0].Score;
        // string winnerTag="1";
        // for (int i = 1; i < playerScoreData.Length; ++i)
        // {
        //     if (playerScoreData[i].Score > biggest)
        //     {
        //         biggest = playerScoreData[i].Score;
        //         winnerTag=(i+1).ToString();
        //     }
        // }
        // return winnerTag;
        return "123";
    }
    public void UpdateScore()
    {
        playerScoreData[QMQuickMatch.instance.currentTurnID-1].Score += distance;
        //QM_UIController.instance.playersUIDatas[QMQuickMatch.instance.currentTurnID-1].avatar.transform.GetChild(0).GetComponent<Text>().text=playerScoreData[QMQuickMatch.instance.currentTurnID-1].Score.ToString();
    }
    public void OnAnimationEventTrigger()
    {
        PennyCamera.SetActive(true);
        if (!photonView.IsMine) return;
        GetComponent<TrailRenderer>().enabled = true;
        pennyFunctionality = null;
        Shoot();
    }
    private void OnEnable()
    {

        QMQuickMatch.instance.Updated = false;
        rb.isKinematic = true;
        //rb.useGravity=false;
        rb.freezeRotation = true;
    }
    public Dictionary<string, float> UserScores;

    public PlayerScoreData[] playerScoreData=new PlayerScoreData[5];
    
    [System.Serializable]
    public class PlayerScoreData
    {
        public string playerTag="1";
        public float Score=0;
    }
}
