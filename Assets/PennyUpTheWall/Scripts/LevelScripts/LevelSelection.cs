using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIExtensions;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Text.RegularExpressions;
/// <summary>
/// This Script is attached to LevelSelection Gameobject in Hierarchy
/// </summary>
public class LevelSelection : MonoBehaviour
{
    public bool isMainMenu;
    public bool isPlaying;
    public Transform[] birdPoints;
    [SerializeField] GameObject[] LevelButtons;//List of all level buttons in content child
    [SerializeField] GameObject[] QMLevelButtons;//List of all QM level buttons in content child
    [SerializeField] public LevelData _levelsData;    //Reference to scriptable data object (LevelData)
    [SerializeField] public BossesData _bossData;    //Reference to scriptable data object (LevelData)
    public bool canRotate = false;
    public GameObject All_Character;
    public GameObject Last_Character;
    [SerializeField] public List<GameObject> _allScenesPrefab = new List<GameObject>();//Array of All environments
    [SerializeField] public List<GameObject> _allScenesIns = new List<GameObject>();//Array of All environments
    [SerializeField] public List<GameObject> _allScenes = new List<GameObject>();//Array of All environments
    public Image coinIns;
    public Image coinInsB;
    public AudioSource s1;
    public AudioSource s2;
    public GameObject followPoint;
    public GameObject Congra;
    public GameObject losgra;
    public GameObject camF;
    public GameObject pausegra;
    public GameObject nowModel;
    public GameObject wonPanel;
    public Text boss_Now;
    public Text level_Now;
    public EnviroSkyLite SFX_Holder;
    public static int _clickedLevelNo;
    public bool isClickN;
    [SerializeField] TextMeshProUGUI Level_Text;
    public bool scaled;
    public CanvasGroup Waiting;

    public List<Texture2D> boss1;
    public string[] fileInfo;
    int inb = 0;

    //public Weather_sytem st;
    #region Singleton Region
    public static LevelSelection instance;
    private void Awake()
    {
        if (instance == null) { instance = this; }
    }
    #endregion

