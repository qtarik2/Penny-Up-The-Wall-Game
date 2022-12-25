using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;

public class PlayFabFriendList : MonoBehaviour
{
    /// singleton
    public static PlayFabFriendList instance;
    private void Awake() {if (instance == null) instance = this;}
    public List<string> invitedPlayersID=new List<string>();
    public List<GameObject> myfriends = new List<GameObject>();
    public GameObject myFriendPrefab, myFriendParent;
    public List<GameObject> requestees = new List<GameObject>();
    public GameObject requesteePrefab, requesteeParent;
    public List<GameObject> requesters = new List<GameObject>();
    public GameObject requesterPrefab, requesterParent;
    public GameObject searchUserObj;
    public InputField searchField;

    [Header("UI Buttons and Panels")]
    public GameObject friendsPanel;
    public GameObject requestsPanel;
    public GameObject sendRequestPanel;
    public GameObject searchUserPanel;
    private void OnEnable() 
    {
        UpdateFriendList();
    }
    public void AddInvitedFriendID(bool add,string id)
    {
        if(add)
        {
            if(!invitedPlayersID.Contains(id))
            {
                invitedPlayersID.Add(id);
            }
            print(invitedPlayersID);
        }
        else
        {
            invitedPlayersID.Remove(id);
            print(invitedPlayersID);
        }
    }
    public void OnMyFriendsClicked()
    {
        searchFriend = true;
        friendsPanel.SetActive(true);
        requestsPanel.SetActive(false);
        sendRequestPanel.SetActive(false);
        searchUserPanel.SetActive(false);
        UpdateFriendList();
    }
    public void OnAddFriendsClicked()
    {
        searchFriend = false;
        friendsPanel.SetActive(false);
        requestsPanel.SetActive(true);
        sendRequestPanel.SetActive(true);
        searchUserPanel.SetActive(false);
        UpdateFriendList();
    }
    // friend list -----------
    public void UpdateFriendList()
    {
        GetFriendsListRequest req = new GetFriendsListRequest();
        PlayFabClientAPI.GetFriendsList(req, OnGetFriendsListResult, OnError);
    }
    public void OnGetFriendsListResult(GetFriendsListResult result)
    {
        foreach (var item in myfriends)
        {
            item.SetActive(false);
        }
        foreach (var item in requesters)
        {
            item.SetActive(false);
        }
        foreach (var item in requestees)
        {
            item.SetActive(false);
        }
        var friends = result.Friends;
        foreach (var item in friends)
        {
            foreach (var tag in item.Tags)
            {
                switch (tag)
                {
                    case "requester":
                        var obj = reciveReqObject();
                        obj.SetActive(true);
                        obj.transform.GetChild(1).GetComponent<Text>().text = item.Username;
                        obj.GetComponent<Playfab_User>().userAccountInfo.PlayFabId=item.FriendPlayFabId;
                        break;
                    case "requestee":
                        var obj1 = SendReqObject();
                        obj1.SetActive(true);
                        obj1.transform.GetChild(1).GetComponent<Text>().text = item.Username;
                        obj1.GetComponent<Playfab_User>().userAccountInfo.PlayFabId=item.FriendPlayFabId;
                        break;
                    case "confirmed":
                        var obj2 = MyFriendObject();
                        obj2.SetActive(true);
                        obj2.transform.GetChild(1).GetComponent<Text>().text = item.Username;
                        obj2 .GetComponent<Playfab_User>().userAccountInfo.PlayFabId=item.FriendPlayFabId;
                        FriendlyChallengeManager.instance.Photonfriends.Add(item.FriendPlayFabId);
                        break;
                }
            }
           // MyFriendObject().SetActive(true);
           // MyFriendObject().transform.GetChild(1).GetComponent<Text>().text = item.TitleDisplayName;
        }
    }
    public GameObject MyFriendObject()
    {
        for (int i = 0; i < myfriends.Count; i++)
        {
            if (!myfriends[i].activeInHierarchy)
            {
                print("set Friend object");
                return myfriends[i];
            }
        }
        var obj = Instantiate(myFriendPrefab, Vector3.zero, Quaternion.identity);
        obj.transform.parent = myFriendParent.transform;
        return obj;
    }
    // search user ----------------
    bool searchFriend = true;
    public void OnSearch()
    {
        if (searchFriend)
        {

        }
        else
        {
            searchUserPanel.SetActive(true);
            requestsPanel.SetActive(false);
            sendRequestPanel.SetActive(false);
            print("send user search req");
            GetAccountInfoRequest req = new GetAccountInfoRequest { Username = searchField.text };
            PlayFabClientAPI.GetAccountInfo(req, OnGetUserSuccess, OnError);
        }
    }
    public void OnGetUserSuccess(GetAccountInfoResult result)
    {
        print("user search success");
        searchUserObj.SetActive(true);
        searchUserObj.transform.GetChild(2).gameObject.SetActive(true);
        searchUserObj.transform.GetChild(3).gameObject.SetActive(false);
        searchUserObj.GetComponent<Playfab_User>().userAccountInfo = result.AccountInfo;
        searchUserObj.transform.GetChild(1).GetComponent<Text>().text = result.AccountInfo.Username;
        //SearchUserObject().SetActive(true);
        //SearchUserObject().transform.GetChild(1).GetComponent<Text>().text = result.AccountInfo.Username;
    }
    public GameObject SendReqObject()
    {
        for (int i = 0; i < requestees.Count; i++)
        {
            if (!requestees[i].activeInHierarchy)
            {
                print("set requestee object");
                return requestees[i];
            }
        }
        var obj = Instantiate(requesteePrefab, Vector3.zero, Quaternion.identity);
        obj.transform.parent = requesteeParent.transform;
        return obj;
    }
    public void OnError(PlayFabError error)
    {
        error.GenerateErrorReport();
        Debug.LogError(error.Error);
    }
    public GameObject reciveReqObject()
    {
        for (int i = 0; i < requesters.Count; i++)
        {
            if (!requesters[i].activeInHierarchy)
            {
                print("set requester object");
                return requesters[i];
            }
        }
        var obj = Instantiate(requesterPrefab, Vector3.zero, Quaternion.identity);
        obj.transform.parent = requesterParent.transform;
        return obj;
    }
    /////   send and cancel requests
    public void SendReq(bool send, string id)
    {
        if (!send)
        {
            ExecuteCloudScriptRequest req2 = new ExecuteCloudScriptRequest { FunctionName = "DenyFriendRequest", FunctionParameter = id };
            PlayFabClientAPI.ExecuteCloudScript(req2, OnCancelReq, OnError);
            return;
        }
        ExecuteCloudScriptRequest req = new ExecuteCloudScriptRequest { FunctionName = "SendFriendRequest", FunctionParameter = id };
        PlayFabClientAPI.ExecuteCloudScript(req, OnSendReq, OnError);
    }

