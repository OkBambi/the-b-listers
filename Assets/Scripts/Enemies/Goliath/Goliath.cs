using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Goliath : EnemyBase
{
    /*
     * Make random Vector3 to move to 
     * Move in direction for like.. 1 second or smth
     * Random.insideUnitCircle smth smth
     * yes
     * 
     */
    public enum State
    {
        Roam,
        Diving,
        Swimming,
        Breach,
    }

    [SerializeField] State currentState = State.Roam;
    [Space]
    [Header("Timers")]
    [SerializeField] float topRoamTime;
    [SerializeField] float swimTime; //time under the map
    [Space]
    [Header("Roam Parameters")]
    [SerializeField] float roamSpeed;
    [SerializeField] int roamDistance;
    [SerializeField] float roamStopTimer;
    float roamTime;
    float remainingDistance;
    Vector3 roamPosition;


    [Space]
    [Header("Dive Parameters")]
    [SerializeField] float diveSpeed;
    [SerializeField] float radiusOfDiveLocation;
    [SerializeField] GameObject map;
    [SerializeField] GameObject goliathHitLocation;
    [SerializeField] float mapRadius;
    [Space]
    [Header("Swim Parameters")]
    [SerializeField] float swimSpeed;

    Transform playerTransform;
    float stateTimer;
    Vector3 startPos;

    protected override void Start()
    {
        ColorSelection(setColor);
        startPos = transform.position;

        PickRoamLocation();

        //for swimming
        //playerTransform = GameManager.instance.player.transform;

        //for diving
        goliathHitLocation.transform.localScale = new Vector3(radiusOfDiveLocation, 0.1f, radiusOfDiveLocation);
    }

    void Update()
    {
        StateCheck();
    }

    void StateCheck()
    {
        if (currentState == State.Roam)
        {

            if (remainingDistance < 1f)
            {
                roamTime += Time.deltaTime;
                if (roamTime >= roamStopTimer)
                {
                    PickRoamLocation();
                }
            }
            else
            {
                RoamToLocation();
            }


            stateTimer += Time.deltaTime;

            if (stateTimer > topRoamTime)
            {
                currentState = State.Diving;
                roamTime = 0;
                Debug.Log("Goliath is diving!");
            }
        }
        else if (currentState == State.Diving)
        {
            stateTimer = 0;
            Diving();
        }
        else if (currentState == State.Swimming)
        {
            stateTimer += Time.deltaTime;

            //track player from under the map
        }
        else if (currentState == State.Breach)
        {

        }
    }

    void PickRoamLocation()
    {
        print("Choosing new location");
        roamTime = 0f;
        Vector3 ranPos = Random.insideUnitCircle * roamDistance;
        ranPos.y = 0f;
        ranPos += startPos;
        roamPosition = ranPos;
        remainingDistance = (transform.position - roamPosition).normalized.magnitude;
    }

    void RoamToLocation()
    {
        remainingDistance = (roamPosition - transform.position).normalized.magnitude;

        Vector3 direction = (roamPosition - transform.position).normalized;
        transform.Translate(direction * roamSpeed * Time.deltaTime);
    }
    
    void Diving()
    {
        if (goliathHitLocation.transform.position == Vector3.zero)
        {
            Vector3 divePos = new Vector3(Random.Range(-1f, 1f) * mapRadius, 1.1f, Random.Range(-1f, 1f) * mapRadius);
            goliathHitLocation.transform.position = divePos;
            goliathHitLocation.GetComponent<Renderer>().enabled = true;
        }


        stateTimer += Time.deltaTime;

        //go down to indicator and through the map
        Vector3 goToPos = new Vector3(goliathHitLocation.transform.position.x, 
            transform.position.y, goliathHitLocation.transform.position.z);
        Vector3 horizontalDirection = (goToPos - transform.position).normalized;

        transform.Translate(horizontalDirection * roamSpeed * Time.deltaTime);
        
        if (Vector3.Distance(goToPos, transform.position) < 0.1f)
        {
            transform.Translate(Vector3.down * diveSpeed * Time.deltaTime, Space.World);

            if (transform.position.y < -20f)
            {
                currentState = State.Swimming;
                goliathHitLocation.GetComponent<Renderer>().enabled = false;
            }
        }        
    }
}
