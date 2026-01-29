using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Necessário para acessar o NavMeshAgent

public class AI : MonoBehaviour
{
    [Header("Components")]
    public List<Transform> waypoints;
    private NavMeshAgent agent;
    private Animator animator;

    [Header("Variables")]
    public int currentWaypointIndex = 0;
    public float speed = 2f;
    public float stoppingDistance = 2f; // Distância para trocar de waypoint

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (agent != null)
        {
            agent.speed = speed;
        }

        // Define o primeiro destino logo ao iniciar
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
        // Se não houver waypoints, não faz nada
        if (waypoints.Count == 0) return;

        // Calcula a distância entre o animal e o waypoint atual
        float distanceToWaypoint = Vector3.Distance(waypoints[currentWaypointIndex].position, transform.position);

        // Se estiver perto o suficiente do ponto atual, muda para o próximo
        if (distanceToWaypoint <= stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }

        // Controla a animação baseado na velocidade real do agente
        // Se a velocidade for maior que 0.1, ele ativa o isWalking
        bool isMoving = agent.velocity.magnitude > 0.1f;
        //animator.SetBool("isWalking", isMoving);
    }
}