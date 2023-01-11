using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.Text.RegularExpressions;

public class DownloadFirstBosses : MonoBehaviour
{
    [Header("Downloaded Data")]
    public DownloadSpritesData downloadData;

    [HideInInspector]
    public List<string> LastFileNameOfBosses1 = new List<string>();

    [Header("Referances")]
    public Animator animator;
    public Text infoDownload;
    public Text percentage;
    public List<Slider> sliderDownload;
    public List<Animation> fade;

    int checkIfAllDownloaded;
    float MBDownloaded;

    bool Done;

    public List<string> resultatImageName = new List<string>();
    private List<int> calculateOrdList = new List<int>();
    private List<string> correctOrdList = new List<string>();

    private List<string> ListUrl = new List<string>();
    private List<string> listPath = new List<string>();

    int nextDownload;
    int nblast;

    AmazonS3Client s3Client;

    float smoothingCalc;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;
        s3Client = new AmazonS3Client(Credentials.UID, Credentials.Secret, RegionEndpoint.EUWest1);


        Debug.Log(Application.persistentDataPath);
        infoDownload.enabled = false;
        nextDownload = 0;
        StartCoroutine(StartWait());
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

    IEnumerator StartWait()
    {
        yield return new WaitForSeconds(2);
        animator.Play("GettingStarted");

        StartCoroutine(CheckInternet());
    }

    IEnumerator CheckInternet()
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();

