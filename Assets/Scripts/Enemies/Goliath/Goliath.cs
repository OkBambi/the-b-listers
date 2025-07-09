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
    [SerializeField] int roamStopTimer;
    float roamTime;
    float remainingDistance;
    Vector3 roamPosition;


    [Space]
    [Header("Dive Parameters")]
    [SerializeField] float diveSpeed;
    [SerializeField] float radiusOfDiveLocation;
    [SerializeField] GameObject map;
    private GameObject goliathHitLocation;
    private float maxX;
    private float maxZ;
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
        maxX = map.transform.lossyScale.x - radiusOfDiveLocation - 30;
        maxZ = map.transform.lossyScale.z - radiusOfDiveLocation - 30;
        goliathHitLocation = GameObject.Find("GoliathHitMarker");
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

            if (remainingDistance < 0.01f)
            {
                roamTime += Time.deltaTime;
                if (roamTime >= roamStopTimer)
                {
                    PickRoamLocation();
                    roamTime = 0f;
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
        roamTime = 0f;
        Vector3 ranPos = Random.insideUnitCircle * roamDistance;
        ranPos += startPos;
        roamPosition = ranPos;
        remainingDistance = (transform.position - roamPosition).normalized.magnitude;
    }

    void RoamToLocation()
    {
        print("Roaming");
        remainingDistance = (transform.position - roamPosition).normalized.magnitude;

        Vector3 direction = (roamPosition - transform.position).normalized;
        transform.Translate(direction * roamSpeed * Time.deltaTime);
    }
    
    void Diving()
    {
        Vector3 divePos = new Vector3(Random.Range(-maxX, maxX), 1.1f,Random.Range(-maxZ, maxZ));
        goliathHitLocation.transform.position = divePos;
        goliathHitLocation.GetComponent<Renderer>().enabled = true;
        stateTimer = 0;

        //go down to indicator and through the map
        Vector3 direction = (divePos - transform.position).normalized;

        transform.Translate(direction * roamSpeed * Time.deltaTime);

        //switch to swimming when Y level is within a certain threshold
        currentState = State.Swimming;
    }
}
