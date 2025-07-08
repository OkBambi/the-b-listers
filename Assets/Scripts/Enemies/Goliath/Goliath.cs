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


    float stateTimer;
    Vector3 startPos;

    protected override void Start()
    {
        ColorSelection(setColor);
        startPos = transform.position;

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
            }

            if (roamTime >= roamStopTimer && remainingDistance < 0.01f)
            {
                Roam();
            }


            stateTimer += Time.deltaTime;

            if (stateTimer > topRoamTime)
            {
                currentState = State.Diving;
                Debug.Log("Goliath is diving!");
            }
        }
        else if (currentState == State.Diving)
        {
            Diving();
        }
        else if (currentState == State.Swimming)
        {
            //track player from under the map
        }
        else if (currentState == State.Breach)
        {

        }
    }

    void Roam()
    {
        roamTime = 0;

        Vector3 ranPos = Random.insideUnitCircle * roamDistance;
        ranPos += startPos;

        remainingDistance = (transform.position - ranPos).normalized.magnitude;

        //NavMeshHit hit;
        //NavMesh.SamplePosition(ranPos, out hit, roamDistance, 1);
        //agent.SetDestination(hit.position);
    }
    
    void Diving()
    {
        Vector3 divePos = new Vector3(Random.Range(-maxX, maxX), 1.1f,Random.Range(-maxZ, maxZ));
        goliathHitLocation.transform.position = divePos;
        goliathHitLocation.GetComponent<Renderer>().enabled = true;
        stateTimer = 0;
        currentState = State.Swimming;
    }
}
