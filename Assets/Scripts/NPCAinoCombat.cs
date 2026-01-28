using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NpcIa : MonoBehaviour
{

    [Header("Components")]
    public List<Transform> mayWayPoints;
    private UnityEngine.AI.NavMeshAgent newMeshAgent; //alterado para navMeshAgent
    private Animator animacao;
    public LayerMask playerLayer;
    public GameObject hitbox;

    [Header("Variables")]
    public int mayCurrentWayPointIndex = 0;
    public float veloc = 2f;
    private bool playerDetected = false;
    private bool onRadio; 

    void Start()
    {
        newMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animacao = GetComponent<Animator>();
        newMeshAgent.speed = veloc;
    }

    void Update()
    {
        if (!playerDetected)
        {
            Andar();
        }
        else
        {
            Parar();
            SceneManager.LoadScene(1);
        }
    }
    private void Andar()
    {
        if (mayWayPoints.Count == 0)
        {
            return;
        }
        float distanceTowaypoint = Vector3.Distance(
            mayWayPoints[mayCurrentWayPointIndex].position,
            transform.position);

        if (distanceTowaypoint <= 1)
        {
            mayCurrentWayPointIndex = (mayCurrentWayPointIndex + 1) % mayWayPoints.Count;
        }

        newMeshAgent.SetDestination(mayWayPoints[mayCurrentWayPointIndex].position);
        animacao.SetBool("Parar", false);
        onRadio = true;
    }

    private void Parar()
    {
        newMeshAgent.isStopped = true;
        animacao.SetBool("Parar", true);
        onRadio = false;
    }
}
