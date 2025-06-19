using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Monk : EnemyBase
{
    //casting
    [SerializeField] Transform Casting;
    [SerializeField] GameObject Wave;
    [SerializeField] float Casttimer;
    [SerializeField] float gongBonkRate;
    [SerializeField] float lockedColorTimer;
    [SerializeField] float pauseToCastTimer;


    //roaming movement
    [SerializeField] int FaceTargetSpeed;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] int roamDist;
    [SerializeField] int StopTime;
    float roamTimer;


    Color colorOriginal;
    bool PlayerInRange;

    
    Vector3 playerDir;
    Vector3 startingPOS;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        GetComponent<Rigidbody>();
        ColorSelection(setColor);
        StartCoroutine(Cast());
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        roamCheck();
        roamTimer += Time.deltaTime;

        if (Casttimer > gongBonkRate)
        {
            PauseForAMoment();

            Cast();
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            FaceTarget();
        }
    }

    public void PauseForAMoment()
    {
        agent.isStopped = true;
        StartCoroutine(isroaming(pauseToCastTimer));
    }
    IEnumerator isroaming(float pauseDuration)
    {
        yield return new WaitForSeconds(pauseDuration);
        agent.isStopped = false;
    }


    void roamCheck()
    {
        if(roamTimer >= StopTime)
        {
            roam();
        }
    }

    void roam()
    {
        roamTimer = 0;
        agent.stoppingDistance = 0;

        Vector3 ranPOS = Random.insideUnitSphere * roamDist;
        ranPOS += startingPOS;

        NavMeshHit hit;
        NavMesh.SamplePosition(ranPOS, out hit, roamDist, 1);
        agent.SetDestination(hit.position);
    }

    IEnumerator Cast()
    {
        //projectile
        Instantiate(Wave, transform.position, Quaternion.identity);

        //if player is in the sphere cast
        if (PlayerInRange)
        {
            //stop walking 
            PauseForAMoment();

            yield return new WaitForSeconds(0.1f);

            // lock the player color choice for 3 seconds
            GameManager.instance.playerScript.canColor = false;

            //wait a moment


            //swap through the colors
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }

    void FaceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * FaceTargetSpeed);
    }
}
