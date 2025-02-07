using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
   [Header("Movement")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float movementMultiplier = 10f;
    public float rbDrag = 6f;
    public float airDrag = 2f;

    public float baseSpeed;
    public bool canDash = false;

    float horizontalMovement, verticalMovement;
    Vector2 moveInput;
    Vector3 moveDirection;

    public bool isFinished;
    private Rigidbody RB;
    private PlayerJump playerJump;

    [Header("Speed Boost")]
    public float speedBoostAmount = 5f;  
    public float boostDuration = 5f;     

    [Header("Dash Ability")]
    public float dashForce = 20f;  
    public float dashDuration = 0.2f;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.freezeRotation = true;
        playerJump = GetComponent<PlayerJump>();
        baseSpeed = moveSpeed; 
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnDash(InputAction.CallbackContext context)
    {
          if(context.performed)
          {
               Debug.Log("dash presesed");
               if(canDash)
               {
                    StartCoroutine(DashRoutine());
               }
          }
    }

    void Update()
    {
        HandleInput();
        ControlDrag();

        // Check if dash key is pressed and dash boon is active
        if (canDash && Keyboard.current.shiftKey.wasPressedThisFrame) 
        {
            StartCoroutine(DashRoutine());
        }
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

    // 🚀 **Apply Speed Boost Boon**
    public void ApplySpeedBoost(float duration)
    {
        StartCoroutine(SpeedBoostRoutine(duration));
    }

    private IEnumerator SpeedBoostRoutine(float duration)
    {
        moveSpeed += speedBoostAmount;
        yield return new WaitForSeconds(duration);
        moveSpeed = baseSpeed;
    }

    // **Apply Dash Boon**
    public void ApplyDashBoon(float duration)
    {
        StartCoroutine(DashBoonRoutine(duration));
    }

    private IEnumerator DashBoonRoutine(float duration)
    {
        canDash = true;
        yield return new WaitForSeconds(duration);
        canDash = false;
    }

    private IEnumerator DashRoutine()
    {
        Vector3 dashDirection = transform.forward;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            RB.velocity = dashDirection * dashForce;
            yield return null;
        }
    }

    void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("BlurryVisionBane"))
    {
        FindAnyObjectByType<BaneManager>().ApplyBlurryVision(10f);
    }
    else if (other.CompareTag("FrictionlessBane"))
    {
        FindAnyObjectByType<BaneManager>().ApplyFrictionless(10f);
    }
    else if (other.CompareTag("SpeedReductionBane"))
    {
        FindAnyObjectByType<BaneManager>().ApplySpeedReduction(10f);
    }
}

     

}

/*
*/



