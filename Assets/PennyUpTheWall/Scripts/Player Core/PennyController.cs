using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using MySpace;
public class PennyController : MonoBehaviour, IPennyController
{
    public GameObject LastCoinPoint;

    //public LineRenderer _LineRendrer;
    Vector3 mousedownPosition, mouseDragPosition, mouseRelesePosition;
    private Vector3 force;

    // //Gameplay Hand prefab
    [HideInInspector] public Transform HandTransform;
    [HideInInspector] public GameObject Hand;
    //For Male And Female
    public GameObject Player_Gander;
    //Penny rigidbody reference
    Rigidbody rb;
    public float playerPower, aiPower;
    //isShoot bool is using to check is penny released or not 
    private bool isShoot;
    public state playe_state;
    public GameObject test;
    public float waitForForfeit = 5f;


    bool collisonOccured = false;
    [HideInInspector]
    public bool stopCrountimeUser = false;
    [HideInInspector]
    public bool stopCrountimeBoss = false;

    TouchInput touchInput;

    //GameObject text;
    public HandData hand_textur;
    //Singleton reference to access this script
    #region Singleton
    public static PennyController instance;
    public Animator thol;
    public Transform camPos;

    float t = 0f;
    bool startfalsing;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public void ControllerType()
    {

    }
    private void Start()
    {
        LevelSelection.instance.isClickN = false;
        Camera.main.transform.parent = null;
        PosBar.instance.stopFollow = false;
        PosBar.instance.enabled = true;
        PowerBar.instance.gameObject.SetActive(true);
        PosBar.instance.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(PosBar.instance.GetComponent<CanvasGroup>().alpha, 1f, Mathf.Sin(1f));
        FindObjectOfType<WindArea>().isStop = true;
        StartCoroutine(FindObjectOfType<WindArea>().SetWindDirection());
        touchInput = FindObjectOfType<TouchInput>();
        touchInput.pennyController = this;
        UIManager.instance.bossImage.fillAmount = 1f;
        UIManager.instance.userImage.fillAmount = 1f;
        t = 0f;
        //  _LineRendrer = GameObject.Find("NewLine").GetComponent<LineRenderer>();
        test = transform.GetChild(0).transform.gameObject;
        test.transform.parent = null;
        test.GetComponent<CameraController>().player = gameObject;
        //test.transform.parent = null;
        //text = GameObject.FindGameObjectWithTag("distance");
        //Storing rigidbody reference to rb variable
        rb = this.GetComponent<Rigidbody>();
        Player_Gander = GameObject.FindGameObjectWithTag("All_Players");
        //Storing Gameplay player Hand Transform reference to HandTransform Gameobject
        /*  if (Player_Gander.transform.GetChild(0).gameObject.activeInHierarchy == true)
          {
              print("Male Hand Is active");
              //HandTransform = GameObject.FindGameObjectWithTag("Player").transform;
              HandTransform = Player_Gander.transform.GetChild(0).transform;
          }
          if (Player_Gander.transform.GetChild(1).gameObject.activeInHierarchy == true)
          {
              print("Female Hand Is Active ");
              //HandTransform = GameObject.FindGameObjectWithTag("Player").transform;
              HandTransform = Player_Gander.transform.GetChild(1).transform;
          }*/
        Hand = GameObject.FindGameObjectWithTag("Hand");
        if (playe_state == state.boss)
        {
            PowerBar.instance.accuracyTxt.transform.SetParent(PowerBar.instance.transform);
            PowerBar.instance.accuracyTxt.gameObject.SetActive(false);
            touchInput.enabled = false;
            PowerBar.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0f;
            UIManager.instance._swipeHand.gameObject.SetActive(false);
            if (BossAI.instance.Total_count == 0)
            {
                UIManager.instance.b1.Boss_load_Dailogue_fun();
                Invoke("bossAI", 2f * ((UIManager.instance.b1.lenght_of_Dialogues) + 2f));
                GameManager.instance.gameStates = GameStates.IgnoreTouches;
                UIManager.instance.Boss_hand_texture_function();
                UIManager.instance.Boss_text.SetActive(true);
                if (Player_Gander.transform.GetChild(0).gameObject.activeInHierarchy == true && BossAI.instance.Boos_Gander == false)
                {
                    //HandTransform = GameObject.FindGameObjectWithTag("Player").transform;
                    HandTransform = Player_Gander.transform.GetChild(0).transform;
                    UIManager.instance.Boss_hand_texture_function();
                }
                if (Player_Gander.transform.GetChild(1).gameObject.activeInHierarchy == true && BossAI.instance.Boos_Gander == true)
                {
                    //HandTransform = GameObject.FindGameObjectWithTag("Player").transform;
                    HandTransform = Player_Gander.transform.GetChild(1).transform;
                }
            }
            else
            {
                Invoke("bossAI", 3f);
                //new WaitForSeconds(3f);
                //bossAI();
                //touch = true;
                GameManager.instance.gameStates = GameStates.IgnoreTouches;
                UIManager.instance.Boss_hand_texture_function();
                if (Player_Gander.transform.GetChild(0).gameObject.activeInHierarchy == true)
                {
                    //HandTransform = GameObject.FindGameObjectWithTag("Player").transform;
                    HandTransform = Player_Gander.transform.GetChild(0).transform;
                }
                if (Player_Gander.transform.GetChild(1).gameObject.activeInHierarchy == true)
                {
                    //HandTransform = GameObject.FindGameObjectWithTag("Player").transform;
                    HandTransform = Player_Gander.transform.GetChild(1).transform;
                }
                UIManager.instance.Boss_text.SetActive(true);
            }
        }
        if (playe_state == state.user)
        {
            GameManager.instance.gameStates = GameStates.Playing;
            PowerBar.instance.accuracyTxt.transform.SetParent(PowerBar.instance.transform.parent);
            PowerBar.instance.accuracyTxt.gameObject.SetActive(false);
            touchInput.enabled = true;
            PowerBar.instance.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
            if (PlayerPrefs.GetString("gander") == "male")
            {
                //For hand Active And Deactivetion 
                GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(1).gameObject.SetActive(false);
                Hand = Player_Gander.transform.GetChild(0).transform.GetChild(1).gameObject;
                Hand.GetComponent<SkinnedMeshRenderer>().materials = hand_textur._HandData[PlayerPrefs.GetInt(PlayerPrefs.GetString("gander"))]._handMaterial;
            }
            else
           if (PlayerPrefs.GetString("gander") == "female")
            {
                GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("All_Players").transform.GetChild(1).gameObject.SetActive(true);
            }
            if (Player_Gander.transform.GetChild(0).gameObject.activeInHierarchy == true)
            {
                //HandTransform = GameObject.FindGameObjectWithTag("Player").transform;
                HandTransform = Player_Gander.transform.GetChild(0).transform;
            }
            if (Player_Gander.transform.GetChild(1).gameObject.activeInHierarchy == true)
            {
                //HandTransform = GameObject.FindGameObjectWithTag("Player").transform;
                HandTransform = Player_Gander.transform.GetChild(1).transform;
            }
            Hand.GetComponent<SkinnedMeshRenderer>().material = hand_textur._HandData[PlayerPrefs.GetInt(PlayerPrefs.GetString("gander"))]._handMaterial[0];
            // Hand.GetComponent<SkinnedMeshRenderer>().materials[1] = hand_textur._HandData[PlayerPrefs.GetInt(PlayerPrefs.GetString("gander"))]._handMaterial;
        }
    }