    void Start()
    {
        GameManager.instance.gameStates = GameStates.Playing;
        //Load Unlocked Level Data
        LoadUnlockedLevel();
        //Clear All level boss numbers
        ClearBossNumber();
        //For Level text in setting panel
        int level = 1 + PlayerPrefs.GetInt("LevelNumber");
        Level_Text.text = level.ToString();
        // set UI Price in multiplayer level selection 
        if (!isMainMenu) { SetQMLevelsCoin(); }
    }
    public void SetQMLevelsCoin()
    {
        for (int i = 0; i < QMLevelButtons.Length; i++)
        {
            int levelPrice = _levelsData._LevelData[i].Price;
            string levelPriceTxt = null;
            if (levelPrice >= 1000 && levelPrice <= 999999)
            {
                levelPriceTxt = (levelPrice / 1000).ToString("0") + "K";
            }
            else if (levelPrice >= 1000000 && levelPrice <= 999999999)
            {
                levelPriceTxt = (levelPrice / 1000000).ToString("0") + "M";
            }
            QMLevelButtons[i].transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Text>().text = levelPriceTxt;
        }
    }
    public void ClearBossNumber()
    {
        for (int i = 0; i < _bossData._totalBossesLevel.Count; i++)
        {
            _bossData._totalBossesLevel[i]._UnlockedBossNo = 0;
        }
    }
    //Unlock New Level
    public void UnlockNewLevel(int LevelNo)
    {
        print("UnLock Level FUN");
        //Check if current complete levelNo is greater than older Level number which is saved in LevelNumber Playerprefs
        if (LevelNo > PlayerPrefs.GetInt("LevelNumber") && LevelNo <= (LevelButtons.Length - 1))
        {
            PlayerPrefs.SetInt("LevelNumber", LevelNo);

            print("UnLock Function Is Called" + PlayerPrefs.GetInt("LevelNumber"));

            LoadUnlockedLevel();//Load all levels again after change in new value
        }
        else { return; }
    }
    public void LoadUnlockedLevel()
    {
        #region Loading Unlocked Level Data

#if UNITY_EDITOR
        //Set All levels unlocked for testing purpose on Editor
        //PlayerPrefs.SetInt("LevelNumber", LevelButtons.Length);
        //PlayerPrefs.SetInt("LevelNumber", 10);
#endif
        var currentLevelNo = PlayerPrefs.GetInt("LevelNumber");
        //Check if user play first time unlock first level everytime
        if (currentLevelNo == 0)
        {
            PlayerPrefs.SetInt("LevelNumber", 1);
            currentLevelNo = PlayerPrefs.GetInt("LevelNumber");
            currentLevelNo -= 1;
            PlayerPrefs.SetInt("LevelNumber", 0);
        }
        //Store current higher level number value in variable
        //Doing -1 because array is starting from 0 point and we need to equal our logic
        //Assign All LevelButtons to LevelButtons Array
        //LevelButtons = new GameObject[transform.childCount];
        for (int i = 0; i <= LevelButtons.Length - 1; i++)
        {

            //Assign all level buttons text value with Level number in start
            //LevelButtons[i] = transform.GetChild(i).gameObject;
            //LevelButtons[i].transform.GetChild(0).GetComponent<Text>().text = _levelsData._LevelData[i].LevelNumber.ToString();
            //Assign All Level Images from the LevelData scriptable object list
            LevelButtons[i].transform.GetComponent<Image>().sprite = _levelsData._LevelData[i].LevelImage;
            LevelButtons[i].transform.GetChild(3).GetComponent<Image>().sprite = _levelsData._LevelData[i].LevelNameImage;



            if (i <= currentLevelNo)
            {
                //LevelButtons[i].transform.GetChild(1).GetComponent<Image>().sprite = _levelsData._LevelData[i].UnlockImage;
                LevelButtons[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
                LevelButtons[i].transform.GetChild(2).GetComponent<Image>().enabled = false;     ///this is the level unlock comment

                LevelButtons[i].transform.GetComponent<Button>().interactable = true;
                LevelButtons[currentLevelNo].GetComponent<UIShiny>().Play();

            }
            else
            {
                // This is also the level unlocking comment
                //LevelButtons[i].transform.GetChild(1).GetComponent<Image>().sprite = _levelsData._LevelData[i].LockImage;
                // LevelButtons[i].transform.GetComponent<Button>().interactable = false;

                //Subhani::For testing only
                LevelButtons[i].transform.GetComponent<Button>().interactable = true;

            }

            //Subhani::For testing only

            LevelButtons[i].transform.GetChild(1).GetComponent<Image>().enabled = false;

        }


        #endregion
    }

    public void NextOrRematchClick()
    {
        isPlaying = true;
        OnLevelButtonClicks(0);
    }

    public void OnLevelButtonClicks(int _LevelNo)
    {
        if (isPlaying)
        {
            if (UIManager.instance.RematchButton.gameObject.activeInHierarchy == false)
                return;
        }

        if (isPlaying)
        {
            _LevelNo = DialogueManager.instance.levelnow + 1;
        }
        StartLevel(_LevelNo);
    }

    public void StartLevel(int _levelNo)
    {
        //LoadBossData.instance.LoadBosses();
        _allScenes.ForEach(a => Destroy(a));
        SetEnvironmentActive(_levelNo - 1);
        PlayerPrefs.SetInt("currentlevelno", _levelNo - 1);
        //u st.enabled = true;
        //Set Deactive Level selection Panel
        //UIManager.instance.SetDeActivatePanel(2);
        if (CoinManager.instance.HasEnoughCoins_yes_No(_bossData._totalBossesLevel[_clickedLevelNo]._eachLevelBosses[0].bet).Equals(false))
        {
            return;
        }
        //Set active Bosses Panel
        UIManager.instance.SetActivePanel(3);
        Invoke("deactivateBossesPanel", 2f);
        _clickedLevelNo = _levelNo - 1;
        LoadBossData.instance._level_Number = _clickedLevelNo;
        CoinManager.instance.SubtractCoins(_bossData._totalBossesLevel[_clickedLevelNo]._eachLevelBosses[0].bet);
        soundsHandler.instance.EnvironmentSound();
        //FindObjectOfType<LoadObstacles>().LoadLevelObstacles();
        coinCollect();
        LoadBossData.instance.LoadBosses();
    }

    public void coinCollect()
    {
        s1.Play();
        s2.Play();
        coinIns.GetComponent<Animator>().enabled = true;
        coinInsB.GetComponent<Animator>().enabled = true;
        StartCoroutine(StartCoinEffect());
        scaled = false;
        isClickN = true;
    }

    IEnumerator StartCoinEffect()
    {
        yield return new WaitForSeconds(1.5f);
        coinIns.GetComponent<Animator>().enabled = false;
        coinInsB.GetComponent<Animator>().enabled = false;
    }
    public void OnQMLevelButtonClicks(int _LevelNo)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            UIManager.instance.noInternetPanel.SetActive(true);
            return;
        }
        // SetEnvironmentActive(_LevelNo - 1);
        PlayerPrefs.SetInt("currentlevelno", _LevelNo - 1);
        if (CoinManager.instance.HasEnoughCoins(_levelsData._LevelData[_LevelNo - 1].Price))
        {
            UIManager.instance.OnLevelSelected(_LevelNo);
        }
    }
    public void OnPrivatLevelSelectionClicked(int _LevelNo)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            UIManager.instance.noInternetPanel.SetActive(true);
            return;
        }
        // SetEnvironmentActive(_LevelNo - 1);
        UIManager.instance.OnPrivatLevelSelected(_LevelNo);

    }

    public void deactivateBossesPanel()
    {
        UIManager.instance.SetDeActivatePanel(3);
        UIManager.instance._WindDialPanel.SetActive(true);
        UIManager.instance._PlayerBossImagePanel.SetActive(true);
    }

    private void Update()
    {
        if (!isMainMenu)
        {
            if (GameManager.instance.gameStates == GameStates.Playing) { SFX_Holder.Audio.weatherSFXVolume = 1f; SFX_Holder.Audio.ambientSFXVolume = .5f; }
            else { if (SFX_Holder != null) { SFX_Holder.Audio.weatherSFXVolume = 0f; SFX_Holder.Audio.ambientSFXVolume = 0f; } }
        }

        if (losgra != null)
        {
            if (scaled || losgra.activeInHierarchy == true || pausegra.activeInHierarchy == true)
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
            }
        }

        else { Time.timeScale = 1; AudioListener.pause = false; }
    }
    public void SetEnvironmentActive(int _sceneNo)
    {
        _allScenes.ForEach(a => Destroy(a));
        // Debug.Log("Active scene name: " + _allScenes[_sceneNo].gameObject.name);
        GameObject ins = Instantiate(_allScenesPrefab[_sceneNo], _allScenesIns[_sceneNo].transform);
        _allScenes[_sceneNo] = ins;
        // _allScenes.Where(t => t != null && !t.Equals(_sceneNo)).ToList().ForEach(g => g.SetActive(false));
        for (int i = 0; i < _allScenesPrefab.Count; i++)
        {
            if (i != _sceneNo) { Destroy(_allScenes[i]); }
        }
    }

}

