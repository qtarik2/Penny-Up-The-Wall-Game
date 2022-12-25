using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTrajectory : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    [Range(3, 30)]
    private int lineSegmentCount = 20;

    private List<Vector3> linePoints = new List<Vector3>();

    #region Singleton
    public static UpdateTrajectory instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    private void Start()
    {
        lineRenderer = FindObjectOfType<LineRenderer>();
    }

    public void UpdateTrajectoryFunction(Vector3 forceVector, Rigidbody rigidBody, Vector3 startingPoint)
    {
        Vector3 velocity = (forceVector / rigidBody.mass) * Time.fixedDeltaTime;

        float FlightDuration = (2 * velocity.y) / Physics.gravity.y;

        float stepTime = FlightDuration / lineSegmentCount;

        linePoints.Clear();

        for (int i = 0; i < lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i; // change in time

            Vector3 MovementVector = new Vector3(

                velocity.x * stepTimePassed,
                velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                velocity.z * stepTimePassed

            );

            linePoints.Add(-MovementVector + startingPoint);
        }
        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());
    }
    public void HideLine()
    {
        lineRenderer.positionCount = 0;
    }
}
