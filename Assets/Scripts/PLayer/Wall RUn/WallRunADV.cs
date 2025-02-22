using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallRunADV : MonoBehaviour
{
    [Header("Wall Run Settings")]
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float wallRunForce, wallClimbSpeed;
    [SerializeField] private float wallJumpUpForce, wallJumpSideForce;
    [SerializeField] private float maxWallRunTime;
    //private float wallRunTimer;


    [Header ("Input")] 
    [SerializeField, Tooltip("R1/RB on GamePad")] private KeyCode upwardsRunK = KeyCode.Q;
    [SerializeField, Tooltip("L1/LB on GamePad")] private KeyCode downwardsRunK = KeyCode.E;
    [SerializeField, Tooltip("X/A on GamePad")] private KeyCode wallJumpK = KeyCode.Space;
    [SerializeField] private bool upwardsRunning, downwardsRunning;
    private float horizontalInput;
    private float verticalInput;


    [Header("Detection")]
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    [SerializeField] private bool wallLeft, wallRight;


    [Header("Exit WallRun")]
    [SerializeField] private bool exitWallRun;
    [SerializeField] private float exitWallTime;
    private float exitWallTimer;

    [Header("Gravity")]
    public bool useGravity;
    public float gravityCounterForce;

    [Header("References")]
    private Rigidbody RB;
    private PlayerMove pMove;
    [SerializeField] private Transform orientation;
    PlayerInput playerInput;

    InputAction jumpAction;


    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        pMove = GetComponent<PlayerMove>();
        playerInput = GetComponent<PlayerInput>();
        jumpAction = playerInput.actions["Jump"];

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

        upwardsRunning = Input.GetKey(upwardsRunK);
        downwardsRunning = Input.GetKey(downwardsRunK);
        if (Gamepad.current != null)
        {
            upwardsRunning = Gamepad.current.rightShoulder.isPressed;  // Example: R1 / RB for upward running
            downwardsRunning = Gamepad.current.leftShoulder.isPressed; // Example: L1 / LB for downward running
        }

        //State 1- wall run state

        if((wallLeft || wallRight) && verticalInput > 0 && AboveGround() && !exitWallRun)
        {
            // start the wall run
            if(!pMove.wallrunning)
            {
                StartWallRun();
            }

            // wallRun timer
            // if(wallRunTimer > 0)
            // {
            //     wallRunTimer -= Time.deltaTime;
            // }
            // if(wallRunTimer <= 0 && pMove.wallrunning)
            // {
            //     exitWallRun = true;
            //     exitWallTimer = exitWallTime;
            // }


            //wallJump
            if(Input.GetKeyDown(wallJumpK) || jumpAction.WasPressedThisFrame())
            {
                Debug.Log("its Happeing" + " Wall jumping");
                WallJump();
            }

        }

        //STATE 2 exitWall

        else if(exitWallRun)
        {
            if(pMove.wallrunning)
            {
                StopWallRun();
            }
            if(exitWallTimer > 0)
            {
                exitWallTimer -= Time.deltaTime;
            }
            if(exitWallTimer <= 0)
            {
                exitWallRun = false;
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

        //wallRunTimer = maxWallRunTime;
        
        GetComponent<PlayerLook>().WallRunFOV(70f);
        GetComponent<PlayerLook>().WallRunTilt(wallRight ? 5f : -5f); // Tilt camera based on wall side
    }


    private void WallRunMovement()
    {
        RB.useGravity = false;
        RB.velocity = new Vector3(RB.velocity.x, 0f, RB.velocity.z);

        Vector3 wallNormal = (wallRight ? rightWallHit : leftWallHit).normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);


        // Ensure wallForward always points in the correct direction
        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }
        
        // forward forces
        RB.AddForce(wallForward * wallRunForce, ForceMode.Force);

        //upwards and downwards force for movement on walls
        if(upwardsRunning)
        {
            RB.velocity = new Vector3(RB.velocity.x, wallClimbSpeed, RB.velocity.z);
        }
        if(downwardsRunning)
        {
            RB.velocity = new Vector3(RB.velocity.x, -wallClimbSpeed, RB.velocity.z);
        }


        //push towards the wall
        if(!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
        {
            RB.AddForce(-wallNormal * 100, ForceMode.Force);
        }

        //weaken Gravity
        // if(useGravity)
        // {
        //     RB.AddForce(transform.up * gravityCounterForce, ForceMode.Force);
        // }
        
    }


    private void StopWallRun()
    {
        pMove.wallrunning = false;

        GetComponent<PlayerLook>().WallRunFOV(60f);  // Reset FOV
        GetComponent<PlayerLook>().WallRunTilt(0f);  // Reset tilt
    }

    private void WallJump()
    {   
        // enter exit wall run State
        exitWallRun = true;
        
        exitWallTimer = exitWallTime;


        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        // reset y and add force
        RB.velocity = new Vector3(RB.velocity.x, 0f, RB.velocity.z);
        RB.AddForce(forceToApply, ForceMode.Impulse);
        
        Debug.Log("Wall Jump Initiated with " + forceToApply + " force");
    }
}
/*
*/
