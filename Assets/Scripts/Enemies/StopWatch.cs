using UnityEngine;
using static EasingLibrary;

public class StopWatch : EnemyBase, IDamage
{
    //[SerializeField] GameObject SpitSac;
    int counter;

    public float slamDuration;
    public Vector3 endPosition;

    private Vector3 startPosition;
    private bool isSlamming;
    private float elapsedTime;
    private Rigidbody rb;

    private void Awake()
    {
        OnAECAwake();
        startPosition = transform.position;
        endPosition = startPosition + new Vector3(0, 4f, 0); // Adjust the end position as needed
        isSlamming = false;
        elapsedTime = 0.1f;
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
        EaseInBack(startPosition.y, endPosition.y, elapsedTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        //if()
    }

    void EndSlam()
    {
    }

}
