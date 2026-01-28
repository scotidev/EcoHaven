using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [Header("Components")]
    public List<Transform> waypoints;
    public LayerMask playerLayer;
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;
    public GameObject hitbox;

    [Header("Variables")]
    public int currentWaypointIndex = 0;
    public float speed = 2f;
    private bool isPlayerDetected = false;
    private bool onRadious;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = speed;
    }

    void Update()
    {
        if (!isPlayerDetected)
        {
            Patrol();
        }
        else
        {
            StopPatrol();
            animator.SetTrigger("Attack");
        }
    }

    private void Patrol()
    {
        if (waypoints.Count == 0)
        {
            Debug.Log("Travado");
            return;
        }

        float distanceToWaypoint = Vector3.Distance(waypoints[currentWaypointIndex].position, transform.position);

        if (distanceToWaypoint <= 2)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }

        agent.SetDestination(waypoints[currentWaypointIndex].position);
        animator.SetBool("isWalking", true);
        onRadious = false;
    }

    private void StopPatrol()
    {
        agent.isStopped = true;
        animator.SetBool("isWalking", false);
        onRadious = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Detectado");
            isPlayerDetected = true;
            hitbox.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player saiu da colis�o");
            isPlayerDetected = false;
            agent.isStopped = false;
            hitbox.SetActive(false);
        }
    }
}
