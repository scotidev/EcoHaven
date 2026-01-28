using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    public static int totalDeColetaveis = 0;
    public Text textoColetavel;

    void Start()
    {
        AtualizarTexto();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            totalDeColetaveis++;
            AtualizarTexto();
            Destroy(gameObject);
        }
    }

    private void AtualizarTexto()
    {
        if (textoColetavel != null)
        {
            textoColetavel.text = totalDeColetaveis.ToString();
        }
    }
}
