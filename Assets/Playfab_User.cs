using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
public class Playfab_User : MonoBehaviour
{
    public UserAccountInfo userAccountInfo;
    
    public void SendRequestClick()
    {
        //userAccountInfo.
        PlayFabFriendList.instance.SendReq(true,userAccountInfo.PlayFabId);
    }
    public void CancelRequestClick()
    {
        PlayFabFriendList.instance.SendReq(false,userAccountInfo.PlayFabId);
    }
    public void AcceptRequestClick()
    {
        PlayFabFriendList.instance.AcceptReq(true,userAccountInfo.PlayFabId);
    }
    public void RejectRequestClick()
    {
        PlayFabFriendList.instance.AcceptReq(false,userAccountInfo.PlayFabId);
    }
    public void OnInvite(Toggle toggle)
    {
        PlayFabFriendList.instance.AddInvitedFriendID(toggle.isOn,userAccountInfo.PlayFabId);
    }
}
