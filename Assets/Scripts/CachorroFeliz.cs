using UnityEngine;

public class CachorroFeliz : MonoBehaviour
{
    [Header("Configurações")]
    public Transform player; // Aqui você vai arrastar o seu Player
    public float distanciaParaLatir = 5.0f; // A que distância ele detecta?

    private AudioSource audioSource;
    private bool jaLatiu = false; // A "trava" para não repetir o som

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Calcula a distância atual entre o cachorro e o player
        float distanciaAtual = Vector3.Distance(transform.position, player.position);

        // SE o player estiver perto E o cachorro ainda não latiu
        if (distanciaAtual <= distanciaParaLatir && jaLatiu == false)
        {
            LatirDeAlegria();
        }

        // Opcional: SE o player se afastar muito, reseta a trava
        // Assim, se o player voltar depois, o cachorro late de novo
        if (distanciaAtual > distanciaParaLatir + 3.0f)
        {
            jaLatiu = false;
        }
    }

    void LatirDeAlegria()
    {
        audioSource.Play();
        jaLatiu = true; // Ativa a trava para não tocar de novo imediatamente

        // DICA: Aqui você também pode chamar uma animação!
        // Ex: animator.SetTrigger("RaboAbanando");
    }
}
