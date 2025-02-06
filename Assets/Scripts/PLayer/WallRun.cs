using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("Wall Run Settings")]
    public float wallRunSpeed = 8f;
    public float wallJumpForce = 10f;
    public float maxWallRunTime = 2f;
    public KeyCode wallRunKey = KeyCode.E; // Press 'E' to wall run
    public LayerMask wallLayer;
    
    
    private Rigidbody rb;
    [SerializeField] private PlayerJump grounded;
    private bool isWallRunning;
    private float wallRunTimer;
    private Vector3 lastWallNormal;
    // Start is called before the first frame update
    void Start()
    {
        grounded = GetComponent<PlayerJump>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckWallRun();

        if (Input.GetKeyDown(KeyCode.Space) && isWallRunning)
        {
            WallJump();
        }
    }

    private void CheckWallRun()
    {
        if (grounded.isGrounded) 
        {
            isWallRunning = false;
            wallRunTimer = 0;
            return;
        }

        RaycastHit hit;
        bool isTouchingWall = Physics.Raycast(transform.position, transform.right, out hit, 1.2f, wallLayer) ||
                            Physics.Raycast(transform.position, -transform.right, out hit, 1.2f, wallLayer);

        if (isTouchingWall && Input.GetKey(wallRunKey))
        {
            if (!isWallRunning)
            {
                lastWallNormal = hit.normal;
                rb.useGravity = false; // Disable gravity for smooth wall run
            }

            isWallRunning = true;

            // ✅ Move along the wall direction instead of just forward
            Vector3 wallRunDirection = Vector3.Cross(lastWallNormal, Vector3.up).normalized;
            if (Vector3.Dot(transform.forward, wallRunDirection) < 0) // Ensures correct direction
            {
                wallRunDirection = -wallRunDirection;
            }

            rb.velocity = wallRunDirection * wallRunSpeed;

            wallRunTimer += Time.deltaTime;
            if (wallRunTimer >= maxWallRunTime)
            {
                isWallRunning = false;
            }
        }
        else
        {
            ExitWallRun();
        }
    }

    private void WallJump()
    {
        rb.velocity = new Vector3(lastWallNormal.x * wallJumpForce, wallJumpForce, lastWallNormal.z * wallJumpForce);
        ExitWallRun();
    }

    private void ExitWallRun()
    {
        if (isWallRunning)
        {
            isWallRunning = false;
            rb.useGravity = true;
        }
    }

}
/*\
*/