using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Monk : MonoBehaviour, IDamage
{
    [SerializeField] int HP;
    [SerializeField] int FaceTargetSpeed;
    [SerializeField] float Casttimer;
    [SerializeField] float gongBonkRate;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform Casting;
    [SerializeField] GameObject wave;
    [SerializeField] Renderer model;

    //noise 
    Color colorOriginal;
    bool PlayerInRange;
    float Lock_Color_Timer;
    Vector3 playerDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Casttimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange)
        {
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
    }

    void Cast()
    {
        //projectile
        Instantiate(wave, transform.position, Quaternion.identity);
        //if player is in the sphere cast
        // lock the player color choice for 3 seconds
        //wait a moment
        //swap through the colors
    }

    void FaceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * FaceTargetSpeed);
    }
    public void takeDamage(PrimaryColor hitColor, int amount)
    {
        throw new System.NotImplementedException();
    }
}