    public void OnSendReq(ExecuteCloudScriptResult result)
    {
        print(result.FunctionResult);
        print(result.FunctionName);
        UpdateFriendList();
        print("request send");
        UpdateFriendList();
    }
    public void OnCancelReq(ExecuteCloudScriptResult result)
    {
        UpdateFriendList();
        print("request cancel");
        UpdateFriendList();
    }

    ///// accept and reject requests 
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SimpleTesting();
        }
    }
    public void SimpleTesting()
    {
        ExecuteCloudScriptRequest req2 = new ExecuteCloudScriptRequest { FunctionName = "hello", FunctionParameter = 123 };
        PlayFabClientAPI.ExecuteCloudScript(req2, test, OnError);
    }
    public void AcceptReq(bool accept, string id)
    {
        if (!accept)
        {
            ExecuteCloudScriptRequest req2 = new ExecuteCloudScriptRequest { FunctionName = "DenyFriendRequest", FunctionParameter = id };
            PlayFabClientAPI.ExecuteCloudScript(req2, OnRejectReq, OnError);
            return;
        }
        ExecuteCloudScriptRequest req = new ExecuteCloudScriptRequest { FunctionName = "AcceptFriendRequest", FunctionParameter = id };
        PlayFabClientAPI.ExecuteCloudScript(req, OnAcceptReq, OnError);
    }

    public void OnAcceptReq(ExecuteCloudScriptResult result)
    {
        print("request accepted");
        UpdateFriendList();
        UpdateFriendList();
    }
    public void OnRejectReq(ExecuteCloudScriptResult result)
    {
        UpdateFriendList();
        print("request rejected");
        UpdateFriendList();
    }
    public void test(ExecuteCloudScriptResult result)
    {
        print(result.Logs);
        print(result.FunctionResult);
    }

}
