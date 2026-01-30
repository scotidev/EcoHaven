using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    public static int coletados = 0;
    public static int totalNaCena = 0;
    public Text textoColetavel;

    void Awake()
    {
        coletados = 0;
        totalNaCena = FindObjectsByType<Collectible>(FindObjectsSortMode.None).Length;
    }

    void Start()
    {
        AtualizarTexto();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Toca o som antes de destruir
            AnimalSound animalSound = GetComponent<AnimalSound>();
            if (animalSound != null)
            {
                animalSound.TocarSom();
            }

            coletados++;
            AtualizarTexto();

            gameObject.GetComponent<Collider>().enabled = false;
            foreach (Transform child in transform) child.gameObject.SetActive(false);
            Destroy(gameObject, 2f);
        }
    }

    private void AtualizarTexto()
    {
        if (textoColetavel != null)
        {
            textoColetavel.text = coletados.ToString() + " / " + totalNaCena.ToString();
        }
    }
}
