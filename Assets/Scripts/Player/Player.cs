using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerCamera playerCamera;

    void Start()
    {
        playerMovement.Initliaze();
        playerMovement.Initliaze();
    }

    void Update()
    {
        playerMovement.UpdateBody();
        playerCamera.UpdateCamera();
    }
}
