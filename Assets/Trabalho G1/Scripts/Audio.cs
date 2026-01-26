using UnityEngine;

public class AnimalSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        // Pega o componente AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    // Método público para ser chamado pelo PlayerManager
    public void TocarLatido()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}