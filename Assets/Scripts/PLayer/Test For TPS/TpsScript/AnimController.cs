using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimController : MonoBehaviour
{
    [SerializeField] private WallRun wallRun;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private PlayerJump playerJump;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        wallRun = GetComponentInParent<WallRun>();
        playerMove = GetComponentInParent<PlayerMove>();
        playerJump = GetComponentInParent<PlayerJump>(); 
    }

    // Update is called once per frame
    void Update()
    {
        WallRunAnim();
        MoveAnim();
        //JumpAnim();
    }
    public void WallRunAnim()
    {
        if(wallRun.wallLeft || wallRun.wallRight)
        {
            anim.SetBool("WallRun",true);
        }
        else
        {
            anim.SetBool("WallRun",false);
        }
    }

    public void MoveAnim()
    {
        if(playerMove.moveInput.magnitude > 0 || playerMove.moveInput.magnitude < 0)
        {
            anim.SetBool("isRunning",true);
        }
        else
        {
            anim.SetBool("isRunning",false);
        }
    }

    public void JumpAnim()
    {
        if(!playerJump.isGrounded)
        {
            anim.SetBool("isJumping",true);
        }
        else
        {
            anim.SetBool("isJumping",false);
        }
    }

}
