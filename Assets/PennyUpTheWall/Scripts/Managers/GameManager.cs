using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager instance;
    public MatchMode selectedMatchMode;
    public int SelectedLevel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion
    public bool isLogout;
    public bool isSinglePlayer = false;
    public bool autoLogin;
    public int totalRound = 3;
    public byte requiredPlayers = 2;
    public byte maxPlayers = 5;
    public int matchStartTime = 50;
    public GameStates gameStates = GameStates.IgnoreTouches;

    #region Fields For BirdController
    //Access Birds Controller 
    [HideInInspector] public lb_BirdController birdControl;

    #endregion
    public void LoadSceneByName(string name, bool aysncLoad, bool isLoad)
    {
        print("scene manager");
        if (!isLoad)
        {
            SceneManager.UnloadSceneAsync(name);
        }
        else if (aysncLoad)
        {
            SceneManager.LoadSceneAsync(name);
        }
        else
        {
            SceneManager.LoadScene(name);
        }
    }

    void Start()
    {
        //Set game start in start mode so 

        #region BirdControl
        //Find and assign bird controller gameobject from scene
        //birdControl = GameObject.Find("FlyingBirdsController").GetComponent<lb_BirdController>();
        //Spawn birds in scene
        //StartCoroutine(SpawnSomeBirds());
        #endregion
    }
    public void OnApplicationQuit()
    {
        print("quit game");
        if (Photon.Pun.PhotonNetwork.InRoom)
        {
            print("quit room");
            Photon.Pun.PhotonNetwork.LeaveRoom();
        }
        if (Photon.Pun.PhotonNetwork.IsConnected)
        {
            Photon.Pun.PhotonNetwork.Disconnect();
        }
    }

    //Use this function to spawn birds in game scene
    IEnumerator SpawnSomeBirds()
    {
        yield return 2;
        birdControl.SendMessage("SpawnAmount", 10);
    }


    private void Update()
    {
        //Check if Game state is null or Playing set time to 0f otherwise set time to 1 for all other states
        //if(gameStates == GameStates.IgnoreTouches || gameStates == GameStates.Playing)
        //{
        //    Time.timeScale = 1f;
        //}
        //else
        //{
        //    Time.timeScale = 0f;

        //    //Set audioSource pause state true if you want to listen music even game in pause state 
        //    //audioSource.ignoreListenerPause = true;
        //   // audioSource.ignoreListenerVolume = true;
        //}
    }
}

[System.Serializable]
public enum GameStates
{
    IgnoreTouches,//Set state Start mode when game home menu is open and we dont want to count touches on penny
    Playing,//Set state Playing mode when user is playing game in game play scene
    Paused,//Set state paused when user open any panel
    Failed,//Set state failed to get if user is failed level or not
    Complete//Set state Complete when user successfully win the level
}
public enum MatchMode
{
    Campaign,
    Multiplayer,
    Private
}
public enum state
{
    user,
    boss
}

