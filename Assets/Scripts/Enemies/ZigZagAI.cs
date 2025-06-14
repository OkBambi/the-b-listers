using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZigZagAI : EnemyBase
{
    //the zigzagger is an enemy that will spawn in pairs from the stopwatch
    //the zigzagger should zigzag towards the player.
    //the zigzagger should jump at the player with physics when it enters a certain range
    //  the jump should have a tiny wind up and doesnt have to be very accurate
    //  for the jump, you will need to disable the navmesh agent temporily and re enable it when you land

    //the zigzagger will need the player's x and z pos, so just get the whole player
    [Space]
    [SerializeField] GameObject player;

    //the zigzagger also needs to move with the navmesh agent
    [SerializeField] NavMeshAgent agent;

    [Space]
    [Header("General Stats")]
    [SerializeField] float speed;
    [SerializeField] float angularSpeed;
    [SerializeField] float acceleration;

    [Header("Jump Stats")]
    [SerializeField] float jumpRange;
    [SerializeField] float jumpPower;


    [Header("ZigZag Stats")]
    //the zigzagger should have adjustable zag stats
    [SerializeField] float zagDistance;
    [SerializeField] float zagAngle; //if this is 0, then it will go straight

    //the zagger should decide which phase of zaggin their on ie. zag left first, zag right first
    [SerializeField] bool isZagLeft = false;
    private int zagModifier = 1;

    private void Awake()
    {
        player = GameManager.instance.player;
        agent.speed = speed;
        agent.angularSpeed = angularSpeed;
        agent.acceleration = acceleration;
        zagModifier = (isZagLeft ? -1 : 1);
        StartCoroutine(ZigZag());
    }

    IEnumerator ZigZag()
    {
        //first we zig, then we zag, zigzag

        bool isZigging = true;
        while (true)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < jumpRange)
            {
                agent.enabled = false;
                //Jump at the player
            }

            if (isZigging)
            {
                Zag();
            }
            else
            {
                Zig();
            }
            isZigging = !isZigging;
            yield return new WaitForSeconds(1f);
            //ZigzagPattern(zigzagPhase);
        }
        yield return null;
    }

    void Zig()
    {
        Vector3 zig = transform.position + Quaternion.AngleAxis(zagAngle * zagModifier, Vector3.up) * ((player.transform.position - transform.position).normalized * zagDistance);
        Debug.Log(zig);
        agent.SetDestination(zig);
    }

    void Zag()
    {
        Vector3 zag = transform.position + Quaternion.AngleAxis(-zagAngle * zagModifier, Vector3.up) * ((player.transform.position - transform.position).normalized * zagDistance);
        Debug.Log(zag);
        agent.SetDestination(zag);
    }

}
