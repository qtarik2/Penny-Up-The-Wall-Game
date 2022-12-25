using UnityEngine ;
using UnityEngine.UI ;
using UnityEngine.Events ;
using System.Collections ;
using TMPro;
using System;
public class Timer : MonoBehaviour {
   [Header ("Timer UI references :")]
   //[SerializeField] private Image uiFillImage ;
   [SerializeField] private TextMeshProUGUI uiText ;
    [SerializeField] public Text X2ButtonuiText;


    public int Duration { get; private set; }

   //public bool IsPaused { get; private set; }

   private int remainingDuration ;
  

   // Events --
    private UnityAction onTimerBeginAction ;
   private UnityAction<int> onTimerChangeAction ;
   private UnityAction onTimerEndAction ;
    private UnityAction SetSystemStartTimer;

    //Use for time Calculations
    TimeSpan TotalCounterSeconds;//Seconds in start when counter start
    TimeSpan CurrentCounterSeconds; //Current seconds when game quit
    TimeSpan SystemTimeDifference;
  
    public int ReturnSeconds(TimeSpan totalTime, TimeSpan CurrentTime)
    {
        TimeSpan subTimer = CurrentTime.Subtract(totalTime).Duration();
        double sec = TotalCounterSeconds.TotalSeconds - subTimer.Seconds;
      
        return subTimer.Seconds;

    }

    private void Start()
    {

        playagain();

    }
    public void playagain()
    {
        //Check if counter seconds are not null then Set system start time
        if (PlayerPrefs.GetInt("CounterSecOnQuit") > 1)
        {
            //Save current Counter time string to Timespan
            DateTime startTime = DateTime.UtcNow;

            //Save Start time to TimeBeginString
            DateTime TimeBeginString = DateTime.Parse(startTime.ToString());
          

            //Save Quit time to TimeQuitString
            DateTime TimeQuitString = DateTime.Parse(PlayerPrefs.GetString("SystemTimeEnd"));
           

            //check if Start time is greater than Quit Time
            if (TimeBeginString > TimeQuitString)
            {
                //Difference between start and end time
                SystemTimeDifference = TimeBeginString - TimeQuitString;
                // SystemTimeDifference =  TimeQuitString-TimeBeginString;
              
                //check if seconds difference is greater than start decided seconds which are stored in CounterSecOnQuit
                int checkdiff = PlayerPrefs.GetInt("CounterSecOnQuit") - (int)SystemTimeDifference.TotalSeconds;
               
                if (checkdiff > 1)
                {
                    
                    PlayerPrefs.SetInt("CounterSecOnQuit", checkdiff);
                  
                  //  SetDuration(PlayerPrefs.GetInt("CounterSecOnQuit")).OnEnd(() => GameController.instance.checkifDoubleMoneyONOFf = false).Begin();
                    //GameController.instance.checkifDoubleMoneyONOFf = true;
                    X2ButtonuiText.gameObject.SetActive(true);
                }
                else
                {
                    //ResetTimer();
                }
            }
            else
            {
                Debug.Log("Time is not greater ");
            }
        }
    }
    void OnApplicationPause(bool pauseStatus)
    {
        if(pauseStatus == true)
        {
            DateTime TimeonQuit = DateTime.UtcNow;
            PlayerPrefs.SetString("SystemTimeEnd", TimeonQuit.ToString());
        }
        else
        {
            playagain();
        }
    }
    //public void SetPaused (bool paused) {
    //   IsPaused = paused ;

    //   if (onTimerPauseAction != null)
    //      onTimerPauseAction.Invoke (IsPaused) ;
    //}

    //Set Seconds to this function to start timer

    public Timer SetDuration (int seconds) {

        //Save total counter seconds for calculations
        TotalCounterSeconds = TimeSpan.FromSeconds((int)seconds);
        PlayerPrefs.SetInt("TotalCounterDuration", (int)TotalCounterSeconds.TotalSeconds);
        
        Duration = remainingDuration = seconds ;
       
        return this ;
   }

