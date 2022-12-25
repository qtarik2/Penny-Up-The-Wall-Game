using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour {
	public string _Dialogue;
    

 	public void OnEnable(){
      
		DialogueManager.instance.DialoguePanel.GetComponent<Animator> ().SetBool ("Open", true);
       
        DialogueManager.instance.LoadDialogue (_Dialogue);
	}
	public void OnDisable(){

		if (DialogueManager.instance.DialoguePanel != null) {
			DialogueManager.instance.DialoguePanel.GetComponent<Animator> ().SetBool ("Open", false);
		}
	}
}
