using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI; // for NavMeshAgent

//CHANGE COMMENTS WHEN CODE CHANGES PLZ
public class Snake : EnemyBase
{
    //HEALTH
    [SerializeField] List<SnakeHead> theBois;
    [SerializeField] List<int> colourIndexes;

    //MOVEMENT/ROAM - trying with waypoint (obstacle avoidance + flocking)
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float wanderingRadius;
    [SerializeField] float wanderingTimer;
    [SerializeField] float startHeight;

    [SerializeField] Rigidbody rb;

    [SerializeField] NavMeshAgent Snakeagent; // for NavMesh navigation

    private float timer;
    private Vector3 wanderingTarget;
    //private bool isWandering = false;

    //FOLLOWING
    [SerializeField] Transform player;
    [SerializeField] float followRange = 10f;

    //private bool isFollowing = false;

    //ATTACKING



    private void Awake()
    {
        OnAECAwake();

        for (int headIndex = 0; headIndex < theBois.Count; headIndex++)
        {
            int rand = Random.Range(0, colourIndexes.Count - 1);
            theBois[headIndex].setColor = (PrimaryColor)colourIndexes[rand];

            Debug.Log((PrimaryColor)colourIndexes[rand]);
            colourIndexes.Remove(colourIndexes[rand]);
        }

        Snakeagent = GetComponent<NavMeshAgent>();
        Snakeagent.speed = movementSpeed;
    }

    void Start()
    {
        player = GameManager.instance.player.transform;
        timer = wanderingTimer;
        startHeight = transform.position.y;
        GetNewWanderTarget();
        name = "Snake";
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

    //moves around the map
    //void MoveTowards(Vector3 target)
    //{
    //    //Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
    //    //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    //    //transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
    //    Vector3 direction = (target - transform.position);
    //    direction.y = 0; //optional: keeps movement flat on the XZ plane
    //    direction.Normalize();

    //    Quaternion targetRotation = Quaternion.LookRotation(direction);
    //    Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    //    rb.MoveRotation(newRotation);

    //    Vector3 moveStep = direction * movementSpeed * Time.deltaTime;
    //    rb.MovePosition(rb.position + moveStep);


    //    if (Vector3.Distance(transform.position, target) < 0.5f)
    //    {
    //        isWandering = false; //stops wandering when close to target
    //    }
    //}

    void GetNewWanderTarget()
    {
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



        //if (Vector3.Distance(Vector3.zero, randomDirection + transform.position) < EnemyManager.instance.stage.transform.localScale.x / 2f)
        //{
        //    randomDirection += transform.position;
        //    wanderingTarget = new Vector3(randomDirection.x, transform.position.y, randomDirection.z);
        //}
        ////my thinking is to send a raycast down to make sure that that spot is okay
        ////if (Physics.Raycast(randomDirection, -Vector3.up, 1f))
        ////{
        ////    randomDirection += transform.position;
        ////    wanderingTarget = new Vector3(randomDirection.x, transform.position.y, randomDirection.z);
        ////}
        //else
        //{
        //    GetNewWanderTarget();
        //}

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