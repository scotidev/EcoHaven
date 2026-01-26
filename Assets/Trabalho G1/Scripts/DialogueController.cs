using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
//using JetBrains.Annotations;

public class DialogueController : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj; //painel de dialogo
    public Text actorNameText; //text component para o nome do ator
    public Text speetchText; //text component para o texto do discurso

    [Header("Variables")]
    public float typingSpeed;   //velocidade de escrita do texto
    private string[] sentences; //array de sentencas do dialogo
    private int index;  //indice para controlar a sentenca atual
    private Coroutine typingCoroutine;  //corrotina para o efeito de digitação

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame)
        {
            NextSentence();
        }
    }

    public void Speech(string[] txt, string actorName)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        dialogueObj.SetActive(true);
        speetchText.text = "";
        actorNameText.text = actorName;
        sentences = txt;
        index = 0;
        typingCoroutine = StartCoroutine(TypeSentence());
    }
    //Debug.Log("Iniciando diálogo...");
    IEnumerator TypeSentence()
    {
        speetchText.text = ""; // Limpa o texto antes de começar a digitar
        foreach (char letter in sentences[index].ToCharArray()) // Converte a sentença atual em um array de caracteres
        {
            speetchText.text += letter; // Adiciona cada letra ao texto
            yield return new WaitForSeconds(typingSpeed); // Espera um pouco antes de adicionar a próxima letra
        }
        typingCoroutine = null; // Reseta a corrotina após a conclusão

    }

    public void NextSentence()
    {
        if(speetchText.text == sentences[index]) // Verifica se a sentença atual foi completamente digitada
        {
            if (index < sentences.Length - 1) // Verifica se há mais sentenças para exibir
            {
                index++;
                speetchText.text = "";
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine); //typingCoroutine armazena a corrotina atual
                }
                typingCoroutine = StartCoroutine(TypeSentence());
            }
            else
            {
                EndDialogue(); // Chama o método para encerrar o diálogo
            }
        }
    }
    public void HidePanel() //
    {
        if (typingCoroutine != null) // Verifica se a corrotina de digitação está em execução
        {
            StopCoroutine(typingCoroutine);
        }
        speetchText.text = ""; 
        actorNameText.text = ""; 
        index = 0; 
        dialogueObj.SetActive(false); // Oculta o painel de diálogo
    }

    public void EndDialogue()
    {
        speetchText.text = "";
        dialogueObj.SetActive(false);
    }

} 



