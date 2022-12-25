using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
/// <summary>
/// This script attached to Hand 
/// </summary>
public class QMHandControlScript : MonoBehaviour,IPunInstantiateMagicCallback
{
    public static QMHandControlScript instance; private void Awake() {instance=this;}
    public GameObject PennyPrefab;
    public GameObject Hand_M_F;
    public Transform HandfixPoint;
    public void AnimationEventStart()
    {
        //this.gameObject.GetComponent<Animator>().enabled = false;
            // PennyPrefab = QMSpawner.instance.Penny;
            // PennyPrefab.GetComponent<PennyControllerQM>().OnAnimationEventTrigger();
            PennyControllerQM.instance.OnAnimationEventTrigger();
    }
    //This function called on hand Animation End event trigger 
    public void AnimationEventEnds()
    {
        if(PennyControllerQM.instance.photonView.IsMine)
        {
            GetComponent<Animator>().SetBool("HandAnimbool", false);
            GetComponent<Animator>().Play("idle");
            Debug.Log("Animation event is Ended ");
        }
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if(info.photonView.IsMine)
        {
            print("spawn penny call");
            QMSpawner.instance.SpawnPenny(HandfixPoint);
        }
        print("instantiate call back work"+info+info.photonView+info.Sender.TagObject+info.photonView.name);
    }
}
