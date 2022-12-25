using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour {

	public int levelBosseNow;
	
	public TextMeshProUGUI Dialogue;
	public string Sentence;
	public GameObject DialoguePanel;
	Animator animator_of_DialoguePanel;
	public Image boss_image_for_load;
	public AudioSource boss_audio_for_load;
	//Singleton reference to access this script
	public static DialogueManager instance;
	// Use this for initialization
	void Awake() {
		if (instance == null)
		{
			instance = this;

		}
		animator_of_DialoguePanel = DialoguePanel.GetComponent<Animator>();
	}
    public void AnimationTrue(string name ,string Dialog) {
		animator_of_DialoguePanel.SetBool(name, true);
		UIManager.instance.userImage.gameObject.SetActive(false);
		UIManager.instance.bossImage.gameObject.SetActive(false);
		StartCoroutine("wait_for_Complete",Dialog);
		
	}
	public IEnumerator ActivateAvatars()
    {
		yield return new WaitForSeconds(0.8f);
		UIManager.instance.userImage.gameObject.SetActive(true);
		UIManager.instance.bossImage.gameObject.SetActive(true);

	}
	public void AnimationFasle(string name)
	{
		animator_of_DialoguePanel.SetBool(name, false);
		StartCoroutine(ActivateAvatars());
	}
	public void ShowDialoguePanel(){
		StartCoroutine ("ShowDialog");
	}
	public void StopDialog(){
		StopCoroutine ("ShowDialogue");
		//DialoguePanel.GetComponent<Animator> ().SetBool ("Open", false);
	}
	IEnumerator ShowDialog(){

		//DialoguePanel.GetComponent<Animator> ().SetBool ("Open", true);
		DialoguePanel.SetActive(true);
		
		yield return new WaitForSeconds (6f);
		//DialoguePanel.GetComponent<Animator> ().SetBool ("Open", false);
		DialoguePanel.SetActive(false);
		

	}
	public void LoadDialogue(string _Dialogue){
		//StopCoroutine ("loadLetter");

		StartCoroutine ("loadLetter",_Dialogue);
       // ShowDialoguePanel();
	}
	IEnumerator loadLetter(string sentence){

		Dialogue.text = "";
		foreach (char letter in sentence.ToCharArray()) {

			Dialogue.text += letter;
			yield return new WaitForSeconds (.04f);
		}
		Dialogue.text=sentence;
		yield return null;
		
	}
	public void show(bool value) {
		DialoguePanel.SetActive(value);
	}
	public IEnumerator wait_for_Complete(string sentence) { 
		yield return new WaitForSeconds(2f);
		
		LoadDialogue(sentence);
		
	}
}
