using UnityEngine;
using static EasingLibrary;

public class StopWatch : EnemyBase, IDamage
{
    //[SerializeField] GameObject SpitSac;
    [SerializeField] float startingHight; // Adjust this value for the starting position hight of the stopwatch
    [SerializeField] float stopHight;// Adjust this value for the height at which the stopwatch will slam down
    [SerializeField] float riseTime; // Adjust this value for the time it takes to ease in back to the starting position
    int counter;

    private Rigidbody rb;
    public float slamForce; // Adjust this value for the desired slam force
    
    private bool isSlamming;

    private void Awake()
    {
        startingHight = transform.position.y; // Set the starting height to the current position's y-coordinate
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
        EaseInBack(startingHight, stopHight, riseTime);

    }

    private void OnTriggerEnter(Collider other)
    {

    }

    void EndSlam()
    {
    }

}
