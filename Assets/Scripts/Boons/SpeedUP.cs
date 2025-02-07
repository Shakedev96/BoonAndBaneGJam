using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUP : MonoBehaviour
{
    public float speedMultiplier = 1.5f;
    public float duration = 10f;

    private PlayerMove playerMovement;
    private PowerUpUI boonManager;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMove>();
        boonManager = FindAnyObjectByType<PowerUpUI>();
    }

    public void Activate()
    {
        StartCoroutine(SpeedBoost());
    }

    private IEnumerator SpeedBoost()
    {
        boonManager.ActivateBoon(PowerUpUI.BoonType.Speed, duration);
        playerMovement.moveSpeed *= speedMultiplier;

        yield return new WaitForSeconds(duration);

        playerMovement.moveSpeed /= speedMultiplier;
    }
}
/*
using UnityEngine;

public class SpeedBoon : MonoBehaviour
{
    
}



*/
