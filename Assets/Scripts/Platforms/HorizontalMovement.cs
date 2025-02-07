using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
   [Header("Movement Settings")]
    public float moveSpeed = 3f;   // Speed of platform movement
    public float minX = -5f;       // Minimum X position
    public float maxX = 5f;        // Maximum X position

    private int direction = 1;     // Movement direction (1 = right, -1 = left)

    private void Update()
    {
        transform.position += Vector3.right * moveSpeed * direction * Time.deltaTime;

        // Clamp movement and reverse direction when hitting limits
        if (transform.position.x >= maxX || transform.position.x <= minX)
        {
            direction *= -1; // Reverse direction
        }
    }
}