   //-- Events ----------------------------------
   public Timer OnBegin (UnityAction action) {
      onTimerBeginAction = action ;
      return this ;
   }

   public Timer OnChange (UnityAction<int> action) {
      onTimerChangeAction = action ;
      return this ;
   }

   public Timer OnEnd (UnityAction action) {
      onTimerEndAction = action ;
      //  PlayerPrefs.SetInt("CounterSecOnQuit",0);
      return this ;
   }

   public Timer OnStart (UnityAction action) {
      SetSystemStartTimer = action ;
      return this ;
   }



    //Call this function to start Timer

   public void Begin () {
      if (onTimerBeginAction != null)
         onTimerBeginAction.Invoke () ;
        
       
      StopAllCoroutines () ;
      StartCoroutine (UpdateTimer ()) ;
   }

    //Using this function for recalling timer 
    public void OnStart()
    {
        SetSystemStartTimer?.Invoke();
        StopAllCoroutines();
        StartCoroutine(UpdateTimer());
    }


    //Update timer with the time
   private IEnumerator UpdateTimer () {
      while (remainingDuration > 0) {
         //if (!IsPaused) {
            if (onTimerChangeAction != null)
               onTimerChangeAction.Invoke (remainingDuration) ;

            UpdateUI (remainingDuration) ;
            remainingDuration-- ;
            
         //}         //}
         yield return new WaitForSeconds (1f) ;
      }
      End () ;
   }


    //Update timer UI
   private void UpdateUI (int seconds) {
        int sec = (int)(seconds % 60);
        int minutes = (int)((seconds / 60) % 60);
        int hours = (int)((seconds / 3600) % 24);
        
       string  CurrentSeconds = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, sec);
//        Debug.Log("current seconds " + CurrentSeconds);

        //show current seconds to popup text
        uiText.text = CurrentSeconds.ToString();
        //Show current seconds to x2 child text 
        X2ButtonuiText.text = CurrentSeconds.ToString();

        //Save current Counter time string to Timespan
        TimeSpan ts = TimeSpan.Parse(CurrentSeconds);
        CurrentCounterSeconds = ts;

        //uiFillImage.fillAmount = Mathf.InverseLerp (0, Duration, seconds) ;
    }

    //This function runs when counter is complete
   public void End () {
      if (onTimerEndAction != null)
         onTimerEndAction.Invoke () ;
      //if(GameStartTutorial.instance != null)
      //  {

      //      Popup.instance.X2MoneyDoublePanel.transform.GetChild(2).GetChild(4).GetComponent<Button>().interactable = true;
      //  }
        //Popup.instance.x2DoubleTimer.gameObject.SetActive(false);
        X2ButtonuiText.gameObject.SetActive(false);
        //ResetTimer () ;
   }

    
    private void OnDestroy()
    {
        if (PlayerPrefs.GetInt("CounterSecOnQuit") > 1)
        {
            //Save Time when application Distroy
            DateTime TimeonQuit = DateTime.UtcNow;
            PlayerPrefs.SetString("SystemTimeEnd", TimeonQuit.ToString());
            Debug.Log("Time on Application Distroy " + PlayerPrefs.GetString("SystemTimeEnd"));

        }
        StopAllCoroutines();
    }


    private void OnApplicationQuit()
    {
   
        int returnedPassedSeconds =  ReturnSeconds(TotalCounterSeconds, CurrentCounterSeconds);
        if (PlayerPrefs.GetInt("CounterSecOnQuit")> 1)
        {
            //Save Time when application quit
            DateTime TimeonQuit = DateTime.UtcNow;
            PlayerPrefs.SetString("SystemTimeEnd", TimeonQuit.ToString());
        }

       
    }
    public void Exit()
    {

        Application.Quit();

    }

}
