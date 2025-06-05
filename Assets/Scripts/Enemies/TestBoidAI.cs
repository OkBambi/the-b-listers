using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestBoidAI : EnemyBase
{
    [Header("Components")]
    [SerializeField] Rigidbody rb;
    public List<Rigidbody> boids;
    [SerializeField] LayerMask hitLayer;
    [SerializeField] GameObject stageGround;
    [SerializeField] GameObject player;

    [Header("Ranges")]
    [SerializeField] float protectedRange;
    [SerializeField] float visualRange;

    [Space]
    [Header("Speeds")]
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;

    [Space]
    [Header("Weights")]
    [SerializeField] float separationWeight = 8f;
    [SerializeField] float alignmentWeight = 1.0f;
    [SerializeField] float cohesionWeight = 1.2f;
    [SerializeField] float stageWeight = 0.5f;
    [SerializeField] float playerWeight = 1f;

    [SerializeField] float noiseMin = 0.8f;
    [SerializeField] float noiseMax = 1.2f;

    private float separationNoise = 1.2f;
    private float alignmentNoise = 1.2f;
    private float cohesionNoise = 1.2f;
    private float stageNoise = 1.2f;
    private float playerNoise = 1.2f;

    
    [Space]
    [Header("Constraints")]
    [SerializeField] float groundAvoidance = 10f;
    [SerializeField] float skyAvoidance = 2f;

    [SerializeField] float maxHeight = 50f;
    [SerializeField] float minHeight = 5;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ColorSelection(setColor);
        //finding the ground
        GameObject[] objects = FindObjectsByType<GameObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach(GameObject potentialGround in objects)
        {
            if(potentialGround.layer == LayerMask.NameToLayer("Ground"))
            {
                stageGround = potentialGround;
                break;
            }
        }

        //finding the player
        if (FindAnyObjectByType<Player>() != null)
            player = FindAnyObjectByType<Player>().gameObject;
        else
        {
            //testing condition
            player = GameObject.FindGameObjectWithTag("Player");
        }

            


            //finding other boids
            UpdateBoidAwareness();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //the boids should not touch the ground and should not go too high
        //they should also stay on stage
        StageAwareness();

        //apply noise
        NoiseWeights();

        //the boids should not touch eachother
        //the boids should move in alignment with the group
        //the boids should stay near the group
        BoidMovement();

        //the boids should 'magnitize towards the player'
        PlayerMagnetism();

        //the boids should keep moving
        ConstantMovement();

        //the boids should look in the direction of movement
        LookAtMoveDirection();
    }

    void UpdateBoidAwareness()
    {
        TestBoidAI[] activeboids = FindObjectsByType<TestBoidAI>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        for (int boidCount = 0; boidCount < activeboids.Length; boidCount++)
        {
            activeboids[boidCount].boids.Add(rb);
        }
    }

    void BoidMovement()
    {
        //variable  initialization
        int boidsInRange = 0;
        Vector3 velocityTotal = new Vector3(0, 0, 0);
        Vector3 totalLocations = new Vector3(0, 0, 0);

        foreach (Rigidbody boid in boids)
        {
            float distanceToBoid = Vector3.Distance(transform.position, boid.position);

            //Avoidance
            //loop through the boids and see how close each one is to this boid
            //if the boid is too close, move away
            if (distanceToBoid <= protectedRange)
                rb.AddForce((((transform.position - boid.position) * protectedRange) - (transform.position - boid.position)) * separationWeight * separationNoise * Time.deltaTime, ForceMode.Acceleration);
            

            if (distanceToBoid <= visualRange)
            {
                velocityTotal += boid.linearVelocity;
                totalLocations += boid.transform.position;
                ++boidsInRange;
            }

            if (boidsInRange > 0)
            {
                //Alignment
                //calculate the average velocity of all the boids
                Vector3 averageVelocity = velocityTotal / boidsInRange;
                //using the average velocity, move the boid in that direction
                rb.AddForce(averageVelocity * alignmentWeight * alignmentNoise * Time.deltaTime, ForceMode.Acceleration);

                //Cohesion
                //get the average location of all the boids
                Vector3 averageLocation = totalLocations / boidsInRange;
                //move the boid towards the average location
                rb.AddForce((averageLocation - transform.position) * cohesionWeight * cohesionNoise * Time.deltaTime, ForceMode.Acceleration);
            }
        }
    }

    void NoiseWeights()
    {
        separationNoise = Random.Range(noiseMin, noiseMax);
        alignmentNoise = Random.Range(noiseMin, noiseMax);
        cohesionNoise = Random.Range(noiseMin, noiseMax);  
        stageNoise = Random.Range(noiseMin, noiseMax);
        playerNoise = Random.Range(noiseMin, noiseMax);

    }

    void ConstantMovement()
    {
        //if the magnitude of the linear velocity is less than the minimum speed
        //grab the unit vector of the linear velocity and multiply it by the minSpeed
        if (rb.linearVelocity.magnitude < minSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * minSpeed;
        else if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
    }

    void LookAtMoveDirection()
    {
        transform.LookAt(transform.position + rb.linearVelocity);
    }

    void StageAwareness()
    {
        #region Height Control

        //so we dont want the boid to hit the floor, like really dont touch the floor,
        RaycastHit hit;

        //boids shouldnt dive over the player, disable this ground detection when approaching the player
        Vector3 toPlayer = player.transform.position - transform.position;
        if (Vector3.Angle(toPlayer.normalized, transform.forward) >= 5f)
        {
            minHeight = 2f;
        }
        else
        {
            minHeight = 5f;
        }

        if (transform.position.y <= minHeight)
        {
            rb.AddForce(Vector3.up * (minHeight - transform.position.y) * groundAvoidance * Time.deltaTime, ForceMode.Acceleration);
        }
        //id say that we have more of a soft cap on the height, so they can breach the limit like a fish out of water
        if (transform.position.y >= maxHeight)
        {
            rb.AddForce(-Vector3.up * (transform.position.y - maxHeight) * skyAvoidance * Time.deltaTime, ForceMode.Acceleration);
        }

        #endregion

        #region Staying on Stage

        //THIS WORKS
        if (Vector3.Distance(stageGround.transform.position, transform.position) >= stageGround.transform.localScale.x * 0.45)
            rb.AddForce((stageGround.transform.position - transform.position) * stageWeight * stageNoise * Time.deltaTime, ForceMode.Acceleration);

        #endregion
    }

    void PlayerMagnetism()
    {
        rb.AddForce((player.transform.position - transform.position).normalized * playerWeight * playerNoise * Time.deltaTime, ForceMode.Acceleration);
    }

    private void OnDestroy()
    {
        TestBoidAI[] activeboids = FindObjectsByType<TestBoidAI>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        for (int boidCount = 0; boidCount < activeboids.Length; boidCount++)
        {
            activeboids[boidCount].boids.Remove(rb);
        }
    }
}
