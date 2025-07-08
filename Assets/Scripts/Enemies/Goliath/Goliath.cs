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
    [Header("Swim Parameters")]
    [SerializeField] float swimSpeed;

    

    Vector3 startPos;

    protected override void Start()
    {
        ColorSelection(setColor);
        startPos = transform.position;
    }

    void Update()
    {
        if (remainingDistance < 0.01f)
        {
            roamTime += Time.deltaTime;
        }

        if (roamTime >= roamStopTimer && remainingDistance < 0.01f)
        {
            Roam();
        }
    }

    void Roam()
    {
        roamTime = 0;

        Vector3 ranPos = Random.insideUnitCircle * roamDistance;
        ranPos += startPos;

        //NavMeshHit hit;
        //NavMesh.SamplePosition(ranPos, out hit, roamDistance, 1);
        //agent.SetDestination(hit.position);
    }
}
