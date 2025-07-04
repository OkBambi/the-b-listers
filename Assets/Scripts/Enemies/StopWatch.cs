using System.Collections;
using UnityEngine;
using static EasingLibrary;

public class StopWatch : EnemyBase
{
    //[SerializeField] GameObject SpitSac;
    [SerializeField] GameObject ShockWave;

    int counter;

    public float slamDuration;
    public float endPosition;

    private float startPosition;
    private bool isSlamming;
    private Rigidbody rb;

    Vector3 StartPos;

    private void Awake()
    {
        RandomizeColor();
        OnAECAwake();
        startPosition = transform.position.y;
        //endPosition = 3f; // Adjust the end position as needed
        isSlamming = false;
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
        StartPos = transform.position;

        EnemyManager.instance.StopwatchTrigger += CountDownTimer; // Subscribe to the stopwatch trigger event
        name = "Stop Watch";

    }

    void CountDownTimer()
    {
        counter++;
        if(counter >= 3 && !isSlamming)
        {
            counter = 0;
            isSlamming = true;
            //StartSlam();
            StartCoroutine(slamer()); // Start the coroutine for slamming
            //SacSpit();
        }
        
    }

    //void SacSpit()
    //{
    //    SacGroundDetection mySac = Instantiate(SpitSac,transform.position,Quaternion.identity).GetComponent<SacGroundDetection>();
    //    mySac.setColor = this.setColor;
    //}

    //void StartSlam()
    //{
    //    Debug.Log("Slam started!");
    //    if (isSlamming)
    //    {
    //        Debug.Log("Already slamming, ignoring request.");
    //        slamDuration = 5f; // Normalized time (0 to 1)

    //        // Apply EaseInBack using your Easing Library
    //        float easedPosition = EaseInBack(startPosition, endPosition, slamDuration); // Replace EasingLibrary with your library's name

    //        transform.position = new Vector3(transform.position.x, easedPosition, transform.position.z);

    //        isSlamming = true;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("groundTag"))
    //    {
            
    //    }
    //}

    IEnumerator slamer() {         float elapsedTime = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(transform.position.x, endPosition, transform.position.z);
        while (elapsedTime < slamDuration)
        {
            float t = elapsedTime / slamDuration;
            t = Mathf.Clamp01(t); // Ensure t is between 0 and 1
            float easedT = EaseInBack(0, 1, t); 
            transform.position = Vector3.Lerp(startPos, endPos, easedT);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos; // Ensure we end at the exact position
        isSlamming = false;
        Instantiate(ShockWave, transform.position - new Vector3(0f,1f,0f), Quaternion.identity).GetComponent<stopWatchShockWave>(); // Instantiate shockwave effect
        StartCoroutine(ReturnToStart()); // Return to start position after slamming 
    }

    IEnumerator ReturnToStart()
    {
        float elapsedTime = 0f;
        while (elapsedTime > slamDuration)
        {
            float t = elapsedTime / slamDuration;
            t = Mathf.Clamp01(t); // Ensure t is between 0 and 1
            float easedT = EaseInBack(1, 0, t); 
            transform.position = Vector3.Lerp(transform.position, StartPos, easedT);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = StartPos; // Ensure we end at the exact position
    }


    public override void DeathCheck()
    {
        if (hp <= 0)
        {
            isAlive = false;
            OnAECDestroy();
            RemoveSelfFromTargetList();
            EnemyManager.instance.StopwatchTrigger -= CountDownTimer; // unSubscribe to the stopwatch trigger event
            Destroy(gameObject);
            return;
        }
    }
}
