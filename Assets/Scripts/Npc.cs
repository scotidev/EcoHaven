using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Npc : MonoBehaviour
{

    private PlayerManager vidaPlayerManager; //referencia ao PlayerManager
    public float perderVida;

    [Header("Components")]
    public List<Transform> wayPoints;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Animator anim;
    public LayerMask playerLayer;
    public GameObject hitbox;

    [Header("Variables")]
    public int currentWayPointIndex = 0;
    public float speed = 2f;
    private bool isPlayerDetected = false;
    private bool onRadious;

    private bool attackInimigo = false; 


    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        navMeshAgent.speed = speed;

        vidaPlayerManager = FindObjectOfType<PlayerManager>(); //encontra o PlayerManager na cena 
    }

    void Update()
    {

        if (isPlayerDetected == true)
        {
            stopWalking();
            anim.SetBool("Attack", true);
            perderVida = vidaPlayerManager.vidaJogador -= 10;
            Debug.Log("Jogador leva dano");
            anim.SetBool("Attack", false);

        }
        else {
            Walking();
        }


        //if (!isPlayerDetected)
        //{
        //    Walking();
        //}
        //else
        //{
        //    stopWalking();
        //    anim.SetBool("Attack", true);

        //    //logica de ataque para perder vida
        //    attackInimigo = true;
        //    if (attackInimigo == true)
        //    {
        //        perderVida = vidaPlayerManager.vidaJogador -= 10;
        //        Debug.Log("Jogador leva dano");
        //    }

        //}
    }
    private void Walking()
    {
        if (wayPoints.Count == 0) 
        {
            return;
        }
        float distanceTowaypoint = Vector3.Distance(
            wayPoints[currentWayPointIndex].position,
            transform.position);

        if (distanceTowaypoint <= 2)
        { 
            currentWayPointIndex = (currentWayPointIndex + 1) % wayPoints.Count;
        }

        navMeshAgent.SetDestination(wayPoints[currentWayPointIndex].position);
        anim.SetBool("Move", true);
        onRadious = false;
    }

    private void stopWalking() 
    {
        navMeshAgent.isStopped = true;
        anim.SetBool("Move", false);
        onRadious = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           //Debug.Log("Player detected!");
            isPlayerDetected = true;
            //hitbox.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider other) //pode ser usado apenas uma vez no script
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player se afastou!");
            isPlayerDetected = false;
            navMeshAgent.isStopped = false;
            //hitbox.SetActive(false);
        }
    }
}
