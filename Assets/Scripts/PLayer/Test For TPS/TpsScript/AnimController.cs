using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimController : MonoBehaviour
{
    [SerializeField] private WallRun wallRun;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        wallRun = GetComponentInParent<WallRun>();
    }

    // Update is called once per frame
    void Update()
    {
        WallRunAnim();
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
}
