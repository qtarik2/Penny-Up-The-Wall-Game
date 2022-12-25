using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    //This is Used For Boss AI And Matches 
    
    public static BossAI instance;
    public Vector3 player_positin, Boss_position;
    public int Total_count=0,user_count=0,boss_count=0;
    public Vector3 Player_force;
    public bool Boos_Gander;
    private void Start()
    {
        if (instance == null)
            instance = this;


    }
}


