using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRBmove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    private Vector3 moveDirection;
    private Vector2 moveInput;

    private Rigidbody rb;
 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents physics rotation issues
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        HandleMovement();
        
        
    }


    private void HandleMovement()
    {
    

        /* float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical"); */

        
        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        float currentSpeed =  runSpeed ;

        

        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);

        
    }

}
/*
{
    
}

*/
