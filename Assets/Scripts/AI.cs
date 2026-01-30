using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [Header("Components")]
    public List<Transform> waypoints;
    private NavMeshAgent agent;

    [Header("Variables")]
    public int currentWaypointIndex = 0;
    public float speed = 2f;
    public float stoppingDistance = 2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.speed = speed;
        }

        if (waypoints.Count > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (waypoints.Count == 0) return;

        float distanceToWaypoint = Vector3.Distance(waypoints[currentWaypointIndex].position, transform.position);

        if (distanceToWaypoint <= stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
}