using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
   [Header ("Movement")]
   [SerializeField] private float moveSpeed;
   [SerializeField] public float movementMultiplier = 10f;
   public float rbDrag = 6f;
   public float airDrag = 2f;

   float horizontalMovement, verticalMovement;

   Vector3 moveDirection;

   private Rigidbody RB;
   private PlayerJump playerJump;

   void Start()
   {
        RB = GetComponent<Rigidbody>();
        RB.freezeRotation = true;
        playerJump = GetComponent<PlayerJump>();
   }

   void Update()
   {
        HandleInput();
        ControlDrag();
   }

   void HandleInput()
   {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
   }

    void ControlDrag()
    {
        if(playerJump.isGrounded)
        {
            RB.drag = rbDrag;
        }
        else
        {
            RB.drag = airDrag;
        }
        
    }

   void FixedUpdate()
   {
        HandleMovement();
   }

   void HandleMovement()
   {
        RB.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
   }


}
