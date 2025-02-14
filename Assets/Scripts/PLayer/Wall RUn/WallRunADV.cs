using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunADV : MonoBehaviour
{
    [Header("Wall Run Settings")]
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private float wallRunForce;
    

    [Header ("Input")] 
    private float horizontalInput;
    private float verticalInput;

    [Header("Detection")]
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    [SerializeField] private bool wallLeft, wallRight;

    [Header("References")]
    private Rigidbody RB;
    private PlayerMove pMove;
    [SerializeField] private Transform orientation;


    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        pMove = GetComponent<PlayerMove>();
    }

   
    // Update is called once per frame
    void Update()
    {
        CheckForWall();
        StateMachine();
    }

    void FixedUpdate()
    {
        if(pMove.wallrunning)
        {
            WallRunMovement();
        }
    }
     private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDistance, whatIsWall);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down,minJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {
        // getting INputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //State 1- wall run state

        if((wallLeft || wallRight) && verticalInput > 0 && AboveGround())
        {
            // start the wall run
            if(!pMove.wallrunning)
            {
                StartWallRun();
            }

        }
        else
        {
            if(pMove.wallrunning)
            {
                StopWallRun();
            }
        }


    }


    private void StartWallRun()
    {
        pMove.wallrunning = true;
    }


    private void WallRunMovement()
    {
        RB.useGravity = false;
        RB.velocity = new Vector3(RB.velocity.x, 0f, RB.velocity.z);

        /* Vector3 wallNormal;
        

        if (wallRight)
        {
            wallNormal = rightWallHit.normal;
        }
            
        else
        {
            wallNormal = leftWallHit.normal;
        } */
            

        Vector3 wallNormal = (wallRight ? rightWallHit : leftWallHit).normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        // Ensure wallForward always points in the correct direction
        if (wallRight)
        {
            wallForward = -wallForward;
        }
        
        

        // forward forces
        RB.AddForce(wallForward * wallRunForce, ForceMode.Force);
    }


    private void StopWallRun()
    {
        pMove.wallrunning = false;
    }
}
