using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRBmove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 10f; // Controls rotation smoothness

    private Vector2 rotateInput;
    public float rotationSmoothTime = 0.1f; // Smoothing for rotation
    private float rotationVelocity; // Helper for smoothing
    private float targetAngle; // Target angle for rotation

    /* [Header("Camera Settings")]
    public Transform cameraTransform; // Assign Main Camera
    public float sensitivity = 2f;
    public float verticalLookLimit = 80f; // Limits how much the player can look
    private float xRotation = 0f; */

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

    public void onRotate(InputAction.CallbackContext context)
    {
        rotateInput = context.ReadValue<Vector2>(); // Map the rotation input (Right Stick)

    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        
    }


    private void HandleMovement()
    {
    

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical"); 

        moveDirection = transform.right * moveX + transform.forward * moveZ;
        //moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        float currentSpeed =  runSpeed ;

        

        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);

        
    }


    private void HandleRotation()
    {
        if (rotateInput.magnitude >= 0.1f) // Rotate only when moving
        {
             // Calculate target rotation angle (relative to world space)
            targetAngle = Mathf.Atan2(rotateInput.x, rotateInput.y) * Mathf.Rad2Deg;

            // Smoothly rotate towards the target angle
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationSmoothTime);

            // Apply rotation to the player's transform
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
            /* Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); */
        }
    }

}
/*
{
    
}

*/
