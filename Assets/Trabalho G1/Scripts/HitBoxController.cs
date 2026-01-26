using UnityEngine;
using UnityEngine.SceneManagement;


public class HitBoxController : MonoBehaviour
{
    public int scene; // para ser alterada na unity
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("GAME OVER!");
            SceneManager.LoadScene(scene);


        }
    }
}
