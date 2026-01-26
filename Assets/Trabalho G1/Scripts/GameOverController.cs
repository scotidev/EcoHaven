using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public float delay = 5f;
    public int scenee; // para ser alterada na unity
    void Start()
    {
        StartCoroutine(LoadAfterSeconds());
    }

    IEnumerator LoadAfterSeconds()
    { 
        yield return new WaitForSeconds(delay); //Aguarde o atraso especificado.
        SceneManager.LoadScene(scenee); // Carregar a cena Game Over  | Depois mudar quando criar o menu principal

    }
   
}
