using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSwing : MonoBehaviour
{
    [SerializeField]private float sliderLastX = 1f;
    public Transform customPivot;

    private void FixedUpdate()
    {

        //Quaternion localRotation = Quaternion.Euler(sliderX - sliderLastX, 0f, 0f);
        transform.RotateAround(customPivot.position, Vector3.up, 20 * Time.deltaTime);
        //sliderLastX = sliderX;
    }
}
