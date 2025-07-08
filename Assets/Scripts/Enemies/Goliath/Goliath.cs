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
    [Space]
    [Header("Swim Parameters")]
    [SerializeField] float swimSpeed;


    float stateTimer;
    Vector3 startPos;

    protected override void Start()
    {
        ColorSelection(setColor);
        startPos = transform.position;
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
        float maxX = map.transform.localScale.x * 2 - radiusOfDiveLocation;
        float maxZ = map.transform.localScale.z * 2 - radiusOfDiveLocation;
        Vector3 divePos = new Vector3(Random.Range(-maxX, maxX), 0,Random.Range(-maxZ, maxZ));
    }
}
