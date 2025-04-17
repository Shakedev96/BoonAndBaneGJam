using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float movementMultiplier = 10f;
    [SerializeField] private float refFloat;
    public float rbDrag = 6f;
    public float airDrag = 2f;

    public float wallRunSpeed;
    [SerializeField] public bool wallrunning;

    public float baseSpeed;
    public bool canDash = false;

    float horizontalMovement, verticalMovement;
   
    public Vector2 moveInput;
    
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

    
    // public void OnDash(InputAction.CallbackContext context)
    // {
    //     if (context.performed)
    //     {
    //         Debug.Log("dash presesed");
    //         if (canDash)
    //         {
    //             //StartCoroutine(DashRoutine());
    //         }
    //     }
    // }

    void Update()
    {

        if(!playerJump.isGrounded && !wallrunning)
        {
            baseSpeed = 0;
        }
        else
        {
            baseSpeed = moveSpeed;
        }
        //HandleInput();
        ControlDrag();
        if(wallrunning)
        {
            baseSpeed = wallRunSpeed;
        }
        else
        {
            baseSpeed = moveSpeed;
        }

        
    }

    /* void HandleInput()
    {
        horizontalMovement = moveInput.x;
        verticalMovement = moveInput.y;
        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    } */

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
        //RB.AddForce(moveDirection.normalized * baseSpeed * movementMultiplier, ForceMode.Acceleration);
        
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 _inputkey = new Vector3(horizontalInput , 0 ,verticalInput );

        //playerRB.velocity = _inputkey * walkSpeed;

        RB.MovePosition(transform.position + _inputkey * baseSpeed * Time.deltaTime);

        
        

        if(_inputkey.magnitude >= 0.1f)
        {
            float rotationAngle = Mathf.Atan2(_inputkey.x,  _inputkey.z) * Mathf.Rad2Deg;
            float smoothRoation = Mathf.SmoothDampAngle(transform.eulerAngles.y , rotationAngle ,ref refFloat , 0.1f);

            transform.rotation = Quaternion.Euler(0 ,smoothRoation ,0);
        }
        
        
    }
    

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("EndLine"))
        {
            isFinished = true;
        }
    }

    



}