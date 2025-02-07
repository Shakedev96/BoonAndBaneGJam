using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
   [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] public float movementMultiplier = 10f;
    public float rbDrag = 6f;
    public float airDrag = 2f;

    float horizontalMovement, verticalMovement;
    Vector2 moveInput;
    Vector3 moveDirection;

    public bool isFinished;
    private Rigidbody RB;
    private PlayerJump playerJump;

    [Header("Speed Boost")]
    public float speedBoostAmount = 5f;  // Extra speed gained from power-up
    public float boostDuration = 5f;     // Duration of speed boost
    private float baseSpeed;             // Stores original speed

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.freezeRotation = true;
        playerJump = GetComponent<PlayerJump>();
        baseSpeed = moveSpeed; // Save original speed
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void Update()
    {
        HandleInput();
        ControlDrag();
    }

    void HandleInput()
    {
        horizontalMovement = moveInput.x;
        verticalMovement = moveInput.y;
        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }

    void ControlDrag()
    {
        RB.drag = playerJump.isGrounded ? rbDrag : airDrag;
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        RB.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("EndLine"))
        {
            isFinished = true;
        }
    }

    // 🚀 **Apply Speed Boost**
    public void ApplySpeedBoost()
    {
        StartCoroutine(SpeedBoostRoutine());
    }

    private IEnumerator SpeedBoostRoutine()
    {
        moveSpeed += speedBoostAmount;  // Increase speed
        yield return new WaitForSeconds(boostDuration);  
        moveSpeed = baseSpeed;  // Reset speed after duration
    }
     

}


