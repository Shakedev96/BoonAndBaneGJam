using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallRun : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform orientation;


    [Header("Detection")]
    [SerializeField] private float wallDistance = 0.5f;
    [SerializeField] private float minimumJumpHeight = 1.5f;

    [Header ("Wall Running Settings")]
    [SerializeField] private float wallRunGravity;
    [SerializeField] private float wallRunJumpForce;

    private bool wallLeft = false;
    private bool wallRight = false;

    RaycastHit leftWallHit, rightWallHit;
    

    private Rigidbody RB;


    void Start()
    {
        RB = GetComponent<Rigidbody>();    
    }
    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);

    }

    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);
    }

    void Update()
    {
        CheckWall();

        if(CanWallRun())
        {
            if(wallLeft)
            {
                StartWallRun();
                Debug.Log("Wall Run On the Left");
            }
            else if(wallRight)
            {
                StartWallRun();
                Debug.Log("Wall Run On The Right");
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    void StartWallRun()
    {
        RB.useGravity = false;
        RB.AddForce(Vector3.down * wallRunGravity,ForceMode.Force);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(wallLeft)
            {                 
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;   // remove * 100 after testing.
                RB.velocity = new Vector3(RB.velocity.x, 0 , RB.velocity.z);
                RB.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
            }
            else if(wallRight)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                RB.velocity = new Vector3(RB.velocity.x, 0 , RB.velocity.z);
                RB.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
            }
        }
    }

    void StopWallRun()
    {
        RB.useGravity = true;
    }



}
