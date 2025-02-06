using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpHeight = 7f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayer;

    // new change
    // [SerializeField] private float jumpAmount;
    // [SerializeField] private float maxDuration;

    // [SerializeField] private float jumpTime;
    // [SerializeField] private bool jumping;

    [SerializeField] private float gravityScale;
    [SerializeField] private PlayerInput playerInput;

    // [SerializeField] private float currentGravityScale;
    // [SerializeField] private float fallingGravityScale;
    
    private Rigidbody rb;
    public bool isGrounded;

    void Awake()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();

        if (Gamepad.current != null)
        {
            playerInput.SwitchCurrentControlScheme("Gamepad");
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
       // Physics.gravity *= gravityMod;
       playerInput = GetComponent<PlayerInput>();


        InputAction jumpAction = playerInput.actions.FindAction("Jump");
        if (jumpAction != null)
        {
            jumpAction.performed += OnJump;
        }
        else
        {
            Debug.LogError("Jump action not found in Input Action Asset!");
        }


       
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed && isGrounded)
        {
            Debug.Log($"Jump button pressed from: {context.control.device.displayName}");
            Jump();
        }
        Debug.Log("X is pressed");
    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
    }

    void Update()
    {
        CheckGround();

        /* if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            
        } */

       
    }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        Debug.Log("Grounded");
    }

    private void Jump()
    {
        isGrounded = false;
        float jumpForce = Mathf.Sqrt(jumpHeight * -3 * (Physics.gravity.y * gravityScale));

        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

}
