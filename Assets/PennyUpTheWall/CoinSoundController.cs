using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CoinSoundController : MonoBehaviour
{
    public static CoinSoundController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public List<AudioSource> audioSources = new List<AudioSource>();
    [Header("AudioSource Prefab")]
    public AudioSource clip;
    public float volume,pitchMin,pitchMax;
    public LayerMask ignoreLayer;
    public string ignoreTag;
    public void PlayCoinSound(LayerMask Colliderlayer)
    {
        //if (ignoreLayer.value==Colliderlayer.value)
        if(((1<<Colliderlayer) & ignoreLayer) == 0)
        {
            PoolAudioObject();
        }
    }
    public void PlayCoinSound(string ignoretag)
    {
        if (ignoreTag == ignoretag)
        {
            return;
        }
            PoolAudioObject();
    }
    public void PlayCoinSound()
    {
        PoolAudioObject();
    }
    public AudioSource PoolAudioObject()
    {
        foreach (var item in audioSources)
        {
            if(!item.gameObject.activeInHierarchy)
            {
                //item.pitch=Random.Range(pitchMin,pitchMax);
                item.gameObject.SetActive(true);
                return item;
            }
            //return (item.gameObject.activeInHierarchy)? item:Instantiate(new GameObject("audio").AddComponent<AudioSource>(),transform.position,Quaternion.identity);
        }
        // var obj = Instantiate(clip, transform.position, Quaternion.identity);
        // obj.pitch=Random.Range(pitchMin,pitchMax);
        // obj.gameObject.SetActive(true);
        // obj.transform.parent = transform;
        // audioSources.Add(obj);
        return null;
    }
}
