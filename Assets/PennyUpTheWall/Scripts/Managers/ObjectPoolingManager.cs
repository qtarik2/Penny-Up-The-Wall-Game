using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// //// This script attached to object pooler gameobject
/// In this script i am pooling penny for re-use
/// </summary>
public class ObjectPoolingManager : MonoBehaviour
{

    public List<GameObject> pooledObjects; //All pooled object will be stored in this list
     GameObject objectToPool; //Reference to that prefab which we want to pool
    public int amountToPool; //Enter amount how many times you want to spawn prefab
    public GameObject SpawnPos;

    #region Singleton
    public static ObjectPoolingManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
       
    }
    #endregion

    private void Start()
    {

        //objectToPool = Resources.Load("Penny") as GameObject;
        //pooledObjects = new List<GameObject>(); //create new list of gameobject
        //GameObject tmp;    //create new gameobject
        //for (int i = 0; i < amountToPool; i++) //For loop will instantiate the objectToPool the specified number of times in amountToPool
        //{
        //    tmp = Instantiate(objectToPool, SpawnPos.transform.position, Quaternion.identity, this.transform); ; // assign instantiated prefab to tmp gameobject
        
        //   if(i>0) tmp.SetActive(false); //set active false current instantiated gameobject
        //    pooledObjects.Add(tmp); //add instantiated gameobject to pooledObjects list

        //}


    }

    /// <summary>
    /// /// Call this function from another script where you want to used pooled objects this function return available inActive prefabs in hierarchy
    /// </summary>
    /// <returns></returns>
    public GameObject GetPooledObject()
    {
        for(int i = 0; i <amountToPool; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    /// <summary>
    /// /// write these lines where you want to instantiate object
    /// set transform position and rotations according to yours
    /// </summary>
    /// <returns></returns>
    
        //GameObject penny = ObjectPoolingManager.instance.GetPooledObject();
        //if(penny != null)
        //{
        //    penny.transform.position = this.transform.position;
        //    penny.transform.rotation = this.transform.rotation;
        //    penny.SetActive(true);
        //}
    

}
