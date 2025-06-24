using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Monk : EnemyBase
{
    //casting
    [SerializeField] GameObject Wave;
    [SerializeField] float pauseToCastTimer;


    [Header("roaming movement")]
    [SerializeField] int FaceTargetSpeed;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] int roamDist;
    [SerializeField] int StopTime;
    float roamTimer;

    //Seperation
    [SerializeField] float minSeperationDistance;
    [SerializeField] GameObject SeperationDome;


    Color colorOriginal;
    bool PlayerInRange;
    public bool monkInRange;

    Vector3 playerDir;
    Vector3 startingPOS;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        startingPOS = transform.position;
        GetComponent<Rigidbody>();
        ColorSelection(setColor);
        StartCoroutine(Cast());
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        roamCheck();
        if (agent.remainingDistance < 0.01f)
        {
            roamTimer += Time.deltaTime;
            if (monkInRange)
            {
                monkScan();
            }
            PauseForAMoment();
            StartCoroutine(Cast());
        }
        FaceTarget();
    }

    //movement
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
        if (roamTimer >= StopTime && agent.remainingDistance < 0.01f)
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

    //spacing
    void monkScan()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, minSeperationDistance);
        for (int i = 0; i < colliders.Length; i++)
        {
            {
                Collider collider = colliders[i];
                if (collider.gameObject != gameObject && collider.CompareTag("monkEnemy"))
                {
                    MonkSpacing(collider);
                    break;
                }
            }
        }
    }

    void MonkSpacing(Collider other)
    {
        Vector3 moveAway = (transform.position - other.transform.position).normalized;
        Vector3 targetPOS = transform.position + moveAway * minSeperationDistance;
        agent.SetDestination(targetPOS);
    }

    void FaceTarget()
    {
        Vector3 direction = GameManager.instance.player.transform.position - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.001)
        {
            Quaternion rot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * FaceTargetSpeed);
        }
    }



    //casting
    IEnumerator Cast()
    {
        PauseForAMoment();
        Wave.SetActive(true);

        Wave.transform.localScale = new Vector3(12, 12, 12).normalized;
        yield return new WaitForSeconds(10);
        Wave.SetActive(false);
        gameObject.transform.GetChild(0).localScale = new Vector3(1, 1, 1);
    }

}
