using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; } // Singleton Pattern

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // DontDestroyOnLoad(gameObject); // Opcional: se você quiser que o PlayerManager persista entre as cenas
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Garante que o AudioSource é inicializado
        Debug.Log("[PlayerManager.Start] PlayerManager inicializado");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            Debug.Log("Animal resgatado!");
            if (audioSource != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource não encontrado no PlayerManager para tocar som de resgate de animal.");
            }

            AnimalSound animalSound = other.GetComponent<AnimalSound>();

            if (animalSound != null)
            {
                animalSound.TocarLatido();
            }
        }
    }

    public void VencerJogo()
    {
        Debug.Log("Vitória! Todos os PETs resgatados.");
        SceneManager.LoadScene("Endgame");
    }
}
