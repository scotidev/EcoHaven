using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private CharacterController controller;
    private Transform myCamera;
    private Animator animator;
    [SerializeField] private Transform foot;
    [SerializeField] private LayerMask collisionLayer;

    [Header("Variables")]
    public float velocity = 5f;
    private bool isGround;
    private float jumpStrength;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        // comentario
    }

    public void Move()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontal -= 1f;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontal += 1f;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) vertical += 1f;

        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) vertical -= 1f;

        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;
        //movement = Vector3.ClampMagnitude(movement, 1f); normaliza movimento diagonal

        movement = myCamera.TransformDirection(movement);
        movement.y = 0;

        controller.Move(movement * velocity * Time.deltaTime);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * 10f);
        }

        animator.SetBool("Mover", movement != Vector3.zero);
        isGround = Physics.CheckSphere(foot.position, 0.3f, collisionLayer);
        animator.SetBool("isGround", isGround);
    }

    public void Jump()
    {

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGround)
        {
            jumpStrength = 5f;
            animator.SetTrigger("Jump");
        }

        if (jumpStrength > -9.81f)
        {
            jumpStrength += -9.81f * Time.deltaTime;
        }

        controller.Move(new Vector3(0, jumpStrength, 0) * Time.deltaTime);
    }
}