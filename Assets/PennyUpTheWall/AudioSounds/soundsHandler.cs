using System.Collections.Generic;
using UnityEngine;

public class soundsHandler : MonoBehaviour
{
    public AudioSource[] sounds;
    public AudioSource Music;
    [Header("Environment Sounds")]
    public List<AudioClip> environmentSounds;
    public static soundsHandler instance;
    void Awake()
    {
       
        //if(PlayerPrefs.GetInt("sounds")!=0&& PlayerPrefs.GetInt("sounds") != 1)
        //{
        //    PlayerPrefs.SetInt("sounds", 1);
        //}
       
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }


    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.GetInt("sounds") == 0)
        {
            for (int i = 0; i < sounds.Length; i++)
            {

                sounds[i].Stop();
            }
        }
       
    }

 
    public void Sounds_playAndStop()
    {

        if (PlayerPrefs.GetInt("sounds") == 0)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].gameObject.SetActive(false);
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("sounds") == 1)
            {
                for (int i = 0; i < sounds.Length; i++)
                {
                    sounds[i].gameObject.SetActive(true);
                }
                play_stop(PlayerPrefs.GetInt("sounds"));
            }
        }
     

    }
    public void play_stop(int value)
    {
        if (value == 1)
        {
            Music.Play();

        }
        else Music.Stop();

    }
    public void EnvironmentSound()
    {
        Music.GetComponent<AudioSource>().clip = environmentSounds[LevelSelection._clickedLevelNo];
        Music.volume = 0.1f;
        play_stop(PlayerPrefs.GetInt("sounds"));
    }





}
