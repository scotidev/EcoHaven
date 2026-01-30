using UnityEngine;

public class AnimalSound : MonoBehaviour
{
    [Header("Configurações de Áudio")]
    [SerializeField] private AudioClip somDoAnimal;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1.0f;
    }

    public void TocarSom()
    {
        if (audioSource != null && somDoAnimal != null)
        {
            audioSource.PlayOneShot(somDoAnimal);
        }
    }
}
