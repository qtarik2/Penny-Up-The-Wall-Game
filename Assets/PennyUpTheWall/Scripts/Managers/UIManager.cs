using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> Panels = new List<GameObject>();
    [SerializeField] public GameObject _speedoMeter;
    [SerializeField] public GameObject _DragSpeed;
    [SerializeField] public GameObject _WindDialPanel;
    [SerializeField] public GameObject _PlayerBossImagePanel;
    [SerializeField] public GameObject _HandSelectionPanel;
    [SerializeField] public Image PennyImage;
    [SerializeField] public RectTransform _CanvasRef;
    [SerializeField] public Camera _mainCamera;
    [SerializeField] public Image _swipeHand;
    [SerializeField] public Image userImage, bossImage;
    [SerializeField] public GameObject Boss_text;
    [SerializeField] public GameObject Won_Particle;
    [SerializeField] public AudioSource SoundDropped;
    public Button RestartButton, RematchButton, PauseButton, nextButton;
    [SerializeField] soundsHandler SoundsHandler;
    public GameObject boss;
    public Text logText;
    public static UIManager instance;
    #region Singleton

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    void Start()
    {
        RestartButton.onClick.AddListener(RestartButtonClicked);
        PennyImage.gameObject.SetActive(false);
        if (PlayerPrefs.GetString("Handed") != "Left" && PlayerPrefs.GetString("Handed") != "Right")
        {
            PlayerPrefs.SetInt("sounds", 1);
            Panels[4].gameObject.transform.GetChild(1).GetComponent<Button>().interactable = false;
            Spawner.instance.enabled = false;
            StartCoroutine(wait_in_start());
        }
        else
        {
            GameObject All_Player_Side = GameObject.FindGameObjectWithTag("All_Players");
            if (PlayerPrefs.GetString("Handed") == "Left")
            {
                All_Player_Side.transform.localScale = new Vector3(-1f, 1f, 1f);
                GameObject.FindGameObjectWithTag("All_Players").transform.localScale = All_Player_Side.transform.localScale;
            }
            else
           if (PlayerPrefs.GetString("Handed") == "Right")
            {
                All_Player_Side.transform.localScale = new Vector3(1f, 1f, 1f);
                GameObject.FindGameObjectWithTag("All_Players").transform.localScale = All_Player_Side.transform.localScale;

            }
            SetActivePanel(4);
            if (PlayerPrefs.GetString("gander") == "male")
            {
                GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(1).gameObject.SetActive(false);

            }
            else
          if (PlayerPrefs.GetString("gander") == "female")
            {
                GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        StartCoroutine(CheckForActivePanel());
    }
    IEnumerator wait_in_start()
    {
        yield return new WaitForSeconds(2f);
        _HandSelectionPanel.SetActive(true);
        Panels[4].gameObject.transform.GetChild(1).GetComponent<Button>().interactable = true;
    }
    float _timer = 300f;
    bool _handActive = false;
    private void Update()
    {
        if (GameManager.instance.gameStates == GameStates.Playing)
        {
            if (!Input.GetMouseButton(0) && !_handActive)
            {
                _timer -= 1f;

            }
            if (Input.GetMouseButton(0) && !_handActive)
            {
                _timer = 300f;
            }
            if (Input.GetMouseButton(0) && _handActive)
            {
                _timer = 300f;
                _swipeHand.gameObject.SetActive(false);
            }
            if (_timer <= 0 && !_handActive)
            {
                _handActive = true;
                if (PennyController.instance.playe_state == state.boss)
                {
                    _swipeHand.gameObject.SetActive(false);/*_handActive = true;*/
                }
                else
                {
                    _swipeHand.gameObject.SetActive(true);
                }
                Invoke("invokeFunc", 10f);
            }
        }
    }

    public void invokeFunc()
    {
        _timer = 300f;
        _handActive = false;
        _swipeHand.gameObject.SetActive(false);
    }
    #region Check Panel Coroutine
    IEnumerator CheckForActivePanel()
    {
        while (Panels.Any(obj => obj.activeInHierarchy == true))
        {
            yield return new WaitForSeconds(1f);

            GameManager.instance.gameStates = GameStates.IgnoreTouches;
            yield return null;
        }
        GameManager.instance.gameStates = GameStates.Playing;
        yield return new WaitForSeconds(1f);
        StartCoroutine(CheckForActivePanel());
    }

    #endregion

    public void SetActivePanel(int _panelNo)
    {
        GameObject panel = Panels[_panelNo];
        panel.SetActive(true);
        Panels.Where(t => t != null && !t.Equals(panel)).ToList().ForEach(g => g.SetActive(false));
    }

    public void SetDeActivatePanel(int paneNo)
    {
        GameObject _panel = Panels[paneNo];
        _panel.SetActive(false);
        Panels.Where(t => t != null && !t.Equals(_panel)).ToList().ForEach(g => g.SetActive(false));

    }
    #region Main Menu Functions
    public void PlayClicked()
    {
        GameManager.instance.selectedMatchMode = MatchMode.Campaign;
        SetActivePanel(2);
    }
    public GameObject CreateMatchPanel;
    public void QuickMatchClicked()
    {
        GameManager.instance.selectedMatchMode = MatchMode.Multiplayer;
        GameManager.instance.SelectedLevel=Random.Range(1,15);
        SceneManager.LoadScene("UserAuth", LoadSceneMode.Additive);
        //SetActivePanel(13);
    }
    public void PrivateMatchClicked()
    {
        GameManager.instance.selectedMatchMode=MatchMode.Private;
        SceneManager.LoadScene("UserAuth", LoadSceneMode.Additive);
        //SetActivePanel(14);
    }
    public void OnLevelSelected(int levelnumber)
    {   
        //GameManager.instance.SelectedLevel = levelnumber;
        GameManager.instance.SelectedLevel = Random.Range(1,15);
        SceneManager.LoadScene("UserAuth", LoadSceneMode.Additive);
    }
    public void OnPrivatLevelSelected(int levelnumber)
    {
       // GameManager.instance.SelectedLevel = levelnumber;
        SceneManager.LoadScene("UserAuth", LoadSceneMode.Additive);
    }
    public GameObject noInternetPanel;
    public void OnFriendsClicked()
    {
        if(Application.internetReachability==NetworkReachability.NotReachable)
        {
            noInternetPanel.SetActive(true);
            return;
        }
        SceneManager.LoadScene("UserAuth",LoadSceneMode.Additive);
    }
    public void RestartButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
    public void ResumeClicked()
    {
        SetDeActivatePanel(8);
        Time.timeScale = 1;
    }
    public void ExitButtonClicked()
    {
        _WindDialPanel.SetActive(false);
        _PlayerBossImagePanel.SetActive(false);
        Time.timeScale = 1;
        BossAI.instance.boss_count = 0;
        BossAI.instance.user_count = 0;
        BossAI.instance.Total_count = 0;
        PennyController.instance.For_exit();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SetActivePanel(4);
    }
    public void RateUsClicked()
    {
#if UNITY_EDITOR || UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
#else
      //  Application.OpenURL("market://details?id=" + RateGameSettings.googlePlayBundleID);
#endif
    }
    #endregion Main Menu Function
    public void cross(int number)
    {
        ResultController.instance.Reset();
        LastPointPulling.instance.DeleteOldPoints();
        ScoreBoard.instance.ResetScore();
        _swipeHand.gameObject.SetActive(false);
        BossAI.instance.boss_count = 0;
        BossAI.instance.user_count = 0;
        BossAI.instance.Total_count = 0;
        UIManager.instance.PauseButton.enabled = true;
        SetDeActivatePanel(number);
        b1.Check_For_Coins();
    }
    public BossesData _bossesData;
    public LoadBossData b1;
    public int count = 1;
    public void next(int number)
    {
        LevelSelection.instance.scaled = false;
        LastPointPulling.instance.DeleteOldPoints();
        ResultController.instance.Reset();
        ScoreBoard.instance.ResetScore();
        bool check = false;
        var levelno = LevelSelection._clickedLevelNo;
        _swipeHand.gameObject.SetActive(false);
        BossAI.instance.boss_count = 0;
        BossAI.instance.user_count = 0;
        BossAI.instance.Total_count = 0;
        PauseButton.enabled = true;
        if (levelno == 14 && _bossesData._totalBossesLevel[levelno]._UnlockedBossNo == 4)
        {
            check = true;
        }
        if (_bossesData._totalBossesLevel[levelno]._UnlockedBossNo < _bossesData._totalBossesLevel[levelno]._eachLevelBosses.Count - 1)
        {
            FindObjectOfType<LoadObstacles>().DestroyObstacle();
            count = _bossesData._totalBossesLevel[levelno]._UnlockedBossNo;
            count += 1;
            _bossesData._totalBossesLevel[levelno]._UnlockedBossNo = count;
            FindObjectOfType<LoadObstacles>().LoadLevelObstacles();
            if (count == 4)
            { RematchButton.gameObject.SetActive(true); }
        }
        else if (check)
        {
            SetActivePanel(4);
            LevelSelection.instance._allScenes[levelno].SetActive(false);
            _WindDialPanel.SetActive(false);
            _PlayerBossImagePanel.SetActive(false);
            b1.LoadBosses();
        }
        else
        {
            levelno++;
            LevelSelection._clickedLevelNo += 1;
            LevelSelection.instance.UnlockNewLevel(levelno);
            print("level number    " + LevelSelection._clickedLevelNo);
            count = _bossesData._totalBossesLevel[levelno]._UnlockedBossNo;
            _bossesData._totalBossesLevel[levelno]._UnlockedBossNo = count;
            PlayerPrefs.SetInt("UnlockedBossNo", count);
            FindObjectOfType<LoadObstacles>().LoadLevelObstacles();
            soundsHandler.instance.EnvironmentSound();
        }
        if (!check)
        {
            print("\nfun Is Running Of Level Active Panel");
            SetDeActivatePanel(number);
            print("level increes");
            BossAI.instance.boss_count = 0;
            BossAI.instance.user_count = 0;
            BossAI.instance.Total_count = 0;
            SetActivePanel(3);
            b1.LoadBosses();
            Invoke("deactivateBossesPanel", 2f);
            LevelSelection.instance.SetEnvironmentActive(levelno);
        }
        else
        {
            LevelSelection._clickedLevelNo = 0;
            _bossesData._totalBossesLevel[LevelSelection._clickedLevelNo]._UnlockedBossNo = 0;
            b1.LoadBosses();
        }
    }
    public void deactivateBossesPanel()
    {
        SetDeActivatePanel(3);
        b1.Check_For_Coins();
    }
    public void Boss_hand_texture_function()
    {
        b1.load_boss_hand_texture_function();
    }
    public void Pause_function()
    {
        Time.timeScale = 0;
    }
    public void Vibrate_on_Off(int value)
    {
        if (value == 1)
        {
            PlayerPrefs.SetInt("Vibration", value);
            //Handheld.Vibrate();
        }
        else PlayerPrefs.SetInt("Vibration", value);
    }
    public void Music_on_Off(int value)
    {
        if (value == 1)
        {
            PlayerPrefs.SetInt("Music", value);
            SoundsHandler.play_stop(value);
        }
        else
        {
            PlayerPrefs.SetInt("Music", value);
            SoundsHandler.play_stop(value);
        }
    }
    public void sounds_on_Off(int value)
    {
        if (value == 1)
        {
            PlayerPrefs.SetInt("sounds", value);
            SoundsHandler.play_stop(value);
        }
        else
        {
            PlayerPrefs.SetInt("sounds", value);
            SoundsHandler.play_stop(value);
        }
    }
    public void main_soundButton(bool clicked)
    {
        if (clicked == true)
        {
            sounds_on_Off(1);
            Music_on_Off(1);
        }
        else if (clicked == false)
        {
            sounds_on_Off(1);
            Music_on_Off(1);
        }
    }
    public void ReMatch_Function()
    {
        _PlayerBossImagePanel.SetActive(true);
        _WindDialPanel.SetActive(true);
        Time.timeScale = 1;
        BossAI.instance.boss_count = 0;
        BossAI.instance.user_count = 0;
        BossAI.instance.Total_count = 0;
        FindObjectOfType<PennyController>().mouse_down = true;
    }
    public IEnumerator Play_stop_Particles()
    {
        GameManager.instance.gameStates = GameStates.IgnoreTouches;
        PennyController.instance.playe_state = state.boss;
        _timer = 300f;
        _handActive = false;
        _swipeHand.gameObject.SetActive(false);
        SetActivePanel(10);
        Won_Particle.SetActive(true);
        LevelSelection.instance.wonPanel.SetActive(true);
        LevelSelection.instance.wonPanel.GetComponent<Animator>().SetBool("Add", true);
        yield return new WaitForSeconds(3f);
        Won_Particle.SetActive(false);
        LevelSelection.instance.wonPanel.SetActive(false);
        PennyController.instance.playe_state = state.user;
        if (LevelSelection.instance.isClickN == false) { LevelSelection.instance.scaled = true; }
    }
    public void OnLogoutClick()
    {
        GameManager.instance.isLogout=true;
        PlayFab.PlayFabClientAPI.ForgetAllCredentials();
        PlayerPrefs.DeleteKey("Email");
        PlayerPrefs.DeleteKey("AccountType");
    }
    public void OnOffLogoutButton(GameObject btn)
    {
       // btn.SetActive(false);
        btn.SetActive(PlayerPrefs.HasKey("Email"));
    }

}