    private void LateUpdate()
    {
        print("coin position" + transform.position);
        //Check if penny is released and not grounded yet rotate the penny 
        if (isShoot && !collisonOccured)
            transform.RotateAround(transform.position, Vector3.right, 540 * Time.deltaTime);

        if (startfalsing) { transform.localEulerAngles = Vector3.zero; }

        if (playe_state == state.boss)
        {
            t = Time.deltaTime;
            UIManager.instance._swipeHand.gameObject.SetActive(false);
            if(UIManager.instance.bossImage.transform.gameObject.activeInHierarchy == true && !stopCrountimeBoss) { UIManager.instance.bossImage.fillAmount -= ((waitForForfeit / 2) * 0.01f) * t; }
            if(UIManager.instance.bossImage.fillAmount == 0f) { forfeit(); }
        }

        if(playe_state == state.user)
        {
            t = Time.deltaTime;
            if (UIManager.instance.userImage.transform.gameObject.activeInHierarchy == true && !stopCrountimeUser) {UIManager.instance.userImage.fillAmount -= ((waitForForfeit / 2) * 0.01f) * t;}
            if (UIManager.instance.userImage.fillAmount == 0f) { forfeit(); }
        }
    }

    //for coin click only one time 
    public bool mouse_down = true;

    public void OnDown()
    {


        if (mouse_down == false)
            return;
        if (GameManager.instance.gameStates == GameStates.IgnoreTouches || playe_state == state.boss)
            return;
        // getting touch position when you touch the screen
        mousedownPosition = Input.mousePosition;

        //Set true coin image when click on the penny
        //UIManager.instance?.PennyImage.gameObject.SetActive(true);


        //Make this penny clone child of the hand transform
        if (PlayerPrefs.GetString("gander") == "female")
        {
            this.transform.parent = Spawner.instance.SpawnPos_F.transform;
        }
        else
        {
            this.transform.parent = Spawner.instance.SpawnPos.transform;

        }

        //Set gravity false when penny is not released
        this.rb.useGravity = false;
        //Set trail false when penny is not released
        //  this.transform.GetComponent<TrailRenderer>().enabled = false;
        Camera.main.ScreenToWorldPoint(new Vector3(0f, (Random.Range(0, Screen.width)), 0f));


    }
    public void OnDrag()
    {
        if (mouse_down == false)
            return;
        // Debug.Log("Mouse is drag");
        if (GameManager.instance.gameStates == GameStates.IgnoreTouches || playe_state == state.boss)
            return;

        stopCrountimeUser = true;
        //Generate Force by calculating distance between first clicked position(mousePressDownPos) and current pull position

        //RectTransformUtility.ScreenPointToLocalPointInRectangle(UIManager.instance?._CanvasRef, Input.mousePosition, UIManager.instance?._CanvasRef.GetComponent<Canvas>().renderMode == RenderMode.ScreenSpaceOverlay ? null : UIManager.instance?._mainCamera, out _anchoredPos);
        //UIManager.instance.PennyImage.GetComponent<RectTransform>().anchoredPosition = _anchoredPos;

        //UN-comment for swipe controls
        //Vector3 forceV = (new Vector3(-direction.x * throwForceInXandY, -direction.y * throwForceInXandY, throwForceInZ / timeInterval));
        //da3

        //check if not already shooted(iShoot is false)
        if (!isShoot)//Call DrawTrajectory script function to draw trajectory and pass derived force,Rigidbody and penny clone current position
        {
            mouseRelesePosition = Input.mousePosition;
            force = mousedownPosition - mouseRelesePosition;
            Vector3 forceV = (new Vector3(force.x, force.y, force.y) * 1f);
            // print("Force Of Y  "+ forceV.y);
            // print("Force Of X "  +forceV.x);
            // if (forceV.y<20f) {
            //  forceV = new Vector3(force.x+.099f, force.y,force.y);
            //   DrawTrajectory.instance.UpdateTrajectory(forceV, rb, this.transform.position);
            // DrawTrajectory.instance.lineSegments = (int)force.y * 2;
            //  }else

            UIManager.instance._speedoMeter.SetActive(true);// Force Panel 
            UIManager.instance._DragSpeed.SetActive(true);//drag panel
            //DrawTrajectory.instance.UpdateTrajectory(force, rb, transform.position);

            Vector3 velocity = (force / rb.mass) * Time.fixedDeltaTime;

            float FlightDuration = (2 * velocity.y / Physics.gravity.y);

            //DradSpeed._instance.SetSpeed(-FlightDuration);


            //UIManager.instance._speedoMeter.GetComponent<SpeedoMeter>().SliderSpeed.value = dif;
            //UIManager.instance._speedoMeter.GetComponent<SpeedoMeter>().SetSpeed(sliderSpeed);
            //  UIManager.instance._speedoMeter.GetComponent<SpeedoMeter>().UpdateSpeedoMeter(sliderSpeed);
        }
    }
    public void OnUp()
    {
        if (GetComponent<PennyShoot>().forceV.y > -80) { stopCrountimeUser = false; }
        else
        {
            if (mouse_down == false)
                return;
            if (GameManager.instance.gameStates == GameStates.IgnoreTouches || playe_state == state.boss)
                return;

            /*if (PlayerPrefs.GetString("gander") == "female") { Camera.main.transform.SetParent(Spawner.instance.SpawnPos_F.transform); }
            else { Camera.main.transform.SetParent(Spawner.instance.SpawnPos.transform); }*/
            FindObjectOfType<WindArea>().isStop = false;
            // add force to balls rigidbody in 3D space depending on swipe time, direction and throw forces
            rb.isKinematic = false;
            UIManager.instance._speedoMeter.SetActive(false);
            UIManager.instance._DragSpeed.SetActive(false);
            if (mousedownPosition.y > mouseRelesePosition.y)
            {
                PowerBar.instance.anim.speed = 0;
                PowerBar.instance.AccuracyChecker();
                PowerBar.instance.gameObject.GetComponent<DOTweenAnimation>().DOPause();
                mouseRelesePosition = Input.mousePosition;
                force = mouseRelesePosition - mousedownPosition;
                BossAI.instance.Player_force = force;
                //Play Hand Throw Animation
                HandTransform.GetComponent<Animator>().SetBool("HandAnimbool", true);
                HandTransform.GetComponent<Animator>().speed = 1f;
                StartCoroutine(wF());
                mouse_down = false;
            }
            else if (mousedownPosition.y < mouseRelesePosition.y)
            {
                return;
                // HideLine();
            }
        }
    }
    //Shoot function is used to apply force on the penny
    IEnumerator wF()
    {
        yield return new WaitForSeconds(1.7f);

    }
    void Shoot()
    {
        GetComponent<MeshRenderer>().enabled = false;
        UIManager.instance._swipeHand.gameObject.SetActive(false);
        startfalsing = true;
        if (playe_state == state.user)
        {
            // PowerBar.instance.anim.speed=0;
            //If bool is already active mean penny is thrown return from this function 
            if (isShoot)
                return;
            //if bool is not active apply force to penny
            //rb.AddForce(new Vector3(Force.y, Force.z, Force.x) * ForceMultiplayer);
            //rb.AddForce(new Vector3(Force.x, Force.y, Force.y) * ForceMultiplayer);
            AudioHandler.instance.ArmMovementSound();
            isShoot = true;
            test.SetActive(true);
            test.GetComponent<CameraController>().startAnim = true;
            camPos.GetComponent<Animator>().Play("CamRotate");
            //test.SetActive(true);
            //transform.GetChild(0).gameObject.SetActive(true);
            //transform.GetChild(0).gameObject.SetActive(true);
            // transform.GetChild(0).gameObject.SetActive(true);
        }

        if (playe_state == state.boss)
        {
            UIManager.instance._swipeHand.gameObject.SetActive(false);
            //If bool is already active mean penny is thrown return from this function 
            if (isShoot)
                return;
            //transform.GetComponent<MeshRenderer>().enabled = true;
            test.SetActive(true);
            test.GetComponent<CameraController>().startAnim = true;
            //if bool is not active apply force to penny
            //rb.AddForce(new Vector3(Force.y, Force.z, Force.x) * ForceMultiplayer);
            //rb.AddForce(new Vector3(Force.x, Force.y, Force.y) * ForceMultiplayer);
            //rb.AddForce(-direction.x * throwForceInXandY, -direction.y * throwForceInXandY, throwForceInZ / timeInterval);
            int selectP = Random.Range(60, 240);
            int selectR = Random.Range(-20, 20);
            int multiple = Random.Range(1, 2);
            rb.AddForce(new Vector3(selectR * multiple, selectP * multiple, selectP * multiple));
            PowerBar.instance.gameObject.SetActive(false);
            //PosBar.instance.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(PosBar.instance.GetComponent<CanvasGroup>().alpha, 0.5f, Mathf.Sin(1f));
        }
    }
    public void bossAI()
    {
        // add force to balls rigidbody in 3D space depending on swipe time, direction and throw forces
        rb.isKinematic = false;
        if (BossAI.instance.Boos_Gander == false)
        {
            this.transform.parent = Spawner.instance.SpawnPos.transform;
        }
        else
            if (BossAI.instance.Boos_Gander == true)
        {
            this.transform.parent = Spawner.instance.SpawnPos_F.transform;
        }
        UIManager.instance._swipeHand.gameObject.SetActive(false);
        //Play Hand Throw Animation
        //new WaitForSeconds(5f);
        HandTransform.GetComponent<Animator>().SetBool("HandAnimbool", true);
        stopCrountimeBoss = true;
        HandTransform.GetComponent<Animator>().speed = 0.8f;
        if (LevelSelection.instance.nowModel != null) { LevelSelection.instance.nowModel.GetComponent<CharacterProperite>().coinHand.gameObject.SetActive(true); }
        if (LevelSelection.instance.nowModel != null) { LevelSelection.instance.nowModel.GetComponent<Animator>().Play("throw"); }
        StartCoroutine(playanimation());
        StartCoroutine(wF());
        PowerBar.instance.anim.speed = 0;
        PowerBar.instance.AccuracyChecker();
        mouse_down = false;


    }
    IEnumerator playanimation()
    {
        yield return new WaitForSeconds(1.65f);
    }
    IEnumerator Fade()
    {
        //text.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(3f);
        AudioHandler.instance.BossWin_Lose_Sound(BossAI.instance.boss_count);
        //text.GetComponent<Text>().enabled = false;
    }
    public int distance;
    public LayerMask ignoreSoundLayer;
    private void OnCollisionEnter(Collision collision)
    {
        // CoinSoundController.instance.PlayCoinSound(collision.gameObject.layer);
        if (collisonOccured) return;
        //When penny collide with ground destroy penny clone prefab

        if (collision.transform.CompareTag("Ground"))
        {
            test.GetComponent<CameraController>().startAnim = false;
            GetComponent<PennyShoot>().up = false;
            startfalsing = false;
            thol.SetBool("rot", false);
            GetComponent<MeshRenderer>().enabled = true;
            thol.GetComponent<MeshRenderer>().enabled = false;
            CoinSoundController.instance.PlayCoinSound(collision.transform.name);
            PosBar.instance.enabled = false;
            PosBar.instance.stopFollow = true;
            // CoinSoundController.instance.PlayCoinSound();
            //qwerUIManager.instance._swipeHand.gameObject.SetActive(false);
            mouse_down = true;
            //if (playe_state == state.user) { FindObjectOfType<CameraController>().startFollow = true; }

            //text.GetComponent<Text>().text = Mathf.Round(Vector3.Distance(Player_Gander.transform.position, this.transform.position)).ToString() + " Meter";
            if (playe_state == state.user) { StartCoroutine(Fade()); }
            // Debug.LogError(Vector3.Distance(Player_Gander.transform.position, this.transform.position));
            collisonOccured = true;
            // AudioHandler.instance.PannySoundPlay();
            distance = (int)Mathf.Round(Vector3.Distance(Player_Gander.transform.position, this.transform.position));
            /*var point=Instantiate(LastCoinPoint,transform.position,Quaternion.identity).GetComponent<LastCoinPosition>();
            GameObject parentObject=GameObject.FindWithTag("LastCoinPoints");
            point.transform.parent=parentObject.transform;*/
            ScoreBoard.instance.OnUpdateScore(distance);
            LastPointPulling.instance.NextPoint(distance.ToString("0") + " Meter", playe_state == state.user).position = transform.position + new Vector3(0,0.1f,0);
            // if(parentObject.transform.childCount>=3)
            // {
            //     Destroy(parentObject.transform.GetChild(0).gameObject);
            // }
            Destroy(test, 1.5f);
            //point.distanceTxt.text=distance.ToString("0")+" Meter";
            //Debug.Log("Point instantiat");
            //FindObjectOfType<CameraController>().gameObject.SetActive(false);
            //transform.GetChild(0).gameObject.SetActive(false);
            //Invoke Re-Spawn event function
            Invoke("InvokeEventAfterWait", .7f);
            if (playe_state == state.user)
            {
                BossAI.instance.player_positin = rb.GetComponent<Transform>().position;
                PosBar.instance.InstatitateIcon(transform, true);
                if (LevelSelection.instance.All_Character.transform.childCount > 0) 
                {
                }
            }
            else
            {
                BossAI.instance.Boss_position = this.GetComponent<Transform>().position;
                BossAI.instance.Total_count++;
                PosBar.instance.InstatitateIcon(transform, false);
                if (BossAI.instance.player_positin.z > BossAI.instance.Boss_position.z)
                {
                    ResultController.instance.ResultSetter(true);
                    BossAI.instance.user_count++;
                    //   StartCoroutine(Turn_Win());
                    if (BossAI.instance.user_count == 2)
                    {

                        //   UIManager.instance.TurnWinPanel.SetActive(false);
                        UIManager.instance._swipeHand.gameObject.SetActive(false);
                        UIManager.instance.PauseButton.enabled = false;

                        UIManager.instance.b1.Bet_won();
                        StartCoroutine(UIManager.instance.Play_stop_Particles());
                        AudioHandler.instance.WinningClap();
                        touchInput.enabled = false;
                        Destroy(this.gameObject, 6f);
                        GameManager.instance.gameStates = GameStates.IgnoreTouches;
                        return;
                    }
                }
                else
                {
                    ResultController.instance.ResultSetter(false);
                    UIManager.instance._swipeHand.gameObject.SetActive(false);
                    //AudioHandler.instance.BossWin_Lose_Sound(BossAI.instance.boss_count);
                    BossAI.instance.boss_count++;
                    //   StartCoroutine(Turn_lose());
                    if (BossAI.instance.boss_count == 2)
                    {
                        //  UIManager.instance.TurnLostPanel.SetActive(false);
                        UIManager.instance._swipeHand.gameObject.SetActive(false);
                        UIManager.instance.PauseButton.enabled = false;
                        //    UIManager.instance.b1.Bet_lose();
                        UIManager.instance.SetActivePanel(11);
                        ScoreBoard.instance.losePng.SetActive(true);
                        AudioHandler.instance.LoseSound();
                        touchInput.enabled = false;
                        Destroy(this.gameObject, 5f);
                        return;
                    }
                }
                if (BossAI.instance.Total_count == 3)
                {
                    if (BossAI.instance.user_count > BossAI.instance.boss_count)
                    {
                        //UIManager.instance.TurnWinPanel.SetActive(false);
                        UIManager.instance._swipeHand.gameObject.SetActive(false);
                        UIManager.instance.PauseButton.enabled = false;
                        UIManager.instance.b1.Bet_won();
                        StartCoroutine(UIManager.instance.Play_stop_Particles());
                    }
                    else
                    {
                        //  UIManager.instance.TurnLostPanel.SetActive(false);
                        UIManager.instance._swipeHand.gameObject.SetActive(false);
                        UIManager.instance.PauseButton.enabled = false;
                        AudioHandler.instance.LoseSound();
                        touchInput.enabled = false;
                        //   UIManager.instance.b1.Bet_lose();
                        ScoreBoard.instance.losePng.SetActive(true);
                        UIManager.instance.SetActivePanel(11);
                    }


                }
            }
            //Destroy current penny after falling on ground
            Destroy(this.gameObject, 5f);
            if (UIManager.instance.count == 4)
            {
                UIManager.instance.RematchButton.gameObject.SetActive(true);
            }
            else { UIManager.instance.RematchButton.gameObject.SetActive(false); }
        }

    }
    void forfeit()
    {
        mouse_down = true;
        StartCoroutine(Fade());
        collisonOccured = true;
        Destroy(test, 1.5f);
        distance = (int)Mathf.Round(Vector3.Distance(Player_Gander.transform.position, this.transform.position));
        ScoreBoard.instance.OnUpdateScore(distance);
        Invoke("InvokeEventAfterWait", 0f);
        if (playe_state == state.user)
        {
            BossAI.instance.player_positin = rb.GetComponent<Transform>().position;
        }
        else
        {
            BossAI.instance.Boss_position = this.GetComponent<Transform>().position;
            BossAI.instance.Total_count++;
            if (BossAI.instance.player_positin.z > BossAI.instance.Boss_position.z)
            {
                ResultController.instance.ResultSetter(true);
                BossAI.instance.user_count++;
                //   StartCoroutine(Turn_Win());
                if (BossAI.instance.user_count == 2)
                {

                    //   UIManager.instance.TurnWinPanel.SetActive(false);
                    UIManager.instance._swipeHand.gameObject.SetActive(false);
                    UIManager.instance.PauseButton.enabled = false;

                    UIManager.instance.b1.Bet_won();
                    StartCoroutine(UIManager.instance.Play_stop_Particles());
                    AudioHandler.instance.WinningClap();
                    Destroy(this.gameObject);
                    GameManager.instance.gameStates = GameStates.IgnoreTouches;
                    return;
                }
            }
            else
            {
                ResultController.instance.ResultSetter(false);
                UIManager.instance._swipeHand.gameObject.SetActive(false);
                BossAI.instance.boss_count++;
                //   StartCoroutine(Turn_lose());
                if (BossAI.instance.boss_count == 2)
                {
                    //  UIManager.instance.TurnLostPanel.SetActive(false);
                    UIManager.instance._swipeHand.gameObject.SetActive(false);
                    UIManager.instance.PauseButton.enabled = false;
                    //    UIManager.instance.b1.Bet_lose();
                    UIManager.instance.SetActivePanel(11);
                    ScoreBoard.instance.losePng.SetActive(true);
                    AudioHandler.instance.LoseSound();
                    Destroy(this.gameObject);
                    return;
                }
            }
            if (BossAI.instance.Total_count == 3)
            {
                if (BossAI.instance.user_count > BossAI.instance.boss_count)
                {
                    //UIManager.instance.TurnWinPanel.SetActive(false);
                    UIManager.instance._swipeHand.gameObject.SetActive(false);
                    UIManager.instance.PauseButton.enabled = false;
                    UIManager.instance.b1.Bet_won();
                    StartCoroutine(UIManager.instance.Play_stop_Particles());
                }
                else
                {
                    //  UIManager.instance.TurnLostPanel.SetActive(false);
                    UIManager.instance._swipeHand.gameObject.SetActive(false);
                    UIManager.instance.PauseButton.enabled = false;
                    AudioHandler.instance.LoseSound();
                    //   UIManager.instance.b1.Bet_lose();
                    ScoreBoard.instance.losePng.SetActive(true);
                    UIManager.instance.SetActivePanel(11);
                }


            }
        }
        //Destroy current penny after falling on ground
        Destroy(this.gameObject);
        if (UIManager.instance.count == 4)
        {
            UIManager.instance.RematchButton.gameObject.SetActive(true);
        }
        else { UIManager.instance.RematchButton.gameObject.SetActive(false); }
    }

