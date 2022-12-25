using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using Photon.Pun;
using Photon.Chat;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;
using PlayFab.AuthenticationModels;
using PlayFab.Events;
using ExitGames.Client.Photon; 

public class FriendlyChallengeManager : MonoBehaviour
{
    public static FriendlyChallengeManager instance; private void Awake(){if(instance==null)instance=this;}
    public List<string> Photonfriends=new List<string>();
    void FriendFinder()
    {
       var f=PhotonNetwork.FindFriends(Photonfriends.ToArray());
    }
}
