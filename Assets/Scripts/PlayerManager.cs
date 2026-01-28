using UnityEngine;
using UnityEngine.InputSystem; //para reconhecer o input do teclado 
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{

    public float vidaJogador = 100f;
    public float vidaMaxima = 100f;
    public Image barraVida; //referência à barra de vida na UI

    private AudioSource audioSource;

    private void Start()
    {
        Debug.Log("[PlayerManager.Start] PlayerManager inicializado");
        Debug.Log($"[PlayerManager.Start] Vida: {vidaJogador}/{vidaMaxima}");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[PlayerManager.OnTriggerEnter] Colisão com: {other.gameObject.name} | Tag: {other.tag}");

        if (other.CompareTag("Animal"))
        {
            Debug.Log("Animal resgatado!");
            audioSource.Play();

            // Tenta obter o componente AnimalSound do objeto colidido.
            AnimalSound animalSound = other.GetComponent<AnimalSound>();

            if (animalSound != null)
            {
                // Chama o método que faz o som tocar.
                animalSound.TocarLatido();
            }
            else
            {
                // Caso você esqueça de anexar o script ao Animal
                Debug.LogError("O GameObject Animal não tem o script AnimalSound anexado!");
            }
        }
    }


    public void VencerJogo()
    {
        Debug.Log("Vitória! Todos os PETs resgatados.");
        SceneManager.LoadScene("Endgame");
    }
}
