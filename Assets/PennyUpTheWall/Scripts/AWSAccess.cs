/**
user: pennyups3
Access key ID: AKIAXWVDESS775WXRDY4
Secret access key : 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
bucket : pennyupthewall
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon.S3;
using Amazon;
using System;
using Amazon.S3.Model;
using TMPro;
using System.Threading.Tasks;
using System.IO;
using Amazon.Runtime;
using Amazon.CognitoIdentity;

public class AWSAccess : MonoBehaviour
{
    public string IdentityPoolId = "";
    public string CognitoIdentityRegion = RegionEndpoint.EUWest1.SystemName;
    private RegionEndpoint _CognitoIdentityRegion
    {
        get { return RegionEndpoint.GetBySystemName(CognitoIdentityRegion); }
    }
    public string S3Region = RegionEndpoint.EUWest1.SystemName;
    private RegionEndpoint _S3Region
    {
        get { return RegionEndpoint.GetBySystemName(S3Region); }
    }
    public string S3BucketName = null;
    public TMP_Text ResultText = null;

    void Start()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

        GetObjects();
    }

    #region private members

    private IAmazonS3 _s3Client;
    private AWSCredentials _credentials;

    private AWSCredentials Credentials
    {
        get
        {
            if (_credentials == null)
                _credentials = new CognitoAWSCredentials(IdentityPoolId, _CognitoIdentityRegion);
            return _credentials;
        }
    }

    private IAmazonS3 Client
    {
        get
        {
            if (_s3Client == null)
            {
                _s3Client = new AmazonS3Client(Credentials, _S3Region);
            }
            //test comment
            return _s3Client;
        }
    }

    #endregion

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

    public void GetBucketList()
    {
        ResultText.text = "Fetching all the Buckets";
        Client.ListBucketsAsync(new ListBucketsRequest(), (responseObject) =>
        {
            ResultText.text += "\n";
            if (responseObject.Exception == null)
            {
                ResultText.text += "Got Response \nPrinting now \n";
                responseObject.Response.Buckets.ForEach((s3b) =>
                {
                    ResultText.text += string.Format("bucket = {0}, created date = {1} \n", s3b.BucketName, s3b.CreationDate);
                });
            }
            else
            {
                ResultText.text += "Got Exception \n";
            }
        });
    }

    public void GetObjects()
    {
        ResultText.text = "Fetching all the Objects from " + S3BucketName;

        var request = new ListObjectsRequest()
        {
            BucketName = S3BucketName,
            Prefix = "Sprites/Bosses/Level1/Boss1/"
        };

        Client.ListObjectsAsync(request, (responseObject) =>
        {
            ResultText.text += "\n";
            if (responseObject.Exception == null)
            {
                ResultText.text += "Got Response \nPrinting now \n";
                responseObject.Response.S3Objects.ForEach((o) =>
                {
                    GetObject(o.Key);
                });
            }
            else
            {
                ResultText.text += "Got Exception \n";
            }
        });
    }

    private void GetObject(string fileName)
    {
        ResultText.text = string.Format("fetching {0} from bucket {1}", fileName, S3BucketName);
        Client.GetObjectAsync(S3BucketName, fileName, (responseObj) =>
        {
            var response = responseObj.Response;
            if (response.ResponseStream != null)
            {
                byte[] bytes = new byte[response.ResponseStream.Length];
                response.ResponseStream.Read(bytes, 0, bytes.Length);
                File.WriteAllBytes(Application.persistentDataPath + "\\" + fileName.Substring(28), bytes);
            }
        });
    }
}
