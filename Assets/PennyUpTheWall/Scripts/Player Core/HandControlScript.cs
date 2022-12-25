using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script attached to Hand 
/// </summary>
public class HandControlScript : MonoBehaviour
{
    public GameObject PennyPrefab;
    public GameObject Hand_M_F;

    //This function called on hand Animation event trigger
    public void AnimationEventStart()
    {
        UIManager.instance._swipeHand.gameObject.SetActive(false);
        //this.gameObject.GetComponent<Animator>().enabled = false;
        PennyPrefab = Spawner.instance.SpawnedObject;
        PennyPrefab.GetComponent<PennyController>().OnAnimationEventTrigger();
        Debug.Log("Animation event is Started ");
    }

    //This function called on hand Animation End event trigger 
    public void AnimationEventEnds()
    {
        UIManager.instance._swipeHand.gameObject.SetActive(false);
        Hand_M_F.GetComponent<Animator>().SetBool("HandAnimbool", false);
        Hand_M_F.GetComponent<Animator>().Play("idle");
        Debug.Log("Animation event is Ended ");
        
    }
}
