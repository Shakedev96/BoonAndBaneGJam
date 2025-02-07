using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;   // Speed of platform movement
    public float minY = 1f;        // Minimum Y position
    public float maxY = 5f;        // Maximum Y position

    private int direction = 1;     // Movement direction (1 = up, -1 = down)

    private void Update()
    {
        transform.position += Vector3.up * moveSpeed * direction * Time.deltaTime;

        // Clamp movement and reverse direction when hitting limits
        if (transform.position.y >= maxY || transform.position.y <= minY)
        {
            direction *= -1; // Reverse direction
        }
    }
}

