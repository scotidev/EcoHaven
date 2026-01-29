using UnityEngine;

public class RescueZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Collectible.coletados >= Collectible.totalNaCena && Collectible.totalNaCena > 0)
            {
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
