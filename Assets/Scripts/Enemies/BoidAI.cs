using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoidAI : EnemyBase
{
    [Header("Components")]
    [SerializeField] protected Rigidbody rb;
    
    [SerializeField] GameObject stageGround;
    [SerializeField] protected GameObject player;

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

    [Space]
    [Header("Noise")]
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
    [SerializeField] float startUpForce = 10f;

    [SerializeField] float maxHeight = 50f;
    [SerializeField] float minHeight = 5;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        RandomizeColor();
        //finding the ground
        stageGround = EnemyManager.instance.stage;

        //finding the player
        player = GameManager.instance.player;
        rb.AddForce(Vector3.up * startUpForce * Time.deltaTime, ForceMode.Acceleration);
    }

    protected override void Start()
    {
        ColorSelection(setColor);
        base.UpdateBoidAwareness();
        StartCoroutine(NoiseWeights());
        name = "Boid";
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {

        //the boids should not touch the ground and should not go too high
        //they should also stay on stage
        StageAwareness();

        //apply noise
        //we apply noise through a coroutine

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

    void BoidMovement()
    {
        //variable  initialization
        int enemiesInRange = 0;
        Vector3 velocityTotal = new Vector3(0, 0, 0);
        Vector3 totalLocations = new Vector3(0, 0, 0);

        foreach (Rigidbody enemy in EnemyManager.instance.boidReferences)
        {
            if (enemy == rb) continue;
            float distanceToBoid = Vector3.Distance(transform.position, enemy.position);

            //Avoidance
            //loop through the boids and see how close each one is to this boid
            //if the boid is too close, move away
            if (distanceToBoid <= protectedRange)
                rb.AddForce((((transform.position - enemy.position) * protectedRange) - (transform.position - enemy.position)) * separationWeight * separationNoise * Time.deltaTime, ForceMode.Acceleration);
          

            if (distanceToBoid <= visualRange)
            {
                velocityTotal += enemy.linearVelocity;
                totalLocations += enemy.transform.position;
                ++enemiesInRange;
            }
        }

        if (enemiesInRange > 0)
        {
            //Alignment
            //calculate the average velocity of all the boids
            Vector3 averageVelocity = velocityTotal / enemiesInRange;
            //using the average velocity, move the boid in that direction
            rb.AddForce(averageVelocity * alignmentWeight * alignmentNoise * Time.deltaTime, ForceMode.Acceleration);

            //Cohesion
            //get the average location of all the boids
            Vector3 averageLocation = totalLocations / enemiesInRange;
            //move the boid towards the average location
            rb.AddForce((averageLocation - transform.position) * cohesionWeight * cohesionNoise * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    protected IEnumerator NoiseWeights()
    {
        while (this.isActiveAndEnabled)
        {
            separationNoise = Random.Range(noiseMin, noiseMax);
            alignmentNoise = Random.Range(noiseMin, noiseMax);
            cohesionNoise = Random.Range(noiseMin, noiseMax);
            stageNoise = Random.Range(noiseMin, noiseMax);
            playerNoise = Random.Range(noiseMin, noiseMax);

            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
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

        //boids shouldnt dive over the player, disable this ground detection when approaching the player
        Vector3 toPlayer = player.transform.position - transform.position;
        if (Vector3.Angle(toPlayer.normalized, transform.forward) <= 5f)
            minHeight = 2f;
        else
            minHeight = 5f;


        if (transform.position.y <= minHeight)
            rb.AddForce(Vector3.up * (minHeight - transform.position.y) * groundAvoidance * Time.deltaTime, ForceMode.Acceleration);
        //id say that we have more of a soft cap on the height, so they can breach the limit like a fish out of water
        if (transform.position.y >= maxHeight)
            rb.AddForce(-Vector3.up * (transform.position.y - maxHeight) * skyAvoidance * Time.deltaTime, ForceMode.Acceleration);

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

    public override void DeathCheck()
    {
        if (hp <= 0)
        {
            isAlive = false;
            RemoveSelfFromTargetList();
            ComboManager.instance.AddScore(score);
            ComboFeed.theInstance.AddNewComboFeed("+ " + ComboManager.instance.getScoreTimesMult(score).ToString() + " " + transform.name);
            Destroy(gameObject);
            return;
        }
    }
}
