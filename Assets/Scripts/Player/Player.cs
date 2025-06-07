using UnityEngine;

public class Player : MonoBehaviour, IDamage
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerCamera playerCamera;
    [SerializeField] PlayerShooting weapon;
    [SerializeField] ColorSwapping colorSwapper;
    [SerializeField] Schmoves schmover;
    [SerializeField] PlayerArm arm;

    [Space]
    [SerializeField] CameraSpring cameraSpring;

    [Space]
    [SerializeField] PrimaryColor currentColor;

    [Space]
    [Header("Conditionals")]
    [SerializeField] public bool canMove;
    [SerializeField] public bool canCam;
    [SerializeField] public bool canAction;

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

            weapon.UpdateWeapon(currentColor, arm);

            schmover.UpdateInput(currentColor);
        }

        arm.UpdateArm(currentColor);
    }

    void LateUpdate()
    {
        float deltaTime = Time.deltaTime;

        cameraSpring.UpdateSpring(deltaTime, Vector3.up);
    }

    public PrimaryColor GetPlayerColor()
    {
        return currentColor;
    }

    public void takeDamage(PrimaryColor hitColor, int amount)
    {
        //DIE.
    }
}

public enum PrimaryColor
{
    RED,
    YELLOW,
    BLUE,
    OMNI
}