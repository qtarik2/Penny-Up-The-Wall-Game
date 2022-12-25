using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script attached to wind Dial Panel
/// </summary>
public class Compass : MonoBehaviour
{
    public Transform playerTransform;
    Vector3 dir;
    int count = 4;
    bool dre = true;
    private void Update()
    {
        dir.z = playerTransform.eulerAngles.y;
        transform.localEulerAngles = dir;
        if (dre == true)
        {
            dre = false;
            changeAngle();
        }
    }

    void changeAngle()
    {
        //playerTransform.transform.rotation = Random.rotation;
        if (count <= 5)
        {
            count++;
            playerTransform.transform.Rotate(new Vector3(0f, 360f, 0f) * (Time.deltaTime));
        }
        else if (count > 5 && count <= 20)
        {
            count++;
            playerTransform.transform.Rotate(new Vector3(0f, 350f, 0f) * (-Time.deltaTime));
            if (count == 20)
            {
                count = 0;
            }
        }
        Invoke("wait", 10f);
        /* var euler = transform.eulerAngles;
         euler.z = Random.Range(0.0f, 360.0f);
         transform.eulerAngles = euler;*/
    }
    void wait()
    {
        dre = true;
        print("Complete");
    }
}
