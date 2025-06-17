using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Monk : EnemyBase
{
    [SerializeField] int FaceTargetSpeed;
    [SerializeField] float Casttimer;
    [SerializeField] float gongBonkRate;
    [SerializeField] float pauseDuration;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform Casting;
    [SerializeField] GameObject Wave;

    //noise 
    Color colorOriginal;
    bool PlayerInRange;
    float Lock_Color_Timer;
    
    Vector3 playerDir;

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
        playerDir = GameManager.instance.transform.position - transform.position;
        agent.SetDestination(GameManager.instance.player.transform.position);
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
        StartCoroutine(isroaming(pauseDuration));
    }
    IEnumerator isroaming(float pauseDuration)
    {
        yield return new WaitForSeconds(pauseDuration);
        agent.isStopped = false;
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
