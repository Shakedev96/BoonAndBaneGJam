using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Beam : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float laserRange = 10f; // Max distance the laser can reach
    public LayerMask hitLayers; // Objects the laser can collide with

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Set LineRenderer properties
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Basic shader
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    void Update()
    {
        DrawLaser();
    }

    void DrawLaser()
    {
        Vector3 start = transform.position;
        Vector3 direction = transform.forward;

        // Raycast to detect objects
        if (Physics.Raycast(start, direction, out RaycastHit hit, laserRange, hitLayers))
        {
            lineRenderer.SetPosition(0, start);      // Start of laser
            lineRenderer.SetPosition(1, hit.point);  // End of laser (on hit point)
        }
        else
        {
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, start + direction * laserRange); // Extend full 10m if no hit
        }
    }
}
