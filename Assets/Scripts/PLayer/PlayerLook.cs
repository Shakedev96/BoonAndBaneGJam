using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    public Camera Cam;

    float mouseX, mouseY;

    float multiplier = 0.01f;

    float xRot, yRot;


    void Start()
    {
        Cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        myInput();
        Cam.transform.localRotation = Quaternion.Euler(xRot,0 ,0);
        transform.rotation = Quaternion.Euler(0, yRot, 0);
    }

    void myInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRot += mouseX * sensX * multiplier;
        xRot -= mouseY * sensY * multiplier;
    }

}
