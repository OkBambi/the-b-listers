using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [Header("Movement")]
    [SerializeField] float speed;

    [Header("Jumping")]
    [SerializeField] float jumpVel;
    [SerializeField] int jumpMax;
    [SerializeField] float gravity;

    int jumpCount;

    Vector3 moveDir;
    Vector3 playerVel;

    public void Initliaze()
    {

    }

    public void UpdateBody()
    {
        //groundcheck
        if (controller.isGrounded)
        {
            jumpCount = 0;
            playerVel.y = 0;
        }


        moveDir = (Input.GetAxis("Horizontal") * transform.right)
                + (Input.GetAxis("Vertical") * transform.forward);

        controller.Move(moveDir * speed * Time.deltaTime);

        //jump
        Jump();

        controller.Move(playerVel * Time.deltaTime);

        playerVel.y -= gravity * Time.deltaTime;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            playerVel.y = jumpVel;
            jumpCount++;
        }
    }

    public bool IsGrounded()
    {
        return controller.isGrounded;
    }

    public float MoveSpeed()
    {
        return controller.velocity.magnitude;
    }
}
