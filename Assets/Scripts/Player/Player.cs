using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerCamera playerCamera;
    [SerializeField] PlayerShooting weapon;
    [SerializeField] ColorSwapping colorSwapper;
    [SerializeField] Schmoves schmover;

    [Space]
    [SerializeField] PrimaryColor currentColor;

    [Space]
    [Header("Conditionals")]
    [SerializeField] bool canMove;
    [SerializeField] bool canCam;
    [SerializeField] bool canAction;

    void Start()
    {
        playerMovement.Initialize();
        playerCamera.Initialize();
        weapon.Initialize();
    }

    void Update()
    {
        if (canMove)
            playerMovement.UpdateBody();

        if (canCam)
            playerCamera.UpdateCamera(playerMovement.IsGrounded());

        if (canAction)
        {
            colorSwapper.UpdateColor(ref currentColor);

            weapon.UpdateWeapon(currentColor);

            schmover.UpdateInput(currentColor);
        }
    }
}

public enum PrimaryColor
{
    RED,
    YELLOW,
    BLUE,
    OMNI
}