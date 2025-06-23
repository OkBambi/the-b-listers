using UnityEngine;
using System.Collections.Generic;

//CHANGE COMMENTS WHEN CODE CHANGES PLZ
public class Snake : EnemyBase
{
    //HEALTH
    [Header("Health")]
    [SerializeField] List<SnakeHead> theBois;
    [SerializeField] List<int> colourIndexes;

    //MOVEMENT/ROAM - trying with waypoint (obstacle avoidance + flocking)
    [Header("Movement/Roam")]
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float wanderingRadius;
    [SerializeField] float wanderingTimer;

    [SerializeField] Rigidbody rb;

    private float timer;
    private Vector3 wanderingTarget;
    private bool isWandering = false;

    //FOLLOWING
    [Header("Following")]
    [SerializeField] Transform player;
    [SerializeField] float followRange = 10f;

    private bool isFollowing = false;

    //ATTACKING
    [Header("Attacking")]
    [SerializeField] float attackRange = 3f; //the distance at which the snake will attack
    [SerializeField] float attackCooldown = 2f; //how often the snake can attack
    [SerializeField] int attackDamage = 10; //how much damage each head deals
    [SerializeField] LayerMask playerLayer; //assigns the player's layer in the Inspector

    private float attackTimer; //to manage the attack cooldown
    private bool canAttack = true;


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
        attackTimer = attackCooldown; //initialises the attack timer
        GetNewWanderTarget();
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

            //ATTACKING
            if (distanceToPlayer < attackRange)
            {
                if (canAttack)
                {
                    Attack();
                    canAttack = false; //prevents immediate reattack
                    attackTimer = attackCooldown; //resets cooldown
                }
            }
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

        //updates attack cooldown
        if (!canAttack)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                canAttack = true;
            }
        }
    }

    //moves around the map
    void MoveTowards(Vector3 target)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            isWandering = false; //stops wandering when close to target
        }
    }

    void GetNewWanderTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderingRadius;
        randomDirection += transform.position;
        wanderingTarget = new Vector3(randomDirection.x, transform.position.y, randomDirection.z);
    }

    //attacking player
    void Attack()
    {
        Debug.Log("Snake is attacking!");
        // Here, you would trigger the attack animation/logic for the snake heads.
        // For example, you could tell each head to perform its attack action.
        foreach (SnakeHead head in theBois)
        {
            head.TryAttackPlayer(attackDamage); // Call a new method on SnakeHead
        }
    }

}
