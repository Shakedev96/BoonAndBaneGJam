using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float duration = 8f;

    private PlayerMove playerMovement;
    private PowerUpUI boonManager;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMove>();
        boonManager = FindAnyObjectByType<PowerUpUI>();
    }

    public void Activate()
    {
        StartCoroutine(DashBoost());
    }

    private IEnumerator DashBoost()
    {
        Debug.Log("dash true");
        boonManager.ActivateBoon(PowerUpUI.BoonType.Dash, duration);
        playerMovement.canDash = true;

        yield return new WaitForSeconds(duration);

        playerMovement.canDash = false;
    }
}
/*
using UnityEngine;

public class DashBoon : MonoBehaviour
{
    
}

*/