    void InvokeEventAfterWait()
    {
        //Invoke event to respawn penny
        PowerBar.instance.anim.speed = 1;
        Spawner.instance.PennyDestroyed.Invoke();
        if (playe_state == state.boss)
        {
            UIManager.instance.Boss_text.SetActive(false);
            Spawner.instance.SpawnedObject.GetComponent<PennyController>().playe_state = state.user;
            if (BossAI.instance.user_count >= 2)
            {
                PennyController.instance.mouse_down = false;
            }
            playe_state = state.user;

        }
        else
        {
            Spawner.instance.SpawnedObject.GetComponent<PennyController>().playe_state = state.boss;
            playe_state = state.boss;

            // new WaitForSeconds(.5f);
            //  Spawner.instance.SpawnedObject.GetComponent<PennyController>().bossAI();
            // state change Func//

        }
        /* switch (playe_state)
         {
             case state.user:
                 Spawner.instance.PennyDestroyed.Invoke();
                 Spawner.instance.SpawnedObject.GetComponent<PennyController>().playe_state = state.boss;
                 playe_state = state.boss;
                 print("player is boss   " + Spawner.instance.SpawnedObject.GetComponent<PennyController>().playe_state);

                 break;
             case state.boss:
                 Spawner.instance.SpawnedObject.GetComponent<PennyController>().playe_state = state.user;
                 Spawner.instance.PennyDestroyed.Invoke();
                 playe_state = state.user;
                 print("player is user  " + Spawner.instance.SpawnedObject.GetComponent<PennyController>().playe_state);
                 break;

         }*/

    }

