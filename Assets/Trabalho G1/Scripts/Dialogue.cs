using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{

    [Header("Variables")]
    public string[] speechText;
    public string actorName;
    private DialogueController dc; //referencia ao DialogueController - o tornando uma variavel 
    private bool onRadious ; //verifica se o jogador esta no alcance do NPC
    private bool isDialogueActive = false; //verifica se o dialogo ja foi iniciado
    public LayerMask playerLayer; //layer do jogador
    public float radious; //raio de interacao com o NPC

    void Start()
    {
        dc = FindObjectOfType<DialogueController>();
    }

    private void FixedUpdate()
    {
        Interact();
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame 
            && onRadious && !isDialogueActive) 
        { 
            StartDialogue();
        }
    }

    private void StartDialogue() 
    { 
        isDialogueActive = true;
        dc.Speech(speechText, actorName);
        Debug.Log("Iniciando diálogo com " + actorName);
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dc.HidePanel();
        Debug.Log("Diálogo encerrado.");
    }

    private void OnDrawGizmos() //Opcional //visualizar o raio de interacao no editor
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radious);
    }

    public void Interact()//
    {
        Vector3 point1 = transform.position + Vector3.up * radious;
        Vector3 point2 = transform.position - Vector3.up * radious;

        Collider[] hits = Physics.OverlapCapsule
            (
                point1, point2, radious, playerLayer
            );
        if (hits.Length > 0) 
        { 
            if (!onRadious) 
            { 
                Debug.Log("Jogador entrou no raio de interação do NPC.");
            }
            onRadious = true;
        }
        else 
        { 
            if (onRadious) 
            { 
                Debug.Log("Jogador saiu do raio de interação do NPC.");
                
            }
            onRadious = false;

            if (isDialogueActive) 
            { 
                EndDialogue();
            }
        }
    }


}
