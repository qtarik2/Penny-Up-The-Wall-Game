using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script attached to Main Camera Not In Use Now
/// </summary>
public class CameraCoordinates : MonoBehaviour
{
    private Camera MainCamera; //be sure to assign this in the inspector to your main camera
    private Vector2 screenBounds;


    // Use this for initialization
    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Vector3 viewPos = transform.position;
        Vector3 viewPos = transform.position ;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x , screenBounds.x * -1 );
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y , screenBounds.y * -1 );
        //transform.position = viewPos;

    }
}
