using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClips : MonoBehaviour
{
    AudioSource clip;
    private void Awake()
    {
        clip = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        clip.Play();
        StartCoroutine(Example());
    }
    bool v;
    IEnumerator Example()
    {
        yield return new WaitUntil(() => !clip.isPlaying);
        clip.volume=1;
        gameObject.SetActive(false);
    }
}
