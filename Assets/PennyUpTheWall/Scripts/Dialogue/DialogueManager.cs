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

public class DialogueManager : MonoBehaviour
{

	public DownloadSpritesData downloadData;
	public TextMeshProUGUI Dialogue;
	public string Sentence;
	public GameObject DialoguePanel;
	Animator animator_of_DialoguePanel;
	public Image boss_image_for_load;
	[HideInInspector]
	public string boss_name_for_load;
	public SpriteRenderer bossesSprite;
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
			if (roundnow == 0) { StartCoroutine(StartBossing(1.2f)); }
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
			if (roundnow == 0)
			{
				StartCoroutine(StartBossing(0));
			}
		}
		if (bossTurn.activeInHierarchy == false && !canPlay)
		{
			canPlay = true;
		}

		lastBossimage.GetComponent<Image>().sprite = bossesSprite.sprite;

		if (startFading) { Fade.ForEach(f => f.alpha = Mathf.Lerp(f.alpha, 1f, Mathf.Sin(0.05f))); blur.SetFloat("_Size", Mathf.Lerp(blur.GetFloat("_Size"), 0f, Mathf.Sin(0.05f))); }
	}

	public SpriteRenderer output;
	[Range(0.5f, 1.5f)] public float secondsToWait = 1f;
	int currentSprite;
	float wait;
	bool stopped = false;

	public void AnimateBoss(bool reset = false)
	{
		wait = secondsToWait * 0.1f;

		if (reset)
		{
			currentSprite = 0;
		}

		stopped = false;
		output.enabled = true;

		if (LevelSelection.instance.level[bossnow].Boss.Count > 1)
		{
			Animate();
		}
		else if (LevelSelection.instance.level[bossnow].Boss.Count > 0)
		{
			output.sprite = Sprite.Create(LevelSelection.instance.level[bossnow].Boss[0], new Rect(0, 0, LevelSelection.instance.level[bossnow].Boss[0].width, LevelSelection.instance.level[bossnow].Boss[0].height), new Vector2(0.5f, 0.5f), 100);
		}
	}

	public virtual void Animate()
	{
		CancelInvoke("Animate");
		if (currentSprite >= LevelSelection.instance.level[bossnow].Boss.Count - 1)
		{

			stopped = true;
			print($"<color=#00FF00>Animation Completed</color>");

		}

		if (currentSprite < LevelSelection.instance.level[bossnow].Boss.Count - 1) { output.sprite = Sprite.Create(LevelSelection.instance.level[bossnow].Boss[currentSprite], new Rect(0, 0, LevelSelection.instance.level[bossnow].Boss[currentSprite].width, LevelSelection.instance.level[bossnow].Boss[currentSprite].height), new Vector2(0.5f, 0.5f), 100); }

		if (!stopped)
		{
			currentSprite++;
		}

		if (!stopped && wait > 0)
		{
			Invoke("Animate", wait);
		}
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
		PowerBar.instance.speed = 0.2f * roundnow;
		animator_of_DialoguePanel.GetComponent<CanvasGroup>().alpha = 1f;
		lastBossimage.transform.localScale = Vector3.one;
		lastBossimage.transform.parent.GetComponent<Image>().enabled = false;
		if (roundnow == 1) { LoadBossData.instance.SideAnimetionPlay(LoadBossData.instance._bossesData._totalBossesLevel[LoadBossData.instance._level_Number]._eachLevelBosses[LoadBossData.instance._Boss_number].BossDialogue[0]); }
		AnimateBoss(true);
		if (roundnow == 1) { lastBossimage.SetActive(true); }
		else { lastBossimage.SetActive(false); }
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
		StartDownloadingNextBoss();
		if (roundnow == 1) { LoadBossData.instance.SideAnimetionPlayFalse(); }
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
		currentSprite = LevelSelection.instance.level[bossnow].Boss.Count - 1;
	}

	public void StartDownloadingNextBoss()
	{
		LevelSelection.instance.level.ForEach(l => l.Boss.Clear());
		nbFile = 0;
		if (bossnow < 5) { DownloadBosses(bossnow + 1); }
	}

	public void countNB()
	{
		nbFile = texturesURL.Count;
	}

	public List<string> texturesURL = new List<string>();
	public List<int> listCalc = new List<int>();
	private void DownloadBosses(int boss)
	{
		string gameServerURL = downloadData.URL + "/" + downloadData.Prefix + "/Level" + (levelnow + 1) + "/Boss" + (boss + 1);

		Debug.Log("Downloading images from game server");

		texturesURL.Clear();
		listCalc.Clear();

		nbFile = 0;

		string url = gameServerURL.Replace(downloadData.URL, "");

		string pre = url.Substring(1);

		var request = new ListObjectsRequest()
		{
			BucketName = downloadData.S3BucketName,
			Prefix = (pre + "/")
		};

		s3Client.ListObjectsAsync(request, (responseObject) =>
		{
			if (responseObject.Exception == null)
			{
				responseObject.Response.S3Objects.ForEach((o) =>
				{

					string imageFileName = string.Format("{0}\n", o.Key.Substring(o.Key.LastIndexOf("/") + 1));
					imageFileName = imageFileName.Remove(imageFileName.Length - 1);

					string imageURL = gameServerURL + "/" + imageFileName;
					texturesURL.Add(imageURL);

					string[] digits = Regex.Split(imageURL, @"\D+");
					int allcalc = 0;
					foreach (string value in digits)
					{
						int number;
						if (int.TryParse(value, out number))
						{
							allcalc = allcalc + number;
						}
					}

					listCalc.Add(allcalc);

				});

				SetAllListOrd(texturesURL, listCalc);
			}
			else
			{
				//Error From US
			}
		});
	}
	public List<string> correct = new List<string>();
	public void SetAllListOrd(List<string> listURL, List<int> listCalc)
	{

		correct.Clear();

		for (int i = 0; i < listCalc.Count; i++)
		{
			correct.Add("");
		}

		for (int i = 0; i < listCalc.Count; i++)
		{
			int diff = listCalc[i];
			int checkmin = 0;
			for (int j = 0; j < listCalc.Count; j++)
			{
				if (diff < listCalc[j]) { checkmin++; }
			}
			correct[(listCalc.Count - 1) - checkmin] = listURL[i];
		}

		for (int i = 0; i < listCalc.Count; i++)
		{
			texturesURL[i] = correct[i];
		}

		StartCoroutine(StartDownloadTexture());
	}

	IEnumerator StartDownloadTexture()
	{
		yield return new WaitForSeconds(0.5f);

		DownloadTexture(texturesURL[nbFile], texturesURL);
	}

	public void DownloadTexture(string url, List<string> urlList)
	{
		WebRequests.GetTexture(
			url,
			(string error) =>
			{
				Debug.Log("Error: " + error);
			},
			(Texture2D texture, byte[] bytes) =>
			{
				if (nbFile < urlList.Count)
				{
					LevelSelection.instance.level[bossnow + 1].Boss.Add(texture);
					DownloadTexture(urlList[nbFile], urlList);
					nbFile++;
				}
			}
		);
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

}
