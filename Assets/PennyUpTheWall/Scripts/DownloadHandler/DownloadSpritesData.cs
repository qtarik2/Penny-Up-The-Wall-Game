using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadSpritesData : ScriptableObject
{


    [Header("URL Serveur")]
    public string URL = "https://pennyupthewall.s3.eu-west-1.amazonaws.com";
    public string Prefix = "Sprites/Bosses";
    public string S3BucketName = "pennyupthewall";


}
