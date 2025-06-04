using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerCamera playerCamera;

    void Start()
    {
        playerMovement.Initialize();
        playerCamera.Initialize();
    }

    void Update()
    {
        playerMovement.UpdateBody();
        playerCamera.UpdateCamera(playerMovement.IsGrounded());
    }
}
