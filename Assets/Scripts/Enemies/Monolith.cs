using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Monolith : EnemyBase
{
    [SerializeField] GameObject normalBoid;
    [SerializeField] GameObject angryBoid;
    [SerializeField] float rotationRadius = 2f;
    [SerializeField] float angularSpeed = 2f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float posX, posZ, angle= 0f;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] int normalBoidSpawnAmt;
    [SerializeField] int angryBoidSpawnAmt;

    private Rigidbody rb;
    bool isSpawning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
        ColorSelection(setColor);
        UpdateBoidAwareness();
    }

    // Update is called once per frame
    void Update()
    {
        movement();

        if (!isSpawning)
        {
            StartCoroutine(SpawnBoids());
        }
    }

    IEnumerator SpawnBoids()
    {
        isSpawning = true;
        yield return new WaitForSeconds(timeBetweenSpawns);
        for (int spawnCount = 0; spawnCount < normalBoidSpawnAmt; spawnCount++)//normal spawn
        {
            normalBoid = Instantiate(normalBoid, transform.position, Quaternion.identity);
            //normalBoid.gameObject.GetComponent<BoidAI>().setColor = this.setColor;
        }

        for (int spawnCount = 0; spawnCount < angryBoidSpawnAmt; spawnCount++)//angry spawn
        {
            angryBoid = Instantiate(angryBoid, transform.position, Quaternion.identity);
            //angryBoid.gameObject.GetComponent<BoidAI>().setColor = this.setColor;
        }

        isSpawning = false;
    }

    void movement()
    {
        //transform.RotateAround(Vector3.zero, Vector3.up,   * Time.deltaTime);//allows the monolith to spin around the center point
        //transform.Rotate(new Vector3(0,  , 0) * Time.deltaTime);//allows the monolith to rotate around the y axis.
        posX = rb.position.x + Mathf.Cos(angle) * rotationRadius;
        posZ = rb.position.z + Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector3(posX, transform.position.y, posZ);
        angle += angularSpeed * Time.deltaTime;

        if (angle >= 360f)
        {
            angle = 0f;
        }
    }
}
