using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerCamera playerCamera;
    [SerializeField] ColorSwapping colorSwapper;

    [SerializeField] PrimaryColor currentColor;

    void Start()
    {
        playerMovement.Initialize();
        playerCamera.Initialize();
    }

    void Update()
    {
        playerMovement.UpdateBody();
        playerCamera.UpdateCamera(playerMovement.IsGrounded());

        colorSwapper.UpdateColor(ref currentColor);
    }
}

public enum PrimaryColor
{
    RED,
    YELLOW,
    BLUE
}