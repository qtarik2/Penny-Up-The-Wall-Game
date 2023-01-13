using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// This script attached to Bosses popup Panel
/// </summary>
public class LoadBossData : MonoBehaviour
{
    [SerializeField] public BossesData _bossesData;
    [SerializeField] public Image UserImage;
    [SerializeField] public Image BossImage, BossImageForTurn;
    [SerializeField] public Image[] AllBossImage;
    [SerializeField] public TextMeshProUGUI UserName;
    [SerializeField] public TextMeshProUGUI BossName;
    [SerializeField] private WindZone Wind;
    public float lenght_of_Dialogues;
    public static LoadBossData instance;
    public GameObject maleArm;
    public GameObject femaleArm;

    //for hand texture of bosses load
    public int _level_Number, _Boss_number;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        //    PlayerPrefs.DeleteAll();
        // UnlockNewBoss(1, 6);
        //Load boss in start
        LoadBosses();
    }
    //For unlocking new boos pass level number and new boss number in this function
    public void UnlockNewBoss(int levelNo, int bossNo)
    {
        //Set unlocked boss number in boss data asset file
        if ((bossNo - 1) <= _bossesData._totalBossesLevel[levelNo]._eachLevelBosses.Count - 1)
        {
            _bossesData._totalBossesLevel[levelNo]._UnlockedBossNo = bossNo - 1;
        }
        else
        {
        }
        //Unlock new level
        LevelSelection.instance.UnlockNewLevel(levelNo);
    }
    //Load boss data in game start
    public void LoadBosses()
    {
        #region Loading each Level Bossed Dat
        //Save temporary current level number for later use 
        //  var CurrentLevelNo = PlayerPrefs.GetInt("LevelNumber");
        var CurrentLevelNo = LevelSelection._clickedLevelNo;
        _level_Number = CurrentLevelNo;
        //Save temporary current level boss number for later use
        var CurrentBossNo = _bossesData._totalBossesLevel[CurrentLevelNo]._UnlockedBossNo;
        _Boss_number = CurrentBossNo;
        //Boss Image In For dialoge panel show
        PowerBar.instance.bossNow = CurrentBossNo;
        LevelSelection.instance.boss_Now.text = "Boss : " + (CurrentBossNo + 1);
        DialogueManager.instance.bossnow = CurrentBossNo;
        DialogueManager.instance.levelnow = _level_Number;
        LevelSelection.instance.level_Now.text = LevelSelection.instance._levelsData._LevelData[_level_Number].LevelName;
        if (LevelSelection.instance.nowModel != null) { Destroy(LevelSelection.instance.nowModel.gameObject); }
        if (_bossesData._totalBossesLevel[CurrentLevelNo]._eachLevelBosses[CurrentBossNo].BosseModel != null) { LevelSelection.instance.nowModel = Instantiate(_bossesData._totalBossesLevel[CurrentLevelNo]._eachLevelBosses[CurrentBossNo].BosseModel, LevelSelection.instance.All_Character.transform); }
        //  var CurrentBossNo = PlayerPrefs.GetInt("UnlockedBossNo");
        for (int i = 0; i <= 4; i++)
        {
            AllBossImage[i].sprite = _bossesData._totalBossesLevel[CurrentLevelNo]._eachLevelBosses[i].BossImage;
            if (i <= CurrentBossNo)
            {
                /*  Transform locked_img;
                  locked_img = AllBossImage[i].transform.GetChild(0);
                  locked_img.gameObject.SetActive(false);
              */
                AllBossImage[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            else AllBossImage[i].transform.GetChild(0).gameObject.SetActive(true);
        }
        //Check if currentboss number is less than that level bosses length
        if (CurrentBossNo <= _bossesData._totalBossesLevel[CurrentLevelNo]._eachLevelBosses.Count - 1)
        {
            //Get boss name from boss asset data file and show on boss popup panel
            BossName.text = _bossesData._totalBossesLevel[CurrentLevelNo]._eachLevelBosses[CurrentBossNo].BossName;
            //Get boss image from boss asset data file and show on boss popup panel
            BossImage.sprite = _bossesData._totalBossesLevel[CurrentLevelNo]._eachLevelBosses[CurrentBossNo].BossImage;
            BossImageForTurn.sprite = _bossesData._totalBossesLevel[CurrentLevelNo]._eachLevelBosses[CurrentBossNo].BossImage;
        }
        else
        {

        }
        Wind.windTurbulence = 1 + CurrentBossNo;
        #endregion
    }
    public void load_boss_hand_texture_function()
    {
        if (_bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].gander == false)
        {
            BossAI.instance.Boos_Gander = false;
            GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(1).gameObject.SetActive(false);


            //  GameObject maleArm = UIManager.instance.maleArm;
            GameObject maleArmMesh = maleArm.transform.GetChild(1).gameObject;
            // maleArmMesh.GetComponent<SkinnedMeshRenderer>().material = m89;
            //   maleArmMesh.GetComponent<SkinnedMeshRenderer>().material = _bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].Hand_material_of_boss[0];
            maleArmMesh.GetComponent<SkinnedMeshRenderer>().materials = _bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].Hand_material_of_boss;

            // maleArm.GetComponent<SkinnedMeshRenderer>().materials[0] = _bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].Hand_material_of_boss[0];
            // maleArm.GetComponent<SkinnedMeshRenderer>().materials[1] = _bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].Hand_material_of_boss[1];
            /*  PennyController.instance.Hand.GetComponent<SkinnedMeshRenderer>().materials[0] = _bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].Hand_material_of_boss[0];
              PennyController.instance.Hand.GetComponent<SkinnedMeshRenderer>().materials[1] = _bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].Hand_material_of_boss[1];
            */
        }
        else
        {
            BossAI.instance.Boos_Gander = true;
            GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(1).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = _bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].Hand_material_of_boss[0];
        }
    }
    public int index_of_dialog = 0;
    bool Complete = true;
    public void Boss_load_Dailogue_fun()
    {
        /* for (int i=0;i< _bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].BossDialogue.Length;i++) {
             DialogueManager.instance.LoadDialogue(_bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].BossDialogue[i]);
             StartCoroutine(Wait());
         }*/
        lenght_of_Dialogues = _bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].BossDialogue.Length;

        if (index_of_dialog < _bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].BossDialogue.Length)
        {
            //StartCoroutine(Wait());
            DialogueManager.instance.show(true);
            if (index_of_dialog == 0)
            {
                index_of_dialog++;
                Invoke("Wait", 4.5f);
                Complete = false;
            }
            else
            {
                DialogueManager.instance.LoadDialogue(_bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].BossDialogue[index_of_dialog]);
                index_of_dialog++;
                Invoke("Wait", 2.5f);
            }
        }
        else
        {
            index_of_dialog = 0;
            Complete = true;
            DialogueManager.instance.Dialogue.text = "";
            // DialogueManager.instance.show(false);
            return;
        }
    }
    void Wait()
    {
        Boss_load_Dailogue_fun();
    }
    public void SideAnimetionPlay(string Dialog)
    {
        DialogueManager.instance.AnimationTrue(_bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].AnimationSide, Dialog);
    }
    public void SideAnimetionPlayFalse()
    {
        DialogueManager.instance.AnimationFasle(_bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].AnimationSide);

    }
    //If player won the match the coins given fun
    public void Bet_won()
    {
        ScoreBoard.instance.winPng.SetActive(true);
        CoinManager.instance.AddCoins((_bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].bet * 2));
        GameManager.instance.gameStates = GameStates.IgnoreTouches;
    }
    public void Bet_lose()
    {
        ScoreBoard.instance.losePng.SetActive(true);
        CoinManager.instance.HasEnoughCoins(_bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].bet);
    }
    public void Check_For_Coins()
    {
        if (CoinManager.instance.HasEnoughCoins_yes_No(_bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].bet) == false)
        {
            PennyController.instance.mouse_down = false;
            return;
        }
        else
        {
            CoinManager.instance.SubtractCoins(_bossesData._totalBossesLevel[_level_Number]._eachLevelBosses[_Boss_number].bet);
            PennyController.instance.mouse_down = true;
        }
    }
}
