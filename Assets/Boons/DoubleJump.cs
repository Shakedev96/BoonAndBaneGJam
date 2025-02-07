using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public float duration = 15f;

    private PlayerJump playerJump;
    private PowerUpUI boonManager;

    private void Start()
    {
        playerJump = FindAnyObjectByType<PlayerJump>();  // Find the PlayerJump script
        boonManager = FindAnyObjectByType<PowerUpUI>(); // Find the UI manager
    }

    public void Activate()
    {
        if (playerJump != null)
        {
            StartCoroutine(DoubleJumpBoost());
        }
        else
        {
            Debug.LogError("PlayerJump script not found on the player!");
        }
    }

    private IEnumerator DoubleJumpBoost()
    {
        // Activate UI boon timer
        boonManager?.ActivateBoon(PowerUpUI.BoonType.DoubleJump, duration);

        // Apply double jump boon effect
        playerJump.ApplyDoubleJumpBoon(duration);

        yield return new WaitForSeconds(duration);

        // Reset UI after the boon ends
        boonManager?.DeactivateBoon(PowerUpUI.BoonType.DoubleJump);
    }
}
/*
using System.Collections;
using UnityEngine;

public class DoubleJumpBoon : MonoBehaviour
{
    
}


*/
