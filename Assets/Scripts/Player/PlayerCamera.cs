using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Look Settings")]
    [SerializeField] int sens;
    [SerializeField] int lockVertMin, lockVertMax;
    [SerializeField] bool invertY;

    [Range(85, 110)]
    [SerializeField] int fov;

    [Space]
    [Header("Camera Tilt")]
    [SerializeField] float leanIntensity = 0.3f;
    [SerializeField] float leanSmooth = 4f;

    float rotX;
    Quaternion initialRotation;

    public void Initialize()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Camera.main.fieldOfView = fov;

        initialRotation = transform.localRotation;
    }

    public void InitializeSettings()
    {
        Camera.main.fieldOfView = fov;
    }

    //TO-DO:
    //- SMOOTHING
    //- TILT
    //- INSANELY slight bob on walk
    public void UpdateCamera(bool isGrounded)
    {
        // get input
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        // give player options to invert look up and down
        if (invertY)
            rotX += mouseY;
        else
            rotX -= mouseY;

        // clamp the camera on the X axis
        rotX = Mathf.Clamp(rotX, lockVertMin, lockVertMax);

        // rotate the camera on the X axis to look up and down
        transform.localRotation = Quaternion.Euler(rotX, 0, 0);

        // rotate the player on the y axis to look left and right
        transform.parent.Rotate(Vector3.up * mouseX);

        //camera leaning
        CameraLean();
    }

    void CameraLean()
    {
        float rotZ = Input.GetAxis("Horizontal") * leanIntensity * -1f;

        Quaternion finalRot = Quaternion.Euler(rotX, 0, rotZ);
        transform.localRotation = Quaternion.Lerp(initialRotation, finalRot, leanSmooth);
    }

    public void CameraAdjustFOV(float multiplier)
    {
        Camera.main.fieldOfView = fov * multiplier;
    }
}
