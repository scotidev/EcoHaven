using UnityEngine;
using UnityEngine.InputSystem; //biblioteca para Input System

public class HeroMoviment : MonoBehaviour
{
    [Header("Components")] //cabecalho no inspector
    private CharacterController controle;
    private Transform miCamera; //objeto responsavel pela camera
    private Animator animacao; //componente de animacao
    // public Transform foot;  //objeto responsavel pelo chao, //colocando a variavel como public ela aparece no inspetor
    [SerializeField] private Transform pee; //[SerializeField] -> variavel se mantem privada, mas aparece no inspetor e pode ser setada la
    [SerializeField] private LayerMask colisaoLayer; //layer de colisao

    [Header("Variables")] //cabecalho no inspector
    public float velocidade = 5f; //velocidade de movimentacao do personagem
    private bool isChao; //verifica se o personagem esta no chao
    private float yForca; //forca aplicada no eixo y (forca do pulo)

    void Start()
    {
        controle = GetComponent<CharacterController>(); //pega o componente CharacterController anexado ao game object
        miCamera = Camera.main.transform; //pega a transform da camera principal
        animacao = GetComponent<Animator>(); //pega o componente Animator anexado ao game object
    }
        
    void Update()
    {
        Mover(); //chama o metodo 
        Pular();

    }

    public void Mover()
    {
        //Debug.Log("Executando o movimento do personagem...");

        float horizontal = 0f; //variaveis para movimentos
        float vertical = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontal -= 1f; 
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontal += 1f;

        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) vertical -= 1f;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) vertical += 1f; 

        Vector3 movimentar = new Vector3(horizontal, 0, vertical); //vetor de movimento //em x,y,z //no 2D seria so (horizontal, vertical)

        movimentar = Vector3.ClampMagnitude(movimentar, 1f); // Normaliza a velocidade diagonal de movimento //passa o movimento e a velocidade usada
        movimentar = miCamera.TransformDirection(movimentar); //transforma o vetor de movimento para o espaco da camera
        movimentar.y = 0; // Mantém o movimento da camera no plano horizontal 

        controle.Move(movimentar * velocidade * Time.deltaTime); // Aplica o movimento ao CharacterController  //Time.deltaTime -> TEMPO DA UNITY
        if (movimentar != Vector3.zero) //Move é um componente do characterController
        { //vetor for diferente de zero (ou seja, se houver movimento)
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.LookRotation(movimentar), //Quaternion é para trabalhar com rotações, angulacoes em 3D
                Time.deltaTime * 10f
             );
        }

        animacao.SetBool("Mover", movimentar != Vector3.zero); //seta o parametro Mover do animator (nome criado no Animator > Parameters)
        isChao = Physics.CheckSphere(pee.position, 0.3f, colisaoLayer); //verifica se o personagem esta no chao 
        //CheckSphere cria uma esfera imaginaria para detectar colisao //passa a posicao do objeto foot, o raio da esfera e a layer de colisao
        
        animacao.SetBool("IsGround", isChao); //seta o parametro IsGround do animator (nome criado no Animator > Parameters)
    }

    public void Pular()
    {
        //Debug.Log("Estou no chão?" + isChao);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isChao) //pega o input do teclado e estar no chao //startar o trigger
        {
            yForca = 5f; //forca do pulo
            animacao.SetTrigger("Jump"); //seta o parametro Jump do animator (nome criado no Animator > Parameters)
        }

        if (yForca > -9.81f) //se a forca no eixo y for maior que -9.81 (forca da gravidade)
        {
            yForca += -9 * Time.deltaTime; //aplica a gravidade
        }

        controle.Move(new Vector3(0, yForca, 0) * Time.deltaTime); // Aplica a gravidade ao CharacterController

    }
}
