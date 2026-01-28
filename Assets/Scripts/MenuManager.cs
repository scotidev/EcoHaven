using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        // Application.Quit(); //forma otimizada de sair do jogo

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //para o modo de jogo no editor
#elif UNITY_WEBGL
            Application.OpenURL("about:blank"); 
#else
            Application.Quit(); //sai do jogo
#endif
    }

}