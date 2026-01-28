using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    public static int coletados = 0;
    public static int totalNaCena = 0;
    public Text textoColetavel;

    void Awake()
    {
        // Resetamos as variáveis estáticas para evitar acúmulo entre jogadas
        coletados = 0;
        // Contamos quantos coletáveis existem na cena no início
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
            // Mostra o progresso: Ex: "3 / 10"
            textoColetavel.text = coletados.ToString() + " / " + totalNaCena.ToString();
        }
    }
}
