using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ///This script attached to penny prefab located in Assets/Resources folder
/// </summary>
public class DrawTrajectory : MonoBehaviour
{
    //public LineRenderer _LineRendrer;

    // [SerializeField] [Range(3, 30)] private int _LineSegmentCount = 20;

    // private List<Vector3> _LinePoints = new List<Vector3>();

    // public static float speedValue = 0;

    #region Singleton

    public static DrawTrajectory instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion


    // private void Start()
    // {
    //     _LineRendrer = GameObject.Find("Line").GetComponent<LineRenderer>();
    //     //_LineRendrer = this.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
    // }

    // public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidbody, Vector3 startingPoint)
    // {
    //     Vector3 velocity = (forceVector / rigidbody.mass) * Time.fixedDeltaTime;

    // //    Debug.Log("Tranjectory Velocity " + velocity);

    //     float FlightDuration = (2 * velocity.y / Physics.gravity.y);

    //     //Debug.Log("Tranjectory FlighDuration " + FlightDuration);

    //     float stepTime = FlightDuration / _LineSegmentCount;


    //     //Debug.Log("Tranjectory StepTime " + stepTime);

    //     _LinePoints.Clear();



    //     for (int i = 0; i < _LineSegmentCount; i++)
    //     {
    //         float stepTimePassed = stepTime * i; // change in time
    //         Vector3 MovementVector = new Vector3(

    //             velocity.x * stepTimePassed,
    //             velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
    //             velocity.z * stepTimePassed
    //             );

    //         _LinePoints.Add(-MovementVector + startingPoint);
    //     }
    //     if (_LineRendrer == null) _LineRendrer = this.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
    //     _LineRendrer.positionCount = _LinePoints.Count;
    //     _LineRendrer.SetPositions(_LinePoints.ToArray());

    //     speedValue = FlightDuration;
    //     //set value in speedmeter 
    //     //SpeedoMeter._instance.SetSpeed(speedValue);

    //     //Debug.Log("Tranjectory StepTime " + speedValue);
    // }


    //public void HideLine()
    // {
    //     if (_LineRendrer == null) _LineRendrer = this.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
    //     _LineRendrer.positionCount = 0;
    // }

    // Start is called before the first frame update
    // Line to Draw Trajectory
    private LineRenderer lineRenderer;
    //To Set The Size Of Line
    public int lineSegments;
    List<Vector3> linePoints = new List<Vector3>();

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    public void UpdateTrajectory(Vector3 forceVectory, Rigidbody Rb, Vector3 startingPoint)
    {
        Vector3 velocity = (forceVectory / Rb.mass) * Time.fixedDeltaTime;
        float flightDuration = (2 * velocity.y) / Physics.gravity.y;
        float stopTime = flightDuration / lineSegments;
        linePoints.Clear();
        linePoints.Add(startingPoint);
        for (int i = 1; i < lineSegments; i++)
        {
            float stopTimepassed = stopTime * i;
            Vector3 movementVector = new Vector3
            (
               velocity.x * stopTimepassed,
               velocity.y * stopTimepassed - .5f * Physics.gravity.y * stopTimepassed * stopTimepassed,
               velocity.z * stopTimepassed
            );
            Vector3 NewPointOnLine = -movementVector + startingPoint;
            //For Detection off line with objects
            RaycastHit hit;

            if (Physics.Raycast(linePoints[i - 1], NewPointOnLine - linePoints[i - 1], out hit, (NewPointOnLine - linePoints[i - 1]).magnitude))
            {
                linePoints.Add(hit.point);
                break;
            }
            linePoints.Add(NewPointOnLine);
        }
        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());
    }
    public void HideLine()
    {
        lineRenderer.positionCount = 0;
    }
}
