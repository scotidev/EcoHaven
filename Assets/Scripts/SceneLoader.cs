using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public float waitTime = 5f;
    public string mainMenuScene = "Menu";
  
    void Start()
    {
        Time.timeScale = 1f; // garante que o tempo esteja rodando
        Invoke(nameof(GoToMainMenu), waitTime);
    }
    void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
