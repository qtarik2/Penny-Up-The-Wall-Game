using UnityEngine;
using UnityEngine.AI;

public class BullPatrolling : MonoBehaviour
{
    private int destPoint = 0;
    public GameObject[] waypoints;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.3f)
            GotoNextPoint();
    }
    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (waypoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.Warp(transform.position);
        agent.destination = waypoints[destPoint].transform.position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % waypoints.Length;
    }

}
