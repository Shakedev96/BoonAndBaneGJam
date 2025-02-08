using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaneManager : MonoBehaviour
{
    public GameObject blurryVisionPlane; // Assign the UI plane covering the screen
    private PlayerMove playerMove;
    private ActivateBane powerUpUI;

    private float originalFriction;
    private float originalSpeed;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        powerUpUI = FindAnyObjectByType<ActivateBane>();

        originalSpeed = playerMove.moveSpeed;
    }

    // *Blurry Vision Bane*
    public void ApplyBlurryVision(float duration)
    {
        StartCoroutine(BlurryVisionRoutine(duration));
    }

    private IEnumerator BlurryVisionRoutine(float duration)
    {
        powerUpUI.ActivateBan(ActivateBane.BaneType.BlurryVision, duration);
        blurryVisionPlane.SetActive(true);

        yield return new WaitForSeconds(duration);

        blurryVisionPlane.SetActive(false);
    }

    // ? *Frictionless Bane*
    public void ApplyFrictionless(float duration)
    {
        StartCoroutine(FrictionlessRoutine(duration));
    }

    private IEnumerator FrictionlessRoutine(float duration)
    {
        powerUpUI.ActivateBan(ActivateBane.BaneType.Frictionless, duration);

        // Set friction to 0 (Make player slippery)
        originalFriction = playerMove.rbDrag;
        playerMove.rbDrag = 0;

        yield return new WaitForSeconds(duration);

        // Restore original friction
        playerMove.rbDrag = originalFriction;
    }

    //  *Speed Reduction Bane*
    public void ApplySpeedReduction(float duration)
    {
        StartCoroutine(SpeedReductionRoutine(duration));
    }

    private IEnumerator SpeedReductionRoutine(float duration)
    {
        powerUpUI.ActivateBan(ActivateBane.BaneType.SpeedReduction, duration);

        // Reduce speed drastically
        playerMove.moveSpeed /= 3;

        yield return new WaitForSeconds(duration);

        // Restore speed
        playerMove.moveSpeed = originalSpeed;
    }
}
/*
using System.Collections;
using UnityEngine;

public class BaneManager : MonoBehaviour
{
    
}

*/