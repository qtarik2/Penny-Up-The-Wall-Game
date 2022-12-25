using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
/// <summary>
/// This script attached to CoinManager in Scene
/// </summary>
public class CoinManager : MonoBehaviour {


	[SerializeField] TextMeshProUGUI CoinText, LevelSelection_CoinText,SettingPanel_CoinText,SpinWheelPanel_CoinText; //For Coin Text
    [SerializeField] GameObject DailogueBox; // For Not Enough Coin_Dailogue panel
    [SerializeField] NumberFormatter n; // For Not Enough Coin_Dailogue panel
    [SerializeField] GameObject Watch_Ad1_panel; // For Not Enough Coin_Dailogue panel
    int Coins =0;
    public bool isMainMenu;
    //**Call like this for adding coins
    //CoinManager.instance.AddCoins(100);
    //*** Call like this for subtract coins
    //if(CoinManager.instance.HasEnoughCoins(100)){
    //CoinManager.instance.SubtractCoins(100);}

    #region Singleton
    public static CoinManager instance { get; set; }

    private void Awake()
	{
		instance = this;
        Coins = PlayerPrefs.GetInt("Coins");
       
    }
    #endregion

    public void Start()
    {
        //if(PlayerPrefs.GetInt("FirstTimePlayed") != 1)
        //{
        //    PlayerPrefs.SetInt("FirstTimePlayed", 1);
        //    PlayerPrefs.SetInt("Coins", 500);
        //}

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 100000);

        if (isMainMenu) { UpdateCoinsText(); }
    }
    public void AddCoins(int addValue)
    {

        Coins += addValue;
        PlayerPrefs.SetInt("Coins",Coins);
        UpdateCoinsText();
    }

    public void SubtractCoins(int value)
    {
        if (Coins >= value)
        {
            Coins -= value;
            PlayerPrefs.SetInt("Coins", Coins);
            UpdateCoinsText();
        }
        else
            DailogueBox.gameObject.SetActive(true);
//        Debug.Log("Not have enough money");

    }
    public bool HasEnoughCoins_yes_No(int amount)
    {
        if (PlayerPrefs.GetInt("Coins") >= amount)
        {
            return true;
        }
        else
        {
            DailogueBox.gameObject.SetActive(true);
            Debug.Log("Not have enough money");
            return false;
        }
    }

    public bool HasEnoughCoins(int amount)
    {
        //   return (PlayerPrefs.GetInt("Coins") >= amount);
        if (PlayerPrefs.GetInt("Coins") >= amount)
        {

            Coins -= amount;
            PlayerPrefs.SetInt("Coins", Coins);
            UpdateCoinsText();
            return true;
        }
        else
        {
            DailogueBox.gameObject.SetActive(true);
            Debug.Log("Not have enough money");
            return false;
        }
            
    }
    public void UpdateCoinsText()
    {
        if (!isMainMenu)
            return;
        Coins = PlayerPrefs.GetInt("Coins");
        CoinText.text = n.numberFormat(Coins);
        LevelSelection_CoinText.text = CoinText.text;
        SettingPanel_CoinText.text = CoinText.text;
        SpinWheelPanel_CoinText.text = CoinText.text;
    }

    public void ResetCoins()
    {
        PlayerPrefs.DeleteKey("Coins");
    }
    public void on_cancel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void On_watch_Ad() {
       
        StartCoroutine(playAd());
        DailogueBox.SetActive(false);

    }
    IEnumerator playAd() {
        Watch_Ad1_panel.SetActive(true);
        yield return new WaitForSeconds(5f);
        AddCoins(500);
        Watch_Ad1_panel.SetActive(false);
        UIManager.instance.b1.Check_For_Coins();
        
    }
}
