using UnityEngine;
using static EasingLibrary;

public class StopWatch : EnemyBase, IDamage
{
    //[SerializeField] GameObject SpitSac;
    [SerializeField] float startingHight; // Adjust this value for the starting position hight of the stopwatch
    [SerializeField] float riseTime; // Adjust this value for the time it takes to ease in back to the starting position
    int counter;

    private Rigidbody rb;
    public float slamForce; // Adjust this value for the desired slam force
    
    private bool isSlamming;

    private void Awake()
    {
        OnAECAwake();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        counter = 0;
        ColorSelection(setColor);
        //base.UpdateBoidAwareness(); //this will need to be commented out once/if the stopwatch gets a rigidbody
        rb = GetComponent<Rigidbody>();
        // Keep gravity off initially
        rb.useGravity = false;
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
        if(counter == 3 && !isSlamming)
        {
            counter = 0;
            StartSlam();
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
        //// Turn on gravity
        rb.useGravity = true;
        // apply downward force
        rb.AddForce(Vector3.down * slamForce, ForceMode.Impulse); 
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("groundTag") && isSlamming)
        {
            EndSlam();
        }
    }

    void EndSlam()
    {
        Debug.Log("Slam Ended");
        isSlamming = false;
        // Turn off gravity
        rb.useGravity = false;
        EaseInBack(transform.position.y, startingHight, riseTime);
    }

}
