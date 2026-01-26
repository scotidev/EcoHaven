using UnityEngine;
using UnityEngine.InputSystem; //para reconhecer o input do teclado 
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    private Npc npcDano; //referencia ao Npc 

    public float vidaJogador = 100f;
    public float vidaMaxima = 100f;
    public Image barraVida; //referência à barra de vida na UI

    private bool podeSalvar = false; // começa como falso

    private Vector3 ultimaPosicaoSalva;
    private float ultimaVidaSalva;
    private bool temDadosSalvos = false; // indica se há dados salvos disponíveis

    private AudioSource audioSource;


    void Start()
    {
        CarregarDadosJogador();
        AtualizarBarraVida();

    }

    void Update()
    {
        AtualizarBarraVida();

        if (podeSalvar && Keyboard.current.bKey.wasPressedThisFrame) //verifica se pode salvar e se a tecla B foi pressionada
        {
            SalvarDadosJogador();
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            LimparDadosJogador();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SavePoint"))
        {
            Debug.Log("Colidiu com o ponto de salvamento! Pressione B para salvar ou C para limpar dados!");
            podeSalvar = true;
        }

        if (other.CompareTag("Animal"))
        {
            Debug.Log("Animal resgatado!");
            audioSource.Play();

            // 1. Tenta obter o componente AnimalSound do objeto colidido.
            AnimalSound animalSound = other.GetComponent<AnimalSound>();

            if (animalSound != null)
            {
                // 2. Chama o método que faz o som tocar.
                animalSound.TocarLatido();
            }
            else
            {
                // Caso você esqueça de anexar o script ao Animal
                Debug.LogError("O GameObject Animal não tem o script AnimalSound anexado!");
            }

        }

        if (other.CompareTag("Animal"))
        {
            Debug.Log("Animal resgatado!");

            AnimalSound animalSound = other.GetComponent<AnimalSound>();

            if (animalSound != null)
            {
                // 2. Chama o método que faz o som tocar.
                animalSound.TocarLatido();
            }
            else
            {
                // Caso você esqueça de anexar o script ao Animal
                Debug.LogError("O GameObject Animal não tem o script AnimalSound anexado!");
            }

            // Lógica de resgate (pode destruir o animal ou desativá-lo)
            // Destroy(other.gameObject); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SavePoint"))
        {
            Debug.Log("Saiu do ponto de salvamento!");
            podeSalvar = false;
        }
    }

    void SalvarDadosJogador()
    {
        ultimaPosicaoSalva = transform.position; //tres floats
        ultimaVidaSalva = vidaJogador;

        PlayerPrefs.SetFloat("PosX", ultimaPosicaoSalva.x); //ultima posiçao salva de cada eixo
        PlayerPrefs.SetFloat("PosY", ultimaPosicaoSalva.y);
        PlayerPrefs.SetFloat("PosZ", ultimaPosicaoSalva.z);

        PlayerPrefs.SetFloat("Vida", ultimaVidaSalva);

        PlayerPrefs.SetInt("temDadosSalvos", 1); //salva os dados

        Debug.Log("Dados salvos com sucesso!");
        podeSalvar = false; //impede salvar novamente sem sair e entrar no ponto

    }

    void LimparDadosJogador()
    {
        PlayerPrefs.DeleteKey("PosX"); //deleta apenas o que esta dentro dos parênteses
        PlayerPrefs.DeleteKey("PosY");
        PlayerPrefs.DeleteKey("PosZ");
        PlayerPrefs.DeleteKey("Vida");
        PlayerPrefs.DeleteKey("temDadosSalvos");

        ultimaPosicaoSalva = Vector3.zero; //carrega o personagem na ultima posiçao salva
        ultimaVidaSalva = 0f;
        temDadosSalvos = false;

        vidaJogador = vidaMaxima; //reseta a vida do jogador
        AtualizarBarraVida();

        Debug.Log("Dados apagados com sucesso!");
    }

    void CarregarDadosJogador()
    {
        if (PlayerPrefs.GetInt("temDadosSalvos") == 1) //verifica se há dados salvos
        {
            float posX = PlayerPrefs.GetFloat("PosX");
            float posY = PlayerPrefs.GetFloat("PosY");
            float posZ = PlayerPrefs.GetFloat("PosZ");
            ultimaPosicaoSalva = new Vector3(posX, posY, posZ);
            ultimaVidaSalva = PlayerPrefs.GetFloat("Vida");

            transform.position = ultimaPosicaoSalva; //carrega o personagem na ultima posiçao salva
            vidaJogador = ultimaVidaSalva;
            temDadosSalvos = true;
            Debug.Log("Dados carregados com sucesso!");
        }
        else
        {
            Debug.Log("Nenhum dado salvo encontrado.");
        }
    }

    void AtualizarBarraVida()
    {
        if (barraVida == null) return;
        float vidaNormalizada = Mathf.Clamp01(vidaJogador / vidaMaxima);
        barraVida.fillAmount = vidaNormalizada;
        if (vidaJogador <= 0)
        {
            SceneManager.LoadScene(3); //carrega a cena de game over
        }



    }

    public void CarregarDano()
    {
        vidaJogador -= npcDano.perderVida;
        AtualizarBarraVida();
    }

    //public void TakeDamage()
    //{
    //    if(Keyboard.current.gKey.wasPressedThisFrame) //tecla G para dano
    //    {
    //        vidaJogador -= 10;
    //        AtualizarBarraVida();

    //        if (vidaJogador <= 0)
    //        {
    //            SceneManager.LoadScene(3); //carrega a cena de game over
    //        }
    //    }
    //}

    // public void PowerLife()
    //{
    //if (Keyboard.current.gKey.wasPressedThisFrame) //tecla G para dano
    //{
    //vidaJogador += 10;
    // AtualizarBarraVida();

    //if (vidaJogador <= 0)
    //{
    //    SceneManager.LoadScene(3); //carrega a cena de game over
    // }
    //}
    // }
}
