using UnityEngine;

//CHANGE COMMENTS WHEN CODE CHANGES PLZ
public class Snake : MonoBehaviour
{
    //HEALTH


    //MOVEMENT/ROAM - trying with waypoint (obstacle avoidance + flocking)
    public float movementSpeed;
    public float rotationSpeed;
    public float wanderingRadius;
    public float wanderingTimer;

    private float timer;
    private Vector3 wanderingTarget;
    private bool isWandering = false;


    void Start()
    {
        timer = wanderingTimer;
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

    //follows the player when in view


    //attacking player


    //health

}
