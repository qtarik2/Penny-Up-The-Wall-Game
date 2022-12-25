using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class Patrolling : MonoBehaviour
{
    private NavMeshAgent agent;
    GameObject[] waypoints;
    GameObject currntWayPoint;
    public AIState currentstate = AIState.Patrol;
    float targetDist = 1.5f;
    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        StartCoroutine(StatePatroll());
    }
    public IEnumerator StatePatroll()
    {
        waypoints = GameObject.FindGameObjectsWithTag("WayPoint");
        currntWayPoint = waypoints[Random.Range(0, waypoints.Length)];
        while (currentstate == AIState.Patrol)
        {

            agent.Warp(this.transform.position);
            if(agent.isActiveAndEnabled)
            agent.SetDestination(currntWayPoint.transform.position);
            if (Vector3.Distance(transform.position, currntWayPoint.transform.position) < targetDist)
            {
                currntWayPoint = waypoints[Random.Range(0, waypoints.Length)];
            }
            //else if(targetDist>0)
           
                //GameObject player = GameObject.FindGameObjectWithTag("Player");
               // agent.transform.LookAt(player.transform);
           // }
            // yield return null;
            yield return new WaitForSeconds(0.13f);
        }
    }
    public enum AIState
    {
        Patrol
    }

}
