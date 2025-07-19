using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.VisualScripting; // for NavMeshAgent

//CHANGE COMMENTS WHEN CODE CHANGES PLZ
public class Snake : EnemyBase
{
    //HEALTH
    [SerializeField] protected List<SnakeHead> theBois;
    [SerializeField] protected List<int> colourIndexes;

    //MOVEMENT/ROAM - trying with waypoint (obstacle avoidance + flocking)
    [SerializeField] protected float movementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float wanderingRadius;
    [SerializeField] float wanderingTimer;
    [SerializeField] protected float startHeight;

    [SerializeField] Rigidbody rb;

    [SerializeField] protected NavMeshAgent Snakeagent; // for NavMesh navigation

    protected float timer;
    private Vector3 wanderingTarget;

    //FOLLOWING
    [SerializeField] protected Transform player;
    [SerializeField] protected float followRange = 10f;


    //ATTACKING



    private void Awake()
    {
        OnAECAwake();

        for (int headIndex = 0; headIndex < theBois.Count; headIndex++)
        {
            int rand = Random.Range(0, colourIndexes.Count - 1);
            theBois[headIndex].setColor = (PrimaryColor)colourIndexes[rand];

            colourIndexes.Remove(colourIndexes[rand]);
        }

        Snakeagent = GetComponent<NavMeshAgent>();
        Snakeagent.speed = movementSpeed;
        Snakeagent.baseOffset = startHeight;
    }

    protected override void Start()
    {
        player = GameManager.instance.player.transform;
        timer = wanderingTimer;
        GetNewWanderTarget();
        name = "Snake";

        if (LevelModifierManager.instance.smallFastEnemies)
        {
            movementSpeed = movementSpeed * 2f;
            Snakeagent.speed = movementSpeed;
        }

        if (LevelModifierManager.instance.lessHealth)
        {
            int NewHP = hp;
            NewHP = hp / 2;
            hp = NewHP;
        }
    }


    void Update()
    {
        //MOVEMENT
        timer -= Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < followRange)
        {
            //follow the player
            Snakeagent.destination = player.position;
        }
        else
        {
            //wander if not following
            if (timer <= 0)
            {
                GetNewWanderTarget();
                Snakeagent.destination = wanderingTarget;
                timer = wanderingTimer;
            }
        }

    }


    void GetNewWanderTarget()
    {
        AudioManager.instance.Play("Snake", gameObject.GetComponent<AudioSource>());
        wanderingTarget = RandomNavPOS(transform.position, wanderingRadius, -1); // -1 for all layers
    }

    //helper function to get random position
    public static Vector3 RandomNavPOS(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    public override void takeDamage(PrimaryColor hitColor, int amount)
    {
        if (hitColor == setColor || hitColor == PrimaryColor.OMNI || setColor == PrimaryColor.OMNI)
        {
            hp -= amount;
            if (isAlive)
                DeathCheck();
        }
    }
}