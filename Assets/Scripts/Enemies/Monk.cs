using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Monk : EnemyBase
{
    [SerializeField] int FaceTargetSpeed;
    [SerializeField] float Casttimer;
    [SerializeField] float gongBonkRate;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform Casting;
    [SerializeField] GameObject wave;

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
    }

    // Update is called once per frame
    void Update()
    {
        playerDir = GameManager.instance.transform.position - transform.position;
        agent.SetDestination(GameManager.instance.player.transform.position);
        if (Casttimer > gongBonkRate)
        {
            Cast();
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            FaceTarget();

        }
    }

    IEnumerator Cast()
    {
        //projectile
        Instantiate(wave, transform.position, Quaternion.identity);

        if (PlayerInRange)
        {
            //if player is in the sphere cast

            yield return new WaitForSeconds(0.1f);
            // lock the player color choice for 3 seconds
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
