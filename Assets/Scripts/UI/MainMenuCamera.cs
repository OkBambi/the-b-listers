using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public void OnButtonPress()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, -90f);
    }

    public void OnBackButton()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, 90f);
    }
}
