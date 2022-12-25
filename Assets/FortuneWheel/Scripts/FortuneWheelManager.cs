using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using TMPro;
public class FortuneWheelManager : MonoBehaviour
{
    [SerializeField] GameObject[] Lights;
    private bool _isStarted;

    private static DateTime dBonusAvailedTime;
    private float[] _sectorsAngles;
    private float _finalAngle;
    private float _startAngle = 0;
    private float _currentLerpRotationTime;
    public Button TurnButton;
    public GameObject Circle, Coin_Animator; 			// Rotatable Object with rewards
    //public Text CoinsDeltaText; 		// Pop-up text with wasted or rewarded coins amount
    //public Text CurrentCoinsText; 		// Pop-up text with wasted or rewarded coins amount
    //public int TurnCost = 300;			// How much coins user waste when turn whe wheel
    //public int CurrentCoinsAmount = 1000;	// Started coins amount. In your project it can be set up from CoinsManager or from PlayerPrefs and so on
    //public int PreviousCoinsAmount;		// For wasted coins animation'
    public GameObject DailogueBox;
    bool stop=true;
    [SerializeField] TextMeshProUGUI CoinText_PopoUp; //For Coin Text
    int Coins = 0;
    private void Awake ()
    {
        TurnButton.interactable = true;
        //PreviousCoinsAmount = CurrentCoinsAmount;
        //CurrentCoinsText.text = CurrentCoinsAmount.ToString ();

        //int diff = DateTime.Now.Hour - GetAvailedTime();
        //if (diff < 4)
        //{
        //    TurnButton.interactable = false;
        //}
    }

    public void TurnWheel ()
    {
        // Player has enough money to turn the wheel
        //if (CurrentCoinsAmount >= TurnCost) {
        stop = true;
        _currentLerpRotationTime = 0f;
    	
    	    // Fill the necessary angles (for example if you want to have 12 sectors you need to fill the angles with 30 degrees step)
    	    _sectorsAngles = new float[] { 45, 90, 135, 180, 225, 270, 315, 360 };
    	
    	    int fullCircles = 5;
    	    float randomFinalAngle = _sectorsAngles [UnityEngine.Random.Range (0, _sectorsAngles.Length)];
    	
    	    // Here we set up how many circles our wheel should rotate before stop
    	    _finalAngle = -(fullCircles * 360 + randomFinalAngle);
    	    _isStarted = true;


        InvokeRepeating(nameof(ActivateRandomObject), 0.1f, 0.2f);
    	    //PreviousCoinsAmount = CurrentCoinsAmount;
    	
    	    // Decrease money for the turn
    	    //CurrentCoinsAmount -= TurnCost;
    	
    	    // Show wasted coins
    	    //CoinsDeltaText.text = "-" + TurnCost;
    	   // CoinsDeltaText.gameObject.SetActive (true);
    	
    	    // Animate coins
    	    //StartCoroutine (HideCoinsDelta ());
    	    //StartCoroutine (UpdateCoinsAmount ());
    	//}
    }

    private void GiveAwardByAngle ()
    {
        
        // Here you can set up rewards for every sector of wheel
        switch ((int)_startAngle) {
    	case 0:
                //    RewardCoins (1000);
                Debug.Log(200);
                Coins = 200;
                CoinText_PopoUp.text = Coins.ToString();
                
            break;
    	case -315:
                Debug.Log(100);
                Coins = 100;
                CoinText_PopoUp.text = Coins.ToString();
                
                //RewardCoins (200);
                break;
    	case -270:
                Coins = 500;
                Debug.Log(500);
                CoinText_PopoUp.text = Coins.ToString();
                
                //RewardCoins (100);
                break;
    	case -225:
                Coins = 900;
                Debug.Log(900);
                CoinText_PopoUp.text = Coins.ToString();
                
                //RewardCoins (500);
                break;
    	case -180:
                Coins = 200;
                Debug.Log(200);
                CoinText_PopoUp.text = Coins.ToString();
                
                //RewardCoins (300);
                break;
    	case -135:
                Coins = 700;
                Debug.Log(700);
                CoinText_PopoUp.text = Coins.ToString();
                
                //RewardCoins (100);
                break;
    	case -90:
                Coins = 300;
                Debug.Log(300);
                CoinText_PopoUp.text = Coins.ToString();
           
                //RewardCoins (900);
                break;
    	case -45:
                Coins = 1000;
                Debug.Log(1000);
                CoinText_PopoUp.text = Coins.ToString();
                
                //RewardCoins (200);
                break;
    	
    	default:
                Coins = 0;
                Debug.Log(0);
                //RewardCoins (300);
    	    break;
        }
        StartCoroutine(PopoUp_Value());
    }
    IEnumerator PopoUp_Value()
    {
        Coin_Animator.SetActive(true);
        Coin_Animator.GetComponent<Animator>().SetBool("Add",true);
        CoinText_PopoUp.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        CoinText_PopoUp.gameObject.SetActive(false);
        Coin_Animator.GetComponent<Animator>().SetBool("Add", false);
        CoinManager.instance.AddCoins(Coins);
        Coin_Animator.SetActive(false);
        TurnButton.interactable = false;
       // SetAvailedTime();
    }