        if(request.error != null) { animator.Play("NoInternet"); fade.ForEach(f => f.Play()); }
        else { GetLastImageNameOfEveryBoss1AtEveryLevel(downloadData.Prefix + "/Level" + (nblast + 1) + "/Boss1/"); }

    }

    IEnumerator CheckAndDownloadBoss()
    {
        yield return new WaitForSeconds(2f);

        animator.Play("ProcessingFile");

        for (int i = 0; i < LastFileNameOfBosses1.Count; i++)
        {
            string path = (Application.persistentDataPath + "/BossesSprites" + "/Level" + (i + 1) + "/Boss1");
            if (!File.Exists(path + "/" + LastFileNameOfBosses1[i]))
            {
                if (LastFileNameOfBosses1[i] != "")
                {
                    if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
                    string url = downloadData.Prefix + "/Level" + (i + 1) + "/" + "Boss1/";
                    ListUrl.Add(url);
                    listPath.Add(path);
                    print($"<color=#FF0000>Level </color>" + (i + 1) + $"<color=#FF0000> Not Downloaded</color>");
                    print($"<color=#FFFF00>Start Downloading </color>" + $"<color=#FFFF00>Level </color>" + (i + 1));
                }
            }
            else
            {
                print($"<color=#00FF00>Level </color>" + (i + 1) + $"<color=#00FF00> Already Downloaded</color>");
                checkIfAllDownloaded++;
            }
        }

        StartCoroutine(StartDownload());
    }

    IEnumerator StartDownload()
    {
        yield return new WaitForSeconds(3f);

        if (checkIfAllDownloaded != 15)
        {
            DownloadOneByOne();
            infoDownload.enabled = true;
        }
    }

    public void DownloadOneByOne()
    {
        resultatImageName.Clear();
        if (nextDownload < ListUrl.Count) { DownloadBoss(ListUrl[nextDownload], listPath[nextDownload]); }
    }

    public void NoInternetOrError(bool isInternet)
    {
        if (isInternet) { animator.Play("NoInternetExit"); }
        if (!isInternet) { animator.Play("ErrorExit"); }

        fade.ForEach(f => f.Stop());

        LastFileNameOfBosses1.Clear();
        resultatImageName.Clear();
        resultatImageName.Clear();
        calculateOrdList.Clear();
        correctOrdList.Clear();
        listPath.Clear();
        listPath.Clear();
        nextDownload = 0;
        nblast = 0;
        checkIfAllDownloaded = 0;
        MBDownloaded = 0;
        Done = false;

        StartCoroutine(StartWait());
    }

    public void LoadBosses(string url, string path, bool isBatchingLastNameOfBosses, bool isDownload)
    {
        calculateOrdList.Clear();
        resultatImageName.Clear();

        var request = new ListObjectsRequest()
        {
            BucketName = downloadData.S3BucketName,
            Prefix = url
        };

        s3Client.ListObjectsAsync(request, (responseObject) =>
        {
            if (responseObject.Exception == null)
            {
                responseObject.Response.S3Objects.ForEach((o) =>
                {
                    string textName = string.Format("{0}\n", o.Key.Substring(o.Key.LastIndexOf("/") + 1));
                    textName = textName.Remove(textName.Length - 1);
                    
                    resultatImageName.Add(textName);

                    if (isBatchingLastNameOfBosses)
                    {
                        string[] digits = Regex.Split(textName, @"\D+");
                        int allcalc = 0;
                        foreach (string value in digits)
                        {
                            int number;
                            if (int.TryParse(value, out number))
                            {
                                allcalc = allcalc + number;
                            }
                        }
                        calculateOrdList.Add(allcalc);
                    }
                });

                if (isDownload) { DownloadTexture(url, path, 0); }
            }
            else
            {
                animator.Play("ErrorPlay");
                fade.ForEach(f => f.Play());
            }

            if (isBatchingLastNameOfBosses) { SetOrdForList(isBatchingLastNameOfBosses); }
        });
    }

    public void DownloadBoss(string url, string path)
    {
        LoadBosses(url, path, false, true);
    }

    public void DownloadTexture(string url, string path,int i)
    {
        animator.Play("DownloadingFiles");
        percentage.gameObject.SetActive(true);
        string fileURL = url + resultatImageName[i];

        if (!File.Exists(path + "/" + fileURL.Substring(28)))
        {
            s3Client.GetObjectAsync(downloadData.S3BucketName, fileURL, (responseObj) =>
            {
                var response = responseObj.Response;
                if (response.ResponseStream != null)
                {
                    byte[] bytes = new byte[response.ResponseStream.Length];
                    response.ResponseStream.Read(bytes, 0, bytes.Length);
                    File.WriteAllBytes(path + "/" + fileURL.Substring(28), bytes);
                    string calcMB = ConvertBytesToMegabytes(bytes.Length).ToString("0.00");
                    MBDownloaded = MBDownloaded + float.Parse(calcMB);
                    if (i < this.resultatImageName.Count) { DownloadTexture(url, path, i + 1); }
                }
                else
                {
                    animator.Play("ErrorPlay");
                    fade.ForEach(f => f.Play());
                }
            });
        }
        else
        {
            if (i < this.resultatImageName.Count - 1) { DownloadTexture(url, path, i + 1); }
        }

        if(i == resultatImageName.Count - 1)
        {
            nextDownload++;
            checkIfAllDownloaded++;
            print($"<color=#00FF00>Level </color>" + (checkIfAllDownloaded) + $"<color=#00FF00> Completed Download</color>");
            DownloadOneByOne();
        }
    }

    public void GetLastImageNameOfEveryBoss1AtEveryLevel(string url)
    {
        LoadBosses(url, "Da3", true, false);
    }

    public void SetOrdForList(bool isBatchingLastNameOfBosses)
    {
        correctOrdList.Clear();

        for (int i = 0; i < calculateOrdList.Count; i++)
        {
            correctOrdList.Add("");
        }

        for (int i = 0; i < calculateOrdList.Count; i++)
        {
            int diff = calculateOrdList[i];
            int checkmin = 0;
            for (int j = 0; j < calculateOrdList.Count; j++)
            {
                if (diff < calculateOrdList[j]) { checkmin++; }
            }
            correctOrdList[(calculateOrdList.Count - 1) - checkmin] = resultatImageName[i];
        }

        if (isBatchingLastNameOfBosses)
        {
            LastFileNameOfBosses1.Add(correctOrdList[correctOrdList.Count - 1]);

            nblast++;

            if (nblast < 15)
            {
                GetLastImageNameOfEveryBoss1AtEveryLevel(downloadData.Prefix + "/Level" + (nblast + 1) + "/Boss1/");
            }
            else { StartCoroutine(CheckAndDownloadBoss()); }
        }

    }

    private void Update()
    {
        if (checkIfAllDownloaded != 15)
        {
            if (MBDownloaded < 10) { infoDownload.text = MBDownloaded.ToString("0.0") + "MB"; }
            else if (MBDownloaded < 100 && MBDownloaded >= 10) { infoDownload.text = MBDownloaded.ToString("00.0") + "MB"; }
            else { infoDownload.text = MBDownloaded.ToString("000.0") + "MB"; }
        }

        float calcPercentage = (checkIfAllDownloaded / 15f) * 100;

        percentage.text = calcPercentage.ToString("0") + "%";
        smoothingCalc = Mathf.Lerp(smoothingCalc, (calcPercentage / 100), Mathf.Tan(0.05f));
        sliderDownload.ForEach(s => s.value = smoothingCalc);

        if (checkIfAllDownloaded == 15 && !Done)
        {
            infoDownload.text = "Done";
            StartCoroutine(WaitForLoading());
            Done = true;
        }
    }

    IEnumerator WaitForLoading()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadSceneAsync(1);

        Destroy(gameObject);
    }

    static double ConvertBytesToMegabytes(long bytes)
    {
        return (bytes / 1024f) / 1024f;
    }
}