    //Animation event attached to the hand thrown animation
    public void OnAnimationEventTrigger()
    {
        thol.SetBool("rot", true);
        thol.GetComponent<MeshRenderer>().enabled = true;
        if (LevelSelection.instance.nowModel != null) { LevelSelection.instance.nowModel.GetComponent<CharacterProperite>().coinHand.gameObject.SetActive(false); }
        UIManager.instance._swipeHand.gameObject.SetActive(false);
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        Shoot();
        GetComponent<PennyShoot>().Shoot();
        UIManager.instance.SoundDropped.Play();
    }
    public void For_exit()
    {
        if (playe_state == state.boss)
        {
            // if (gameObject!=null) {
            //    Destroy(gameObject, 2f); }
            UIManager.instance.Boss_text.SetActive(false);
            Spawner.instance.SpawnedObject.GetComponent<PennyController>().playe_state = state.user;
            playe_state = state.user;
            if (Spawner.instance.PennyDestroyed == null) { Spawner.instance.PennyDestroyed.Invoke(); }
        }
        else
        {
            // Destroy(this.gameObject, 2f);
            UIManager.instance.Boss_text.SetActive(false);
            Spawner.instance.SpawnedObject.GetComponent<PennyController>().playe_state = state.user;
            playe_state = state.user;
            if (Spawner.instance.PennyDestroyed == null) { Spawner.instance.PennyDestroyed.Invoke(); }
        }
    }
    /*  IEnumerator Turn_Win() {
          GameManager.instance.gameStates = GameStates.IgnoreTouches;
          UIManager.instance.TurnWinPanel.SetActive(true);
          yield return new WaitForSeconds(1.5f);
          UIManager.instance.TurnWinPanel.SetActive(false);
          GameManager.instance.gameStates = GameStates.Playing;
      }
      IEnumerator Turn_lose() {
          GameManager.instance.gameStates = GameStates.IgnoreTouches;
        //  UIManager.instance.TurnLostPanel.SetActive(true);
          yield return new WaitForSeconds(1.5f);
         // UIManager.instance.TurnLostPanel.SetActive(false);
          GameManager.instance.gameStates = GameStates.Playing;

      }
      */
}
