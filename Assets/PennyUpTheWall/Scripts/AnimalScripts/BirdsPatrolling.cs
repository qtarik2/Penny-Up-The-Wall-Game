using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsPatrolling : MonoBehaviour
{
   // [SerializeField] [Range(0f, 4f)] float lerpTime;
    public float speed=3; 
    [SerializeField] Transform[] myPositions;
    //private Quaternion _lookRotation;
    //private Vector3 _direction;
    int posIndex;
    
    void Start()
    {
        //myPositions = LevelSelection.instance.birdPoints;
        posIndex = 0;
        transform.LookAt(myPositions[posIndex].transform.position);
    
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(transform.position, myPositions[posIndex].transform.position);
        if(dist<1f)
        {
            IncreaseIndex();
        }
        Patrol();

       
    }

    //public IEnumerator BirdPatrolle()
    //{
    //    yield return new WaitForSeconds(0.01f);
    //    //transform.position = Vector3.Lerp(transform.position, myPositions[posIndex].transform.position, lerpTime * Time.deltaTime);
    //    transform.LookAt(myPositions[posIndex]);
    //    transform.Translate(Vector3.forward * 2.0f * Time.deltaTime);
    //    t = Mathf.Lerp(t, 2.0f, lerpTime * Time.deltaTime);
    //    if (t > .9f)
    //    {
    //        t = 0;
    //        posIndex++;
    //        posIndex = (posIndex >= length) ? 0 : posIndex;
    //        //if(posIndex>=length)
    //        //{
    //        //    posIndex = 0;
    //        //}
    //        //else
    //        //{
    //        //    return;
    //        //}
    //    }
    //    //StartCoroutine(BirdPatrolle());
    //}
    public void IncreaseIndex()
    {
        posIndex++;
        if(posIndex >= myPositions.Length)
        {
            posIndex = 0;
        }
        transform.LookAt(myPositions[posIndex].transform.position);
    }
    public void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
