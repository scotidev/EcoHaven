using UnityEngine;
using UnityEngine.InputSystem; //para reconhecer o input do teclado 
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovimentoo : MonoBehaviour
{
    public Image barraAnimais; //referência à barra
    public GameObject PegarAnimal;
    public float cadaAnimal = 10f;
    public float totalAnimais = 100f;



    void Start()
    {
        AtualizarBarraAnimal();
        PegarAnimal.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        AtualizarBarraAnimal();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Espada"))
        {
            Debug.Log("Espada coletada!");
            PegarAnimal.SetActive(true);


        }
    }

    void AtualizarBarraAnimal()
    {
        if (barraAnimais == null) return;
        float totalAnimal = Mathf.Clamp01(totalAnimais  / cadaAnimal);
        barraAnimais.fillAmount = totalAnimal;



    }
}
