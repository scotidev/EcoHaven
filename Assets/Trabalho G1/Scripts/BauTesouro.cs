using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class BauTesouro : MonoBehaviour
{
    [Header("Components")]
    private Animator animBau;
    public LayerMask playerLayer2;
    //public GameObject hitbox2;

    [Header("Variables")]
    private bool isPlayerDetected2 = false;

    void Start()
    {
        animBau = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerDetected2)
        {
            openBau();
            
        }
    }

    private void openBau()
    {
        isPlayerDetected2 = true;
        animBau.SetBool("OpenBau", true);
        //hitbox2.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected!");
            isPlayerDetected2 = true;
            //hitbox2.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player se afastou!");
            isPlayerDetected2 = false;
            //hitbox2.SetActive(false);
        }
    }
}
