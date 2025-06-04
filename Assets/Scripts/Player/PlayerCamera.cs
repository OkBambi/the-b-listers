using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] int sens;
    [SerializeField] int lockVertMin, lockVertMax;
    [SerializeField] bool invertY;

    [Range(85, 120)]
    [SerializeField] int fov;

    float rotX;

    public void Initliaze()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        InitializeSettings();
    }

    public void InitializeSettings()
    {
        Camera.main.fieldOfView = fov;
    }

    //TO-DO: Smoothing, camera tilt and lean, the works.
    public void UpdateCamera()
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
    }
}
