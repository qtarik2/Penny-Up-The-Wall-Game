using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandSelection : MonoBehaviour
{
    public HandData _hand_textur;
    public GameObject player_hand_M, player_hand_F;
    public GameObject male_hand, Female_hand;
    GameObject All_Player_Side;
    GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        panel = this.gameObject;
        PlayerPrefs.SetString("gander", "male");
        All_Player_Side = GameObject.FindGameObjectWithTag("All_Players");
        UIManager.instance.Music_on_Off(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Gender(string gander) {
        string Enter_value;
        Enter_value = gander;
        print("Gander Value"+Enter_value);
        if (Enter_value.Equals("male")) {
            PlayerPrefs.SetString("gander", "male");
            print(" Player Gander  "+PlayerPrefs.GetString("gander"));
        }
        else
            if (Enter_value.Equals("female")) {
            PlayerPrefs.SetString("gander", "female");
            print(" Player Gander  " + PlayerPrefs.GetString("gander"));
        }
    }
    public void handed(string handed) {
            PlayerPrefs.SetString("Handed",handed);
        if (handed== "Left") {
            All_Player_Side.transform.localScale = new Vector3(-1f,1f,1f);
            GameObject.FindGameObjectWithTag("All_Players").transform.localScale= All_Player_Side.transform.localScale;
        }
        else
            if (handed== "Right") {
            All_Player_Side.transform.localScale = new Vector3(1f, 1f, 1f);
            GameObject.FindGameObjectWithTag("All_Players").transform.localScale = All_Player_Side.transform.localScale;

        }
    }
    public void hand_texture(int hand_number) {
        string gander = PlayerPrefs.GetString("gander");
        print("Gander Value" + PlayerPrefs.GetString("gander"));
        if (gander.Equals("male")) {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("gander"),hand_number);
            print("Hand Number   " + PlayerPrefs.GetInt(PlayerPrefs.GetString("gander")));
            player_hand_M.GetComponent<SkinnedMeshRenderer>().materials=_hand_textur._HandData[PlayerPrefs.GetInt(PlayerPrefs.GetString("gander"))]._handMaterial;
            Debug.Log(PlayerPrefs.GetInt(PlayerPrefs.GetString("gander")));
            panel.SetActive(false);
            UIManager.instance.SetActivePanel(4);
            male_hand.SetActive(true);
            Female_hand.SetActive(false);
            Spawner.instance.enabled = true;
        }
        else
            if (gander.Equals("female")) { 
        
            PlayerPrefs.SetInt(PlayerPrefs.GetString("gander"),hand_number);
            print("Hand Number   " + PlayerPrefs.GetInt(PlayerPrefs.GetString("gander")));
            player_hand_F.GetComponent<SkinnedMeshRenderer>().material = _hand_textur._HandData[PlayerPrefs.GetInt(PlayerPrefs.GetString("gander"))]._handMaterial[0];
            panel.SetActive(false);
            UIManager.instance.SetActivePanel(4);
            Female_hand.SetActive(true);
            male_hand.SetActive(false);
            Spawner.instance.enabled = true;

        }
    
    
    }
    
}
