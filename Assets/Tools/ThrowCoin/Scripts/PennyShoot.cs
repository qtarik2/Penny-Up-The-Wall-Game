using System.Collections;
using UnityEngine;

public class PennyShoot : MonoBehaviour
{
    [HideInInspector]
    public Vector3 mousePressDownPos;
    [HideInInspector]
    public Vector3 mouseReleasePos;
    [HideInInspector]
    public Vector3 forceV;
    [HideInInspector]
    public Vector3 Force;

    [HideInInspector]
    public Rigidbody rb;

    [HideInInspector]
    public bool isShoot;

    public TouchInput touchInput;

    public bool up;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        touchInput = FindObjectOfType<TouchInput>();
        touchInput.pennyShoot = this;
    }
    private void LateUpdate()
    {
        if(up && GetComponent<Rigidbody>().velocity.y > -3)
        {
            transform.position += new Vector3(0,0.05f,0);
        }
    }
    public void OnDown()
    {
        mousePressDownPos = Input.mousePosition;
    }
    public void OnDrag()
    {
        Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
        forceV = new Vector3(Mathf.Clamp(forceInit.x, -70, 70), Mathf.Clamp(forceInit.y, -125, 0), Mathf.Clamp(forceInit.y, -125, 0)) * 2f;

        if (!isShoot)
        {
            UpdateTrajectory.instance.UpdateTrajectoryFunction(forceV, rb, transform.position);
            Vector3 velocity = (forceInit / rb.mass) * Time.fixedDeltaTime;
            float FlightDuration = (2 * velocity.y / Physics.gravity.y);
            SpeedoMeter._instance.SetSpeed(FlightDuration * 2);
        }
    }
    public void OnUp()
    {
        UpdateTrajectory.instance.HideLine();
        mouseReleasePos = Input.mousePosition;
        Force = mouseReleasePos - mousePressDownPos;
        touchInput.IsUp = false;
    }

    public void Shoot()
    {
        if (isShoot)
            return;

        Vector3 forcing = new Vector3(Force.x, Force.y, Force.y);
        rb.AddForce(-(forceV / 2) * PowerBar.instance.SetSpeed);
        up = true;
        isShoot = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            rb.isKinematic = true;
        }
    }

}