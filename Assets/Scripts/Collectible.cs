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
            coletados++;
            AtualizarTexto();
            Destroy(gameObject);
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
