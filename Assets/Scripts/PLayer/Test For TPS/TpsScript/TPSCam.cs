using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSCam : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot; // Empty GameObject used as rotation pivot
    [SerializeField] private Transform playerModel; // The player's character model (optional)
    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;
    [SerializeField] private float distanceFromPlayer = 3f;
    [SerializeField] private float minYAngle = -20f;
    [SerializeField] private float maxYAngle = 60f;
    [SerializeField] private float rotationSmoothing = 10f;

    private Vector2 lookInput;
    private float xRot, yRot;
    private Camera cam;
    private Coroutine fovCoroutine;
    private Coroutine tiltCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cameraPivot == null)
        {
            Debug.LogError("Camera Pivot is not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        ApplyLook();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void MyInput()
    {
        lookInput = Vector2.zero;

        // Read controller input (right stick)
        Vector2 controllerInput = Gamepad.current?.rightStick.ReadValue() ?? Vector2.zero;

        // Read mouse delta input
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * 0.1f;

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

        yRot += lookInput.x * sensX * Time.deltaTime;
        xRot -= lookInput.y * sensY * Time.deltaTime;

        xRot = Mathf.Clamp(xRot, minYAngle, maxYAngle);

        Quaternion targetRotation = Quaternion.Euler(xRot, yRot, 0);
        cameraPivot.rotation = Quaternion.Lerp(cameraPivot.rotation, targetRotation, Time.deltaTime * rotationSmoothing);

        // Set camera position behind the pivot
        cam.transform.position = cameraPivot.position - cameraPivot.forward * distanceFromPlayer;
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
        float startFOV = cam.fieldOfView;
        float duration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            cam.fieldOfView = Mathf.Lerp(startFOV, targetFOV, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cam.fieldOfView = targetFOV;
    }

    public void WallRunTilt(float zTilt)
    {
        if (tiltCoroutine != null)
        {
            StopCoroutine(tiltCoroutine);
        }
        tiltCoroutine = StartCoroutine(ChangeTilt(zTilt));
    }

    private IEnumerator ChangeTilt(float targetTilt)
    {
        float startTilt = cam.transform.localRotation.eulerAngles.z;
        if (startTilt > 180) startTilt -= 360;
        float duration = 0.3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newTilt = Mathf.Lerp(startTilt, targetTilt, elapsedTime / duration);
            cam.transform.localRotation = Quaternion.Euler(xRot, 0, newTilt);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cam.transform.localRotation = Quaternion.Euler(xRot, 0, targetTilt);
    }


}

/*
{
    
}
*/
