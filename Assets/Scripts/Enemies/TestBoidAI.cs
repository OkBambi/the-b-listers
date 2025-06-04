using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestBoidAI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody rb;
    public List<Rigidbody> boids;
    [SerializeField] LayerMask hitLayer;
    [SerializeField] GameObject stageGround;

    [Header("Ranges")]
    [SerializeField] float protectedRange;
    [SerializeField] float visualRange;

    [Space]
    [Header("Speeds")]
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float currentSpeed;

    [SerializeField] float rotateSpeed;
    [Space]
    [Header("Weights")]
    [SerializeField] float separationWeight = 8f;
    [SerializeField] float alignmentWeight = 1.0f;
    [SerializeField] float cohesionWeight = 1.2f;
    [SerializeField] float stageWeight = 0.5f;

    //[SerializeField] float avoidanceWeight = 5.0f;
    [Space]
    [SerializeField] float groundAvoidance = 10f;
    [SerializeField] float skyAvoidance = 2f;

    [SerializeField] float playerAttraction = 1.0f;
    [SerializeField] float maxHeight = 50f;
    [SerializeField] float minHeight = 5;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] objects = FindObjectsByType<GameObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach(GameObject potentialGround in objects)
        {
            if(potentialGround.layer == LayerMask.NameToLayer("Ground"))
            {
                stageGround = potentialGround;
                break;
            }
        }
        
        UpdateBoidAwareness();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //the boids should not touch the ground and should not go too high
        //they should also stay on stage
        StageAwareness();

        //the boids should not touch eachother
        Avoidance();

        //the boids should move in alignment with the group
        Alignment();

        //the boids should stay near the group
        Cohesion();

        //the boids should stay on the stage
        

        //the boids should 'magnitize towards the player'

        //the boids should keep moving
        //ConstantMovement();
        LookAtMoveDirection();

    }

    void UpdateBoidAwareness()
    {
        //this method will need to be fixed later, mainly for testing purposes
        //when new boids spawn, the pre-existing boids will need to be updated
        
        TestBoidAI[] activeboids = FindObjectsByType<TestBoidAI>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        for (int boidCount = 0; boidCount < activeboids.Length; boidCount++)
        {   
            if (activeboids[boidCount] != this)
            {
                boids.Add(activeboids[boidCount].GetComponent<Rigidbody>());

                //if (!activeboids[boidCount].boids.Contains(rb) && !activeboids[boidCount].boids.Contains(rb))
                //{
                //    activeboids[boidCount].boids.Add(rb);
                //}
                //maybe instead of this boid updating it's own list, new boids will update all boids?
                //this is where we would have a single manager for the boids
                //else
                //{
                //    activeboids[boidCount].boids.Remove(this.transform);
                //}
            }

        }
    }

    void ConstantMovement()
    {
        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
        rb.AddForce(transform.forward * currentSpeed * Time.deltaTime);
    }

    void LookAtMoveDirection()
    {
        transform.LookAt(transform.position + rb.linearVelocity);
    }


    void Avoidance()
    {
        //loop through the boids and see how close each one is to this boid
        //if the boid is too close, move away
        foreach(Rigidbody boid in boids)
        {
            float distanceToBoid = Vector3.Distance(transform.position, boid.position);
            if (distanceToBoid <= protectedRange)
            {
                rb.AddForce( (((transform.position - boid.position) * protectedRange) - (transform.position - boid.position)) * separationWeight * Time.deltaTime, ForceMode.Acceleration);
            }
        }
    }

    void Alignment()
    {
        Vector3 velocityTotal = new Vector3(0, 0, 0);
        int boidsInRange = 0;

        //get all the boids in visual range
        foreach (Rigidbody boid in boids)
        {
            float distanceToBoid = Vector3.Distance(transform.position, boid.position);

            if (distanceToBoid <= visualRange)
            {
                velocityTotal += boid.linearVelocity;
                ++boidsInRange;
            }
        }
        
        if (boidsInRange > 0)
        {
            //calculate the average velocity of all the boids
            Vector3 averageVelocity = velocityTotal / boidsInRange;
            //using the average velocity, move the boid in that direction
            rb.AddForce(averageVelocity * separationWeight * Time.deltaTime, ForceMode.Acceleration);
        }
            
    }

    void Cohesion()
    {
        Vector3 totalLocations = new Vector3(0, 0, 0);
        int boidsInRange = 0;

        //get all the boids in visual range
        foreach (Rigidbody boid in boids)
        {
            float distanceToBoid = Vector3.Distance(transform.position, boid.position);

            if (distanceToBoid <= visualRange)
            {
                totalLocations += boid.transform.position;
                ++boidsInRange;
            }
        }
        if (boidsInRange > 0)
        {
            //get the average location of all the boids
            Vector3 averageLocation = totalLocations / boidsInRange;

            //move the boid towards the average location
            rb.AddForce((averageLocation - transform.position) * cohesionWeight * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    void StageAwareness()
    {
        #region Height Control

        //so we dont want the boid to hit the floor, like really dont touch the floor,
        RaycastHit hit;

        Debug.DrawRay(transform.position, -Vector3.up * minHeight, Color.red);
        if (Physics.SphereCast(transform.position, 0.25f, -Vector3.up, out hit, minHeight, hitLayer))
        {
            Debug.Log("send out cast");
            Debug.Log(hit.collider.name);
            rb.AddForce(Vector3.up * (minHeight - (transform.position.y - hit.collider.transform.position.y)) * groundAvoidance * Time.deltaTime, ForceMode.Acceleration);
        }

        //id say that we have more of a soft cap on the height, so they can breach the limit like a fish out of water
        if (transform.position.y >= maxHeight)
        {
            rb.AddForce(-Vector3.up * (transform.position.y - maxHeight) * skyAvoidance * Time.deltaTime, ForceMode.Acceleration);
        }

        #endregion

        #region Staying on Stage

        //THIS DOESNT WORK :)
        if (Vector3.Distance(stageGround.transform.position, transform.position) >= stageGround.transform.localScale.x)
            rb.AddForce((stageGround.transform.position - transform.position) * stageWeight * Time.deltaTime);

        #endregion




    }
}
