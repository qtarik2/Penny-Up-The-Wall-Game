using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour {

	public TextMeshProUGUI Dialogue;
	public string Sentence;
	public GameObject DialoguePanel;
	Animator animator_of_DialoguePanel;
	public Image boss_image_for_load;
	[HideInInspector]
	public string boss_name_for_load;
	public Animator bossesAnimator;
	public GameObject activePanel;
	public GameObject bossTurn;
	public Material blur;
	bool canPlay;
	public bool canAdd;
	bool startFading = false;
	public static DialogueManager instance;
	public GameObject lastBossimage;
	public GameObject touch;
	public List<GameObject> allHand;
	public List<CanvasGroup> Fade;
	public List<string> jwida;
	public List<string> kbira;
	public List<string> esmo;
	public List<TransBoss> trans;


	public int roundnow;
	[HideInInspector]
	public int bossnow;
	[HideInInspector]
	public int levelnow;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;

		}
		animator_of_DialoguePanel = DialoguePanel.GetComponent<Animator>();
	}

	public void AnimationTrue(string name, string Dialog)
	{
		animator_of_DialoguePanel.SetBool(name, true);
		StartCoroutine("wait_for_Complete", Dialog);

	}
	public IEnumerator ActivateAvatars()
	{
		yield return new WaitForSeconds(0.2f);
		UIManager.instance.userImage.gameObject.SetActive(true);
		UIManager.instance.bossImage.gameObject.SetActive(true);

	}
	public void AnimationFasle(string name)
	{
		animator_of_DialoguePanel.SetBool(name, false);
		StartCoroutine(ActivateAvatars());
	}
	public void ShowDialoguePanel()
	{
		StartCoroutine("ShowDialog");
	}
	public void StopDialog()
	{
		StopCoroutine("ShowDialogue");
		//DialoguePanel.GetComponent<Animator> ().SetBool ("Open", false);
	}
	IEnumerator ShowDialog()
	{

		//DialoguePanel.GetComponent<Animator> ().SetBool ("Open", true);
		DialoguePanel.SetActive(true);

		yield return new WaitForSeconds(6f);
		//DialoguePanel.GetComponent<Animator> ().SetBool ("Open", false);
		DialoguePanel.SetActive(false);


	}

    private void Update()
    {
		/*if(roundnow != 1)
        {
			animator_of_DialoguePanel.GetComponent<CanvasGroup>().alpha = 0f;
			Fade[0].alpha = 1f;
			Fade[0].transform.GetChild(0).gameObject.SetActive(true);
			Fade[0].transform.GetChild(1).gameObject.SetActive(true);
        }*/
		
		if (activePanel.activeInHierarchy == true && canAdd)
		{
			canAdd = false;
			roundnow = 0;
			PowerBar.instance.speed = 0.4f;
			StartCoroutine(StartBossing(1.2f));
			allHand.ForEach(h => h.layer = 11);
			Spawner.instance.SpawnedObject.GetComponent<MeshRenderer>().enabled = false;
		}
		if (activePanel.activeInHierarchy == false && !canAdd)
		{
			canAdd = true;
		}

		if (bossTurn.activeInHierarchy == true && canPlay)
		{
			canPlay = false;
			StartCoroutine(StartBossing(0));
		}
		if (bossTurn.activeInHierarchy == false && !canPlay)
		{
			canPlay = true;
		}

		lastBossimage.GetComponent<Image>().sprite = bossesAnimator.GetComponent<SpriteRenderer>().sprite;

		if (startFading) { Fade.ForEach(f => f.alpha = Mathf.Lerp(f.alpha, 1f, Mathf.Sin(0.05f))); blur.SetFloat("_Size", Mathf.Lerp(blur.GetFloat("_Size"), 0f, Mathf.Sin(0.05f))); }
	}

	IEnumerator StartBossing(float wait)
    {
		yield return new WaitForSeconds(wait);

		for (int i = 0; i < esmo.Count; i++)
		{
			if (boss_name_for_load == esmo[i])
			{
				lastBossimage.transform.GetComponent<RectTransform>().anchoredPosition = trans[i].pos;
				lastBossimage.GetComponent<RectTransform>().sizeDelta = trans[i].size;
			}
		}
		Dialogue.text = "";
		roundnow++;
		PowerBar.instance.speed = 0.1f * roundnow;
		animator_of_DialoguePanel.GetComponent<CanvasGroup>().alpha = 1f;
		lastBossimage.transform.localScale = Vector3.one;
		lastBossimage.transform.parent.GetComponent<Image>().enabled = false;
		LoadBossData.instance.SideAnimetionPlay(LoadBossData.instance._bossesData._totalBossesLevel[LoadBossData.instance._level_Number]._eachLevelBosses[LoadBossData.instance._Boss_number].BossDialogue[0]);
		bossesAnimator.Play(boss_name_for_load);
		if (roundnow == 1) { lastBossimage.SetActive(true); }
		else { lastBossimage.SetActive(false); }
		float timeforload = LoadBossData.instance._bossesData._totalBossesLevel[LevelSelection._clickedLevelNo].
		   _eachLevelBosses[LoadBossData.instance._bossesData._totalBossesLevel[LevelSelection._clickedLevelNo]._UnlockedBossNo].bossAudioClips[BossAI.instance.boss_count].length + 3f;
		StartCoroutine(StopBossing(timeforload));
		if (!bossTurn.activeInHierarchy) { StartCoroutine(FadeSound()); Fade.ForEach(f => f.alpha = 0f); Dialogue.text = ""; blur.SetFloat("_Size", 1.2f); }
		touch.GetComponent<Image>().raycastTarget = false;
		startFading = false;
		Dialogue.gameObject.SetActive(false);
	}
	IEnumerator StopBossing(float time)
	{
		yield return new WaitForSeconds(time);
		LoadBossData.instance.SideAnimetionPlayFalse();
		lastBossimage.SetActive(false);
		startFading = true;
		touch.GetComponent<Image>().raycastTarget = true;
		lastBossimage.transform.parent.GetComponent<Image>().enabled = false;
		allHand.ForEach(h => h.layer = 9);
		Spawner.instance.SpawnedObject.GetComponent<MeshRenderer>().enabled = true;
		if (roundnow == 1)
		{
			UIManager.instance.bossImage.fillAmount = 1f;
			UIManager.instance.userImage.fillAmount = 1f;
			Dialogue.gameObject.SetActive(true);
			Dialogue.text = "";
			LoadBossData.instance.SideAnimetionPlay(LoadBossData.instance._bossesData._totalBossesLevel[LoadBossData.instance._level_Number]._eachLevelBosses[LoadBossData.instance._Boss_number].BossDialogue[0]);
			lastBossimage.SetActive(false);
			StartCoroutine(hideit());
		}
	}
	IEnumerator hideit()
    {
		yield return new WaitForSeconds(5f);
		LoadBossData.instance.SideAnimetionPlayFalse();
	}
	IEnumerator FadeSound()
	{
		//text.GetComponent<Text>().enabled = true;
		yield return new WaitForSeconds(1.5f);
		AudioHandler.instance.BossWin_Lose_Sound(BossAI.instance.boss_count);
		//text.GetComponent<Text>().enabled = false;
	}
	public void LoadDialogue(string _Dialogue)
	{
		//StopCoroutine ("loadLetter");

		StartCoroutine("loadLetter", _Dialogue);
		// ShowDialoguePanel();
	}
	IEnumerator loadLetter(string sentence)
	{

		Dialogue.text = "";
		foreach (char letter in sentence.ToCharArray())
		{

			Dialogue.text += letter;
			yield return new WaitForSeconds(.04f);
		}
		Dialogue.text = sentence;
		yield return null;

	}
	public void show(bool value)
	{
		DialoguePanel.SetActive(value);
	}
	public IEnumerator wait_for_Complete(string sentence)
	{
		yield return new WaitForSeconds(2f);

		LoadDialogue(sentence);

	}

	[System.Serializable]
	public class TransBoss
    {
		public Vector3 pos;
		public Vector2 size;
    }


	public void resetInt()
	{
		roundnow = 2;
	}

	public void resetSpeed()
    {
		PowerBar.instance.speed = 0.1f;

	}

}
