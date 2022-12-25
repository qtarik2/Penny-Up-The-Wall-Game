using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteC : MonoBehaviour
{
    public List<GameObject> next;
    public List<GameObject> prev;

    bool wasClick = false;

    private void Update()
    {
        if(GameManager.instance.gameStates == GameStates.Playing)
        {
            GetComponent<Animator>().SetBool("PlayAnimation", true);
        }

        if (PlayerPrefs.GetInt("WindEdu") == 1) { Destroy(gameObject); }
    }

    public void btnClick()
    {
        if (wasClick) { PlayerPrefs.SetInt("WindEdu", 1); }
        else
        {
            next.ForEach(n => n.SetActive(true));
            prev.ForEach(p => p.SetActive(false));
        }

        wasClick = true;
    }

}
