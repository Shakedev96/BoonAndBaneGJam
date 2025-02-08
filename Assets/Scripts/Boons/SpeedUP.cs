using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUP : MonoBehaviour
{
    public float duration = 5f;  // Boost duration

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMove player = other.GetComponent<PlayerMove>();
            if (player != null)
            {
                player.ApplySpeedBoost(duration);
            }

            // Disable power-up after pickup
            gameObject.SetActive(false);
        }
    }
}
/*
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float duration = 5f;  // Boost duration

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMove player = other.GetComponent<PlayerMove>();
            if (player != null)
            {
                player.ApplySpeedBoost();
            }

            // Disable power-up after pickup
            gameObject.SetActive(false);
        }
    }
}


*/
