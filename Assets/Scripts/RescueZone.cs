using UnityEngine;
using UnityEngine.SceneManagement;

public class RescueZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se é o jogador
        if (other.CompareTag("Player"))
        {
            // Verifica se todos os pets foram coletados
            if (Collectible.coletados >= Collectible.totalNaCena && Collectible.totalNaCena > 0)
            {
                // Chama o método VencerJogo do PlayerManager
                PlayerManager playerManager = other.GetComponent<PlayerManager>();
                if (playerManager != null)
                {
                    playerManager.VencerJogo();
                }
            }
            else
            {
                Debug.Log($"Ainda faltam PETs! Coletados: {Collectible.coletados} / {Collectible.totalNaCena}");
            }
        }
    }
}
