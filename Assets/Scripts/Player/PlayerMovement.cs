using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDamage
{
    [SerializeField] PlayerShooting shooter;
    [SerializeField] Rigidbody rb;

    [Header("Movement")]
    [SerializeField] float speed;

    [Header("Jumping")]
    [SerializeField] float jumpVel;
    [SerializeField] int jumpMax;

    [Header("Dashing")]
    [SerializeField] float dashForce;
    [SerializeField] bool canDash = true;

    [Header("Misc Movement")]
    [SerializeField] float slamForce;

    int jumpCount;
    bool isGrounded;

    Vector3 moveDir;
    Vector3 playerVel;

    public void Initialize()
    {

    }

    public void UpdateBody()
    {
        //groundcheck
        if (isGrounded)
        {
            jumpCount = 0;
            canDash = true;
        }


        moveDir = (Input.GetAxis("Horizontal") * transform.right)
                + (Input.GetAxis("Vertical") * transform.forward);

        //transform.Translate(moveDir * speed * Time.deltaTime);
        rb.MovePosition(transform.position + moveDir * Time.fixedDeltaTime * speed);

        //jump, dash, etc etc
        Space();
    }

    void Space()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                if (jumpCount < jumpMax)
                {
                    Debug.Log("Jump");
                    rb.AddForce(transform.up * jumpVel, ForceMode.Impulse);
                    jumpCount++;
                }
            }
            else
            {
                if (canDash && moveDir.magnitude > 0)
                {
                    //dash, derive from input
                    if(Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") == 0)
                    {
                        //forward dash
                        Debug.Log("FDash");
                        rb.AddForce(Camera.main.transform.forward * dashForce, ForceMode.Impulse);
                        canDash = false;
                    }
                    else
                    {
                        //directional dash
                        Debug.Log("DirDash");
                        rb.AddForce(moveDir * dashForce, ForceMode.Impulse);
                        canDash = false;
                    }
                }
                else if (moveDir.magnitude == 0)
                {
                    //groundslam
                    Debug.Log("SLAM!");
                    rb.AddForce(-transform.up * slamForce, ForceMode.Impulse);
                }

            }
        }
    }

    public bool IsGrounded()
    {
        //return Physics.Raycast(transform.position, -Vector3.up, 1f);
        return isGrounded;
    }

    public float MoveSpeed()
    {
        return rb.linearVelocity.magnitude;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("groundTag"))
        {
            isGrounded = true;
            shooter.currentRocketJumps = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("groundTag"))
        {
            isGrounded = false;
        }
    }

    public void takeDamage(PrimaryColor hitColor, int amount)
    {
        Debug.Log("Fall.");
        transform.parent.GetComponent<Player>().Die();
    }
}
