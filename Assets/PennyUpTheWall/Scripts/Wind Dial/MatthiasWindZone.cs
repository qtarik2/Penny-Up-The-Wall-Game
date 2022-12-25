using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySpace;
public class MatthiasWindZone : MonoBehaviour {

    List<Rigidbody> RigidbodiesInWindZoneList = new List<Rigidbody>();
     Vector3 windDirection ;
    public float windStrength = 5;
    public GameObject wind;
    private void Start()
    {
        
        windDirection = wind.transform.position;
    }
    private void OnTriggerEnter(Collider col)
    {
        windDirection = wind.transform.position;
        Rigidbody objectRigid = col.gameObject.GetComponent<Rigidbody>();
        if(objectRigid != null)
            RigidbodiesInWindZoneList.Add(objectRigid);
    }

    private void OnTriggerExit(Collider col)
    {
        Rigidbody objectRigid = col.gameObject.GetComponent<Rigidbody>();
		if (objectRigid != null)
			RigidbodiesInWindZoneList.Remove(objectRigid);
    }

    private void FixedUpdate()
    {
        if(RigidbodiesInWindZoneList.Count > 0) {
            foreach (Rigidbody rigid in RigidbodiesInWindZoneList)
            {/*
                if (wind.transform.position.z >= 102)
                {
                    rigid.AddForce(windDirection * windStrength);
                }
                else { rigid.AddForce(-(windDirection * windStrength)); } 
                */
                // Quaternion.Euler(windDirection);
                if (wind.transform.eulerAngles.y>=102) { rigid.AddForce(windDirection * windStrength); }
                else { rigid.AddForce(-(windDirection * windStrength)); }
            }
        }
    }
}
