using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    public Camera Cam;

    float mouseX, mouseY;
    private Vector2 lookInput;

   [Tooltip("Rotation Sensitivity" + "DO NOT CHANGE")] [SerializeField] private float multiplier = 0.01f;


    float xRot, yRot;
    private Coroutine fovCoroutine;
    private Coroutine tiltCoroutine;


    void Start()
    {
        Cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MyInput();
        ApplyLook();


        // Cam.transform.localRotation = Quaternion.Euler(xRot,0 ,0);
        // transform.rotation = Quaternion.Euler(0, yRot, 0);


    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
        Debug.Log("Looking Here and There");
    }


    void MyInput()
    {
        //updated logic
         // Reset input at the start of the frame to prevent continuous rotation
        lookInput = Vector2.zero;

        // Read controller input (right stick)
        Vector2 controllerInput = Gamepad.current?.rightStick.ReadValue() ?? Vector2.zero;

        // Read mouse delta input
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * 0.1f; // Scale mouse input

        // Combine inputs (use either controller or mouse input)
        if (mouseDelta != Vector2.zero)
        {
            lookInput = mouseDelta;
        }
        else if (controllerInput != Vector2.zero)
        {
            lookInput = controllerInput;
        }
    }

    private void ApplyLook()
    {
        if (lookInput == Vector2.zero) return;

        yRot += lookInput.x * sensX * multiplier;  // Horizontal look
        xRot -= lookInput.y * sensY * multiplier;  // Vertical look

        xRot = Mathf.Clamp(xRot, -90f, 90f); // Prevent flipping


        Cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRot, 0);
    }

    public void WallRunFOV(float endValue)
    {
        if (fovCoroutine != null)
        { 
            StopCoroutine(fovCoroutine);
        }
        fovCoroutine = StartCoroutine(ChangeFOV(endValue));
    }
    private IEnumerator ChangeFOV(float targetFOV)
    {
        float startFOV = Cam.fieldOfView;
        float duration = 0.5f; 
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Cam.fieldOfView = Mathf.Lerp(startFOV, targetFOV, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Cam.fieldOfView = targetFOV;
    }

    public void WallRunTilt(float zTilt)
    {
        if (tiltCoroutine != null) StopCoroutine(tiltCoroutine);
        tiltCoroutine = StartCoroutine(ChangeTilt(zTilt));
    }
    private IEnumerator ChangeTilt(float targetTilt)
    {
        float startTilt = Cam.transform.localRotation.eulerAngles.z;
        if (startTilt > 180) startTilt -= 360; // Keep angles between -180 and 180
        float duration = 0.3f; 
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newTilt = Mathf.Lerp(startTilt, targetTilt, elapsedTime / duration);
            Cam.transform.localRotation = Quaternion.Euler(xRot, 0, newTilt);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Cam.transform.localRotation = Quaternion.Euler(xRot, 0, targetTilt);
    }
    

}

/*
*/

