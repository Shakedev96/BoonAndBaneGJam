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
    public float gravityScale = 1f;
    public bool isGrounded;
    private Rigidbody rb;
    private PlayerInput playerInput;

    [Header("Jump Logic")]
    public int maxJumps = 1;  // Default jump count
    private int currentJumps = 0;

    [Header("Power-Up Settings")]
    public int boostedJumpCount = 2;   // Double jump
    public float powerUpDuration = 5f; // Duration of double jump effect

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        if (Gamepad.current != null)
        {
            playerInput.SwitchCurrentControlScheme("Gamepad");
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        ////InputAction jumpAction = playerInput.actions.FindAction("Jump");
        ////if (jumpAction != null)
        ////{
        ////    jumpAction.performed += OnJump;
        ////}
        ////else
        ////{
        ////    Debug.LogError("Jump action not found in Input Action Asset!");
        ////}
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded) //&& currentJumps < maxJumps)
        {
            Debug.Log($"Jump button pressed from: {context.control.device.displayName}");
            Jump();
            
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
        CheckGround();
    }

    void Update()
    {
        
    }

    private void CheckGround()
    {
        if (Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer))
        {
            isGrounded = true;
            currentJumps = 0; // Reset jump count when grounded
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        isGrounded = false;

        float jumpForce = Mathf.Sqrt(jumpHeight * -3 * (Physics.gravity.y * gravityScale));
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    

}
/*

}

*/