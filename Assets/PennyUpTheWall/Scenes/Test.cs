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

public class Test : MonoBehaviour
{
    AmazonS3Client s3Client;

    private void Start()
    {
        UnityInitializer.AttachToGameObject(gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;
        s3Client = new AmazonS3Client(Credentials.UID, Credentials.Secret, RegionEndpoint.EUWest1);

        StartDownloading();
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

    public void StartDownloading()
    {
        Debug.Log("The Downloading is Start");
    }
}
