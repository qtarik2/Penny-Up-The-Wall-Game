using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using Amazon.S3.Model;
using Amazon;
using Amazon.S3;
using System.Text.RegularExpressions;
using UnityEngine.Video;
using System;

public class DialogueManager : MonoBehaviour
{
	public List<Level> level;
	public VideoPlayer vP;
	public TextMeshProUGUI Dialogue;
	public string Sentence;
	public GameObject DialoguePanel;
	public RawImage RImage;
	Animator animator_of_DialoguePanel;
	public GameObject activePanel;
	public GameObject bossTurn;
	public Material blur;
	bool canPlay;
	public bool canAdd;
	bool startFading = false;
	public static DialogueManager instance;
	public GameObject touch;
	public List<GameObject> allHand;
	public List<CanvasGroup> Fade;
	public List<string> esmo;
	public List<TransBoss> trans;


	public int roundnow;
	[HideInInspector]
	public int bossnow;
	[HideInInspector]
	public int levelnow;

	int nbFile;
	bool stop;

	AmazonS3Client s3Client;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;

		}
		animator_of_DialoguePanel = DialoguePanel.GetComponent<Animator>();

		UnityInitializer.AttachToGameObject(this.gameObject);
		AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;
		s3Client = new AmazonS3Client(Credentials.UID, Credentials.Secret, RegionEndpoint.EUWest1);
	}

#if UNITY_ANDROID
	public void UsedOnlyForAOTCodeGeneration()
	{
		//Bug reported on github https://github.com/aws/aws-sdk-net/issues/477
		//IL2CPP restrictions: https://docs.unity3d.com/Manual/ScriptingRestrictions.html
		//Inspired workaround: https://docs.unity3d.com/ScriptReference/AndroidJavaObject.Get.html

		AndroidJavaObject jo = new AndroidJavaObject("android.os.Message");
		int valueInt = jo.Get<int>("what");
		string valueString = jo.Get<string>("what");
	}
#endif

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
			roundnow = 0;
			canAdd = false;
			roundnow = 0;
			PowerBar.instance.speed = 0.4f;
			StartCoroutine(StartBossing(1.2f));
			allHand.ForEach(h => h.layer = 11);
			Spawner.instance.SpawnedObject.GetComponent<MeshRenderer>().enabled = false;
			vP.clip = level[levelnow].BossesClips[bossnow];
		}
		if (activePanel.activeInHierarchy == false && !canAdd)
		{
			canAdd = true;
		}

		if (startFading) { Fade.ForEach(f => f.alpha = Mathf.Lerp(f.alpha, 1f, Mathf.Sin(0.05f))); blur.SetFloat("_Size", Mathf.Lerp(blur.GetFloat("_Size"), 0f, Mathf.Sin(0.05f))); }
	}

	IEnumerator StartBossing(float wait)
	{
		yield return new WaitForSeconds(wait);

		/*for (int i = 0; i < esmo.Count; i++)
		{
			if (boss_name_for_load == esmo[i])
			{
				lastBossimage.transform.GetComponent<RectTransform>().anchoredPosition = trans[i].pos;
				lastBossimage.GetComponent<RectTransform>().sizeDelta = trans[i].size;
			}
		}*/
		Dialogue.text = "";
		roundnow++;
		RImage.enabled = true;
		PowerBar.instance.speed = 0.2f * roundnow;
		animator_of_DialoguePanel.GetComponent<CanvasGroup>().alpha = 1f;
		if (roundnow == 1)
		{
			LoadBossData.instance.SideAnimetionPlay(LoadBossData.instance._bossesData._totalBossesLevel[LoadBossData.instance._level_Number]._eachLevelBosses[LoadBossData.instance._Boss_number].BossDialogue[0]);
			vP.Play();
		}
		float timeforload = LoadBossData.instance._bossesData._totalBossesLevel[LevelSelection._clickedLevelNo].
		   _eachLevelBosses[LoadBossData.instance._bossesData._totalBossesLevel[LevelSelection._clickedLevelNo]._UnlockedBossNo].bossAudioClips[BossAI.instance.boss_count].length + 3f;
		StartCoroutine(StopBossing(4f));
		if (!bossTurn.activeInHierarchy) { StartCoroutine(FadeSound()); Fade.ForEach(f => f.alpha = 0f); Dialogue.text = ""; blur.SetFloat("_Size", 1.2f); }
		touch.GetComponent<Image>().raycastTarget = false;
		startFading = false;
		Dialogue.gameObject.SetActive(false);
	}
	public IEnumerator StopBossing(float time)
	{
		yield return new WaitForSeconds(time);
		if (roundnow == 1) { LoadBossData.instance.SideAnimetionPlayFalse(); }
		startFading = true;
		RImage.enabled = false;
		touch.GetComponent<Image>().raycastTarget = true;
		allHand.ForEach(h => h.layer = 9);
		Spawner.instance.SpawnedObject.GetComponent<MeshRenderer>().enabled = true;
		if (roundnow == 1)
		{
			vP.Stop();
			UIManager.instance.bossImage.fillAmount = 1f;
			UIManager.instance.userImage.fillAmount = 1f;
			Dialogue.gameObject.SetActive(true);
			Dialogue.text = "";
			LoadBossData.instance.SideAnimetionPlay(LoadBossData.instance._bossesData._totalBossesLevel[LoadBossData.instance._level_Number]._eachLevelBosses[LoadBossData.instance._Boss_number].BossDialogue[0]);
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
		roundnow = 1;
		PowerBar.instance.speed = 0.2f;
		Time.timeScale = 1;
	}

	public void resetSpeed()
	{
		PowerBar.instance.speed = 0.2f;

	}

	[Serializable]
	public class Level
	{
		public List<VideoClip> BossesClips;
	}

}