    void SetAvailedTime()
    {
        dBonusAvailedTime = DateTime.Now;
        PlayerPrefs.SetInt("dbonushours", dBonusAvailedTime.Hour);
        PlayerPrefs.Save();
    }

    //public int GetAvailedTime()
    //{
    //   // return PlayerPrefs.GetInt("dbonushours", 0);
        
    //}

    void Update ()
    {
        // Make turn button non interactable if user has not enough money for the turn
        //if (_isStarted || CurrentCoinsAmount < TurnCost) {
        if (_isStarted )
        {
            TurnButton.interactable = false;
    	    //TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
    	} else {
    	   // TurnButton.interactable = true;
    	    //TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 1);
    	}

    	if (!_isStarted)
    	    return;
        if (stop == true)
        {
            float maxLerpRotationTime = 4f;

            // increment timer once per frame
            _currentLerpRotationTime += Time.deltaTime;
            if (_currentLerpRotationTime > maxLerpRotationTime || Circle.transform.eulerAngles.z == _finalAngle)
            {
                _currentLerpRotationTime = maxLerpRotationTime;
                _isStarted = false;
                _startAngle = _finalAngle % 360;

                GiveAwardByAngle();
                //StartCoroutine(HideCoinsDelta ());
            }

            // Calculate current position using linear interpolation
            float t = _currentLerpRotationTime / maxLerpRotationTime;

            // This formulae allows to speed up at start and speed down at the end of rotation.
            // Try to change this values to customize the speed
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            float angle = Mathf.Lerp(_startAngle, _finalAngle, t);
            Circle.transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    GameObject selection;
    public void ActivateRandomObject()
    {
        selection = Lights
        .Where(i => !i.activeSelf)
        .OrderBy(n => UnityEngine.Random.Range(0,Lights.Length)).FirstOrDefault();
        Debug.Log("Slection object value "+ selection);
        // selection will be null if all game objects are already active
        if (selection != null) selection.SetActive(true);
        Invoke(nameof(ActiveFalseLight), 0.1f);
        //  yield return new WaitForSeconds(.1f);
    }

    public void ActiveFalseLight()
    {
        selection.SetActive(false);

    }

    public void OnClickStopCoroutine()
    {
        print("Angle_before "+Circle.transform.eulerAngles.z);
        //   Circle.transform.Rotate(0.0f, 0.0f,0.0f);
        Circle.transform.eulerAngles = new Vector3(0, 0, 0);
        print("Angle_after" + Circle.transform.eulerAngles.z);
        CancelInvoke();
        stop = false;
        _isStarted = false;
        _currentLerpRotationTime = 0f;
        CoinText_PopoUp.gameObject.SetActive(false);
        // TurnButton.interactable = true;
    }

    //private void RewardCoins (int awardCoins)
    //{
    //    CurrentCoinsAmount += awardCoins;
    //    //DailogueBox.SetActive(true);
    //    //CoinsDeltaText.text = "+" + awardCoins;
    //    // CoinsDeltaText.gameObject.SetActive (true);
    //    //StartCoroutine (UpdateCoinsAmount ());
    //    StartCoroutine(UpdateDeltaCoin(awardCoins));
    //}

    //   private IEnumerator HideCoinsDelta ()
    //   {
    //       yield return new WaitForSeconds (1f);
    //CoinsDeltaText.gameObject.SetActive (false);
    //   }

    //private IEnumerator UpdateCoinsAmount ()
    //{
    //	// Animation for increasing and decreasing of coins amount
    //	const float seconds = 0.5f;
    //	float elapsedTime = 0;

    //	while (elapsedTime < seconds) {
    //	    //CurrentCoinsText.text = Mathf.Floor(Mathf.Lerp (PreviousCoinsAmount, CurrentCoinsAmount, (elapsedTime / seconds))).ToString ();
    //	    elapsedTime += Time.deltaTime;

    //	    yield return new WaitForEndOfFrame ();
    //    }

    //	PreviousCoinsAmount = CurrentCoinsAmount;
    //	//CurrentCoinsText.text = CurrentCoinsAmount.ToString ();
    //}

    //private IEnumerator UpdateDeltaCoin(int coinAmount)
    //{
    //    // Animation for increasing and decreasing of coins amount
    //    const float seconds = 0.5f;
    //    float elapsedTime = 0;

    //    while (elapsedTime < seconds)
    //    {
    //        //CoinsDeltaText.text = Mathf.Floor(Mathf.Lerp(1, coinAmount, (elapsedTime / seconds))).ToString();
    //        elapsedTime += Time.deltaTime;

    //        yield return new WaitForEndOfFrame();
    //    }

    //    //CurrentCoinsText.text = coinAmount.ToString();
    //}

}
