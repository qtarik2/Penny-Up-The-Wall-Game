using PlayFab;
using System.Collections;
using System.Collections.Generic;
using PlayFab.AuthenticationModels;
using PlayFab.ClientModels;
using PlayFab.ProfilesModels;
using PlayFab.DataModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;
public class PlayfabManager : MonoBehaviour
{
    public bool isFriendSystem;
    public static PlayfabManager instance;
    public delegate void OnLoginDelegate();
    public OnLoginDelegate onLoginDelegate;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public enum LoginWith
    {
        custom, facebook, google, apple
    }
    public LoginWith loginWith;
    public UnityEvent OnLoginSuccess, OnLogoutSuccess, OnSignupSuccess;
    public InputField usernameField, passwordField, password2Field;
    public Text usertxt;
    public Text logtxt;
    public GameObject loadingPanel;

    async void Start()
    {
        //PhotonNetwork.ConnectUsingSettings();
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            PlayFabSettings.staticSettings.TitleId = "C9232";
        }
        if (PlayerPrefs.HasKey("Email")&&GameManager.instance.autoLogin)
        {
            loadingPanel.SetActive(true);
            await System.Threading.Tasks.Task.Delay(500);
            AutoLogin();
        }
    }
    public void AutoLogin()
    {
        if (PlayerPrefs.HasKey("AccountType"))
        {
            var type = PlayerPrefs.GetString("AccountType");
            switch (type)
            {
                case "Playfab":
                    var UserRegister = new LoginWithPlayFabRequest { Username = PlayerPrefs.GetString("Email"), Password = PlayerPrefs.GetString("Password") };
                    PlayFabClientAPI.LoginWithPlayFab(UserRegister, OnAutoLogin, OnError);
                    break;
                case "Facebook":
                    PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest { CreateAccount = true, AccessToken = PlayerPrefs.GetString("Email") },
                            Onlogin, OnError);
                    break;
                case "Apple":
                    PlayFabClientAPI.LoginWithApple(new LoginWithAppleRequest { CreateAccount = true, IdentityToken = PlayerPrefs.GetString("Email") },
                    Onlogin, OnError);
                    break;
            }
        }
    }
    //logout
    public void Logout()
    {
        GameManager.instance.isLogout=false;
        PlayFabClientAPI.ForgetAllCredentials();
        PlayerPrefs.DeleteKey("Email");
        PlayerPrefs.DeleteKey("AccountType");
        OnLogoutSuccess?.Invoke();
        SceneManager.LoadScene(0);
    }
    //logout end
    // public void AutoLogin()
    // {
    //     var UserRegister = new LoginWithPlayFabRequest { Username = PlayerPrefs.GetString("Email"), Password = PlayerPrefs.GetString("Password") };
    //     PlayFabClientAPI.LoginWithPlayFab(UserRegister, OnAutoLogin, OnError);
    // }
    // singup region
    public string CredentialsValidationsLog()
    {
        if(usernameField.text.Contains("^[a-zA-Z0-9]([._-](?![._-])|[a-zA-Z0-9]){3,18}[a-zA-Z0-9]$"))
        {
            return "Enter Valid Username";
        }
        else if(passwordField.text.Length<6)
        {
            return "password must contain 6 letters";
        }
        else if(password2Field.text!=passwordField.text)
        {
            return "password not match";
        }
        return "Valid";
    }
    public void SignUp()
    {
        if (CredentialsValidationsLog()=="Valid")
        {
            //var UserRegister= new RegisterPlayFabUserRequest{Email="umarhyatt",Password="qqqqqqqq",RequireBothUsernameAndEmail=false};
            var UserRegister = new RegisterPlayFabUserRequest { Username = usernameField.text, Password = passwordField.text, RequireBothUsernameAndEmail = false };
            PlayFabClientAPI.RegisterPlayFabUser(UserRegister, OnRegister, OnError);
        }
        else
        {
            logtxt.gameObject.SetActive(true);
            logtxt.text = CredentialsValidationsLog();
        }
    }
    void OnRegister(RegisterPlayFabUserResult result)
    {
        Debug.Log(result);
        usernameField.text="";
        passwordField.text="";
        OnSignupSuccess.Invoke();
    }
    //login region
    public void LoginWithPlayfab()
    {
        PlayerPrefs.SetString("AccountType", "Playfab");
        var request = new LoginWithPlayFabRequest { Username = usernameField.text, Password = passwordField.text };
        PlayFabClientAPI.LoginWithPlayFab(request, Onlogin, OnError);
        PlayerPrefs.SetString("Email", usernameField.text);
    }
    public void LoginWithFacebook(string token)
    {
        print("login with apple called");
        PlayerPrefs.SetString("AccountType", "Facebook");
        PlayerPrefs.SetString("Email", token);
      //  print(FacebookManager.instance.accessToken.TokenString);
        PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest { CreateAccount = true, AccessToken = token }, Onlogin, OnError);
        // PlayFabClientAPI.LoginWithFacebook(request, Onlogin, OnError);
    }
    public void LoginWithApple(string token)
    {
        print("login with apple called");
        PlayerPrefs.SetString("AccountType", "Apple");
        PlayerPrefs.SetString("Email", token);
        PlayFabClientAPI.LoginWithApple(new LoginWithAppleRequest { CreateAccount = true, IdentityToken = token }, Onlogin, OnError);
    }
    void Onlogin(LoginResult result)
    {
        GameManager.instance.isLogout=false;
        Debug.Log("login success");
        OnLoginSuccess.Invoke();
        UI_UserAuth.instance.SetupObjects();
        PlayerPrefs.SetString("Password", passwordField.text);
        //PhotonManager.instance.OnLeftRoom();
        //PhotonManager.instance.quickMatchButton.gameObject.SetActive(Photon.Pun.PhotonNetwork.IsConnected);
        var request = new GetAccountInfoRequest { PlayFabId = PlayFabSettings.staticPlayer.PlayFabId };
        PlayFabClientAPI.GetAccountInfo(request, (GetAccountInfoResult result1) => {/* usertxt.text = result1.AccountInfo.Username;*/ PhotonManager.instance.SetName(result1.AccountInfo.PlayFabId); }, OnError);
        Debug.Log(result);
        //RequestPhotonToken(result);
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion("eu");
        SetUserDeviceID();
    }
    public void RequestPhotonToken(LoginResult obj)
    {
        _playFabPlayerIdCache = obj.PlayFabId;
        PlayFabClientAPI.GetPhotonAuthenticationToken(new GetPhotonAuthenticationTokenRequest { PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime }, AuthWithPhoton, OnError);
    }
    private string _playFabPlayerIdCache;
    public void AuthWithPhoton(GetPhotonAuthenticationTokenResult obj)
    {
        Debug.Log("photon token is " + obj.PhotonCustomAuthenticationToken);
        //LogMessage("Photon token acquired: " + obj.PhotonCustomAuthenticationToken + "  Authentication complete.");

        //We set AuthType to custom, meaning we bring our own, PlayFab authentication procedure.
        AuthenticationValues customAuth = new AuthenticationValues(obj.PhotonCustomAuthenticationToken);
        customAuth.AuthType = CustomAuthenticationType.Custom;
        //We add "username" parameter. Do not let it confuse you: PlayFab is expecting this parameter to contain player PlayFab ID (!) and not username.
        customAuth.AddAuthParameter("username", _playFabPlayerIdCache);    // expected by PlayFab custom auth service

        //We add "token" parameter. PlayFab expects it to contain Photon Authentication Token issues to your during previous step.
        customAuth.AddAuthParameter("token", obj.PhotonCustomAuthenticationToken);

        //We finally tell Photon to use this authentication parameters throughout the entire application.
        PhotonNetwork.AuthValues = customAuth;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion("eu");

    }
    void OnAutoLogin(LoginResult result)
    {
        GameManager.instance.isLogout=false;
        Debug.Log("autologin success");
        loadingPanel.SetActive(false);
        OnLoginSuccess.Invoke();
        UI_UserAuth.instance.SetupObjects();
        var request = new GetAccountInfoRequest { PlayFabId = PlayFabSettings.staticPlayer.PlayFabId };
        PlayFabClientAPI.GetAccountInfo(request, (GetAccountInfoResult result1) => { /* usertxt.text = result1.AccountInfo.Username;*/ PhotonManager.instance.SetName(result1.AccountInfo.PlayFabId); }, OnError);
        //usertxt.text=PlayFabSettings.staticPlayer.PlayFabId;
        print("auto login with " + PlayerPrefs.GetString("Email"));
        // RequestPhotonToken(result);
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion("eu");
        SetUserDeviceID();
    }
     void OnError(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage);
        PlayerPrefs.DeleteKey("Email");
        string log=error.ErrorMessage;
        if(error.ErrorMessage.Contains("Invalid input parameters"))
        {
            log="Invalid input parameters";
        }
        logtxt.gameObject.SetActive(true);
        logtxt.text = log;
       // await System.Threading.Tasks.Task.Delay(2000);
       // logtxt.text = "";
    }
    public void CheckCurrentLogin()
    {
        var request = new ExecuteCloudScriptRequest { FunctionName = "hello" };
        PlayFabClientAPI.ExecuteCloudScript(request, OnSuccessCloudScript, OnError);
    }
    #region CheckmultiUser
    void SetUserDeviceID()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>(){
            {"deviceID",SystemInfo.deviceUniqueIdentifier},
        }
        },
        result => { Debug.Log("Successfully updated user device ID"); /*StartCoroutine(CheckMultiUser()); for multiuser blocker*/ },
        error =>
        {
            Debug.Log("Got error setting user data Ancestor to Arthur"); 
            Debug.Log(error.GenerateErrorReport());
        });
    }
    void OnSuccessCloudScript(ExecuteCloudScriptResult cloudScriptResult)
    {
        var req = new SendAccountRecoveryEmailRequest { };
        Debug.Log(cloudScriptResult.FunctionResult.ToString());
    }
    string deviceid;
    IEnumerator CheckMultiUser()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            GetUserData(PlayFabSettings.staticPlayer.PlayFabId);
        }
    }
    void GetUserData(string myPlayFabeId)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myPlayFabeId,
            Keys = null
        }, result =>
        {
            Debug.Log("Got user data:");
            if (result.Data == null || !result.Data.ContainsKey("deviceID"))
            {
                Debug.Log("no id set");
            }
            else
            {
                Debug.Log("DeviceID: " + result.Data["deviceID"].Value);
                deviceid = result.Data["deviceID"].Value;
                LogoutIfOld();
            }
        }, (error) =>
        {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }
    void LogoutIfOld()
    {
        if (deviceid != SystemInfo.deviceUniqueIdentifier)
        {
            print("logout because new signin");
            Logout();
        }
    }
    #endregion
    public void BackButton()
    {
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
        DestroyImmediate(PhotonManager.instance.gameObject);
        SceneManager.UnloadSceneAsync("UserAuth");
    }
}