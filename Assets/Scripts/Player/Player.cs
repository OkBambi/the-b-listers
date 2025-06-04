using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerCamera playerCamera;
    [SerializeField] PlayerShooting weapon;
    [SerializeField] ColorSwapping colorSwapper;

    [Space]
    [SerializeField] PrimaryColor currentColor;

    void Start()
    {
        playerMovement.Initialize();
        playerCamera.Initialize();
        weapon.Initialize();
    }

    void Update()
    {
        playerMovement.UpdateBody();
        playerCamera.UpdateCamera(playerMovement.IsGrounded());

        colorSwapper.UpdateColor(ref currentColor);

        weapon.UpdateWeapon();
    }
}

public enum PrimaryColor
{
    RED,
    YELLOW,
    BLUE
}