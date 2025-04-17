using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClimbObjects : MonoBehaviour
{
    [SerializeField] private Transform topPlayer;
    [SerializeField] private Transform bottomPlayer;
    [SerializeField] private KeyCode Up;
    Rigidbody RB;
    [SerializeField] private float climbforce;

    [SerializeField] private float topRadius,bottomRadius;

    [SerializeField] private LayerMask WallGrab;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        CheckTop();
        //CheckBottom();
    }

    void CheckTop()
    {
        if(Physics.CheckSphere(topPlayer.position,topRadius,WallGrab) && Input.GetKey(Up))
        {
            Debug.Log("Grab up true");
            RB.AddForce(Vector3.up * climbforce,ForceMode.Force);
        }
    }
    void CheckBottom()
    {
        if(Physics.CheckSphere(bottomPlayer.position,bottomRadius,WallGrab))
        {
            Debug.Log("Grab down true");
            RB.AddForce(Vector3.up * -climbforce, ForceMode.Force);
        }
    }
    void CheckInput()
    {
        
    }
}
