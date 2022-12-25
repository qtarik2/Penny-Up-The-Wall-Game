using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    #region Singleton 
    public static Spawner instance;
    

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    #endregion

    public GameObject ObjectToSpawn;
    public GameObject SpawnedObject;
    public GameObject SpawnPos,SpawnPos_F;
    public GameObject playerModel;
    public UnityEvent PennyDestroyed;

    public void Start()
    {
            //Check if penny Event is null
            if (PennyDestroyed == null)
                PennyDestroyed = new UnityEvent();
           // ObjectToSpawn = Resources.Load("PennyQM") as GameObject;
            //Register Penny Event
            PennyDestroyed.AddListener(ReGenerateOnDestroy);
            //Call Penny Event
            PennyDestroyed.Invoke();
    }

   public void ReGenerateOnDestroy()
    {
        //Debug.Log("Penny is destroyed Regenerate Again");

        if (PlayerPrefs.GetString("gander") == "female")
        {
            SpawnedObject = Instantiate(ObjectToSpawn, SpawnPos_F.transform.position, SpawnPos_F.transform.rotation, this.transform);
           // SpawnedObject = Instantiate(ObjectToSpawn, SpawnPos_F.transform.position, Quaternion.identity, this.transform);
        }
        else
        {
            SpawnedObject = Instantiate(ObjectToSpawn, SpawnPos.transform.position, SpawnPos.transform.rotation, this.transform);
            //u SpawnedObject = Instantiate(ObjectToSpawn, SpawnPos.transform.position, Quaternion.identity, this.transform);
        }//SpawnedObject.transform.parent = PennyController.instance.HandTransform;
        // this.transform.parent = Spawner.instance.SpawnPos.transform;
        //GameObject penny = ObjectPoolingManager.instance.GetPooledObject();
        //if (penny != null)
        //{
        //    //    penny.transform.position = SpawnPos.transform.position;
        //    //    penny.transform.rotation = SpawnPos.transform.rotation;
        //    penny.SetActive(true);
        //}
    }
}
