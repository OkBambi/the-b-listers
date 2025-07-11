using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Monk : EnemyBase
{
    [Header("Casting")]
    [SerializeField] GameObject Wave;
    [SerializeField] float pauseToCastTimer;
    [SerializeField] float waveSize;
    private Vector3 waveSizeOriginal;
    [SerializeField] float waveGrowthSpeed;
    float waveGrowthTimer = 0;


    [Header("roaming movement")]
    [SerializeField] int faceTargetSpeed;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float roamDist;
    [SerializeField] float StopTime;
    float roamTimer;

    [Header("Seperation")]
    [SerializeField] float minSeperationDistance;
    [SerializeField] GameObject SeperationDome;

    //detectors 
    public bool monkInRange;
    bool isCasting = false;
    Color colorOriginal;

    Vector3 startingPOS;

    private void Awake()
    {
        OnAECAwake();
        RandomizeColor();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        startingPOS = transform.position;
        ColorSelection(setColor);
        agent = GetComponent<NavMeshAgent>();
        colorOriginal = model.material.color;
        if (Wave != null)
        {
            waveSizeOriginal = Wave.transform.localScale;
        }
        name = "Monk";
        roam();

        if (LevelModifierManager.instance.lowEnemyCooldowns)
        {
            StopTime = StopTime * 0.25f;
            pauseToCastTimer = pauseToCastTimer * 0.25f;
            roamDist = roamDist * 0.25f;
        }

        if (LevelModifierManager.instance.smallFastEnemies)
        {
            model.transform.localScale = model.transform.localScale * 0.75f;
            agent.speed = agent.speed * 2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        roamCheck();
        if (agent.remainingDistance < 0.01f && !agent.pathPending && !isCasting)
        {
            roamTimer += Time.deltaTime;

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
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
        }
    }

    //casting
    IEnumerator Cast()
    {
        isCasting = true;
        PauseForAMoment();

        for (int i = 0; i < 2; i++)
        {
            AudioManager.instance.Play("Monk_Blinker");
            model.material.color = Color.white;
            yield return new WaitForSeconds(0.05f);
            model.material.color = colorOriginal;
            yield return new WaitForSeconds(0.05f);
        }
        AudioManager.instance.Play("Monk_Cast");
       
        yield return new WaitForSeconds(0.20f);
        Wave.SetActive(true);
        Wave.transform.localScale = Vector3.zero;
        waveGrowthTimer = 0;
        while (waveGrowthTimer < waveGrowthSpeed)
        {
            float growthRate = waveGrowthTimer / waveGrowthSpeed;
            Wave.transform.localScale = Vector3.Lerp(Vector3.zero, waveSizeOriginal * waveSize, growthRate);
            waveGrowthTimer += Time.deltaTime;
            yield return null;
        }
        Wave.transform.localScale = waveSizeOriginal;



        if (monkInRange)
        {
            monkScan();
        }
        roam();
        isCasting = false;
    }

}
