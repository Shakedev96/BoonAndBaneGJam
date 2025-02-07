using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTrap : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 100f;  // Speed of rotation
    public bool clockwise = true;  // Toggle direction of rotation
    public Transform rotationPivot; // The fixed pivot point (should be the pole's position)

    private Vector3 rotationAxis = Vector3.up; // Rotates around the vertical Y-axis

    void Update()
    {
        if (rotationPivot == null)
        {
            Debug.LogError("Rotation Pivot is not set! Assign the pole's transform.");
            return;
        }

        RotateAxe();
    }

    void RotateAxe()
    {
        float direction = clockwise ? 1f : -1f;
        transform.RotateAround(rotationPivot.position, Vector3.up, direction * rotationSpeed * Time.deltaTime);
    }
}
/*
using UnityEngine;

public class RotatingAxeTrap : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 100f; // Speed of rotation
    public bool clockwise = true; // Toggle direction of rotation
    public Transform rotationPivot; // The fixed pivot point (should be the pole's position)

    private void Update()
    {
        if (rotationPivot == null)
        {
            Debug.LogError("Rotation Pivot is not set! Assign the pole's transform.");
            return;
        }

        RotateAxe();
    }

    void RotateAxe()
    {
        float direction = clockwise ? 1f : -1f;
        transform.RotateAround(rotationPivot.position, Vector3.up, direction * rotationSpeed * Time.deltaTime);
    }
}


*/
