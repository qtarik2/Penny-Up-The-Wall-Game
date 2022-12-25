using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler instance;
    public AudioSource Music;
  
    [Header("ArmMovement Sounds")]
    public List<AudioClip> armMovmentClips;

    [Header("Lose Sounds Clips")]

    public List<AudioClip> loseClips;

    [Header("Coin Sounds")]

    public List<AudioClip> pannySound;
    public AudioClip clapSound;
    [SerializeField] public BossesData _bossesData;
    private void Awake()
    {
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
    public void WinningClap()
    {
        Music.PlayOneShot(clapSound);
    }
    public void PannySoundPlay()
    {
        if (PlayerPrefs.GetInt("sounds") == 1)
        {
            Music.PlayOneShot(pannySound[Random.Range(0,pannySound.Count)], 1.0f);
        }
        else
        {
            Music.Stop();
            Debug.Log(PlayerPrefs.GetInt("sounds"));
            //soundsHandler.instance.play_stop(PlayerPrefs.GetInt("sounds"));
        }
    }
  
    public void ArmMovementSound()
    {
        if (PlayerPrefs.GetInt("sounds") == 1)
        {
            Music.PlayOneShot(armMovmentClips[Random.Range(0, armMovmentClips.Count)], 1.0f);
        }
        else
        {
            Music.Stop();
            //soundsHandler.instance.play_stop(PlayerPrefs.GetInt("sounds"));
        }
          
    }
    public void BossWin_Lose_Sound(int audioIndex)
    {
        AudioClip clip = _bossesData._totalBossesLevel[LevelSelection._clickedLevelNo].
           _eachLevelBosses[_bossesData._totalBossesLevel[LevelSelection._clickedLevelNo]._UnlockedBossNo].bossAudioClips[audioIndex];
        if (clip != null && PlayerPrefs.GetInt("sounds") == 1)
        {
            Music.PlayOneShot(clip);
        }
        else
        {
            Music.Stop();
            //soundsHandler.instance.play_stop(PlayerPrefs.GetInt("sounds"));
        }
           
       
    }
    public void LoseSound()
    {
        Music.PlayOneShot(loseClips[Random.Range(0, loseClips.Count)]);
        Music.priority = 10;
    }

}
