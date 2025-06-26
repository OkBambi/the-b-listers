using UnityEngine;
using System.Collections.Generic;

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

    [SerializeField] Rigidbody rb;

    private float timer;
    private Vector3 wanderingTarget;
    private bool isWandering = false;

    //FOLLOWING
    [SerializeField] Transform player;
    [SerializeField] float followRange = 10f;

    private bool isFollowing = false;

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
    }

    void Start()
    {
        player = GameManager.instance.player.transform;
        timer = wanderingTimer;
        GetNewWanderTarget();
        name = "Snake";
    }


    void Update()
    {
        //MOVEMENT
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            GetNewWanderTarget();
            isWandering = true;
            timer = wanderingTimer;
        }

        if (isWandering)
        {
            MoveTowards(wanderingTarget);
        }

        // Example of raycast obstacle avoidance (simplified)
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            if (hit.collider.tag != "Player") // Ignore player
            {
                GetNewWanderTarget(); // Change direction immediately
            }
        }

        //FOLLOWING
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < followRange)
        {
            isFollowing = true;
            isWandering = false;
            MoveTowards(player.position);
        }
        else if (!isWandering)
        {
            isFollowing = false;
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                GetNewWanderTarget();
                isWandering = true;
                timer = wanderingTimer;
            }

            if (isWandering)
            {
                MoveTowards(wanderingTarget);
            }
        }
    }

    //moves around the map
    void MoveTowards(Vector3 target)
    {
        //Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
        Vector3 direction = (target - transform.position);
        direction.y = 0; //optional: keeps movement flat on the XZ plane
        direction.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        rb.MoveRotation(newRotation);

        Vector3 moveStep = direction * movementSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveStep);


        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            isWandering = false; //stops wandering when close to target
        }
    }

    void GetNewWanderTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderingRadius;


        if (Vector3.Distance(Vector3.zero, randomDirection + transform.position) < 50f)
        {
            randomDirection += transform.position;
            wanderingTarget = new Vector3(randomDirection.x, transform.position.y, randomDirection.z);
        }
        //my thinking is to send a raycast down to make sure that that spot is okay
        //if (Physics.Raycast(randomDirection, -Vector3.up, 1f))
        //{
        //    randomDirection += transform.position;
        //    wanderingTarget = new Vector3(randomDirection.x, transform.position.y, randomDirection.z);
        //}
        else
        {
            GetNewWanderTarget();
        }

        
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