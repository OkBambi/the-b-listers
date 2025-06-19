using UnityEngine;

public class StopWatch : EnemyBase, IDamage
{
    //[SerializeField] GameObject SpitSac;

    int counter;

    private Rigidbody rb;
    public float slamForce; // Adjust this value for the desired slam force

    private bool isSlamming;

    private void Awake()
    {
        OnAECAwake();

        rb = GetComponent<Rigidbody>();
        // Keep gravity off initially
        rb.useGravity = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        counter = 0;
        ColorSelection(setColor);
        base.UpdateBoidAwareness(); //this will need to be commented out once/if the stopwatch gets a rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            CountDownTimer();
        }
    }

    void CountDownTimer()
    {
        counter++;
        if(counter == 3)
        {
            counter = 0;
            //SacSpit();
        }
    }

    //void SacSpit()
    //{
    //    SacGroundDetection mySac = Instantiate(SpitSac,transform.position,Quaternion.identity).GetComponent<SacGroundDetection>();
    //    mySac.setColor = this.setColor;
    //}

    void StartSlam()
    {
        isSlamming = true;
        // Turn on gravity
        rb.useGravity = true;
        // apply downward force
        rb.AddForce(Vector3.down * slamForce, ForceMode.Impulse); 
    }

    void EndSlam()
    {
        isSlamming = false;
        // Turn off gravity
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
    }

    void OnCollisionCheck(Collision collision)
    {
        // Check if the collision is with the ground layer
        if (collision.gameObject.CompareTag("Floor"))
        {
            EndSlam();
        }
    }
}
