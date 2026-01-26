using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class NpcAmigo : MonoBehaviour
{

    [Header("Components")]
    public List<Transform> wayPointsNpc;
    private UnityEngine.AI.NavMeshAgent navMeshAgent2;
    private Animator animNpc;
    public LayerMask playerLayer2;

    [Header("Variables")]
    public int currentWayPointIndex2 = 0;
    public float speeds = 2f;
    private bool isPlayerDetected2 = false;
    private bool onRadious2;

    void Start()
    {
        navMeshAgent2 = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animNpc = GetComponent<Animator>();
       // navMeshAgent2.speed = speed;
    }

    void Update()
    {
        if (!isPlayerDetected2)
        {
            Correr();
        }
        else
        {
            animNpc.SetBool("Parar", true);
        }
    }
    private void Correr()
    {
        if (wayPointsNpc.Count == 0)
        {
            return;
        }
        float distanceTowaypoint = Vector3.Distance(
            wayPointsNpc[currentWayPointIndex2].position,
            transform.position);

        if (distanceTowaypoint <= 2)
        {
            currentWayPointIndex2 = (currentWayPointIndex2 + 1) % wayPointsNpc.Count;
        }

        navMeshAgent2.SetDestination(wayPointsNpc[currentWayPointIndex2].position);
        animNpc.SetBool("Move", true);
        onRadious2 = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player detected!");
            isPlayerDetected2 = true;
            //hitbox2.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player se afastou!");
            isPlayerDetected2 = false;
            navMeshAgent2.isStopped = false;
            //hitbox2.SetActive(false);
        }
    }
}
