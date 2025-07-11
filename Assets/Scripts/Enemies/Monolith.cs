using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Monolith : EnemyBase
{
    [SerializeField] GameObject normalBoid;
    [SerializeField] GameObject angryBoid;
    //[SerializeField] float rotationRadius = 2f;
    //[SerializeField] float angularSpeed = 2f;
    [SerializeField] float rotationSpeed = 1f;
    //[SerializeField] float posX, posZ, angle = 0f;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] int normalBoidSpawnAmt;
    [SerializeField] int angryBoidSpawnAmt;

    private Rigidbody rb;
    bool isSpawning;

    private void Awake()
    {
        RandomizeColor();
        OnAECAwake();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
        ColorSelection(setColor);
        UpdateBoidAwareness();
        name = "Monolith";
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
        AudioManager.instance.Play("Monolith_Growl");
        //thiss should make it so that the first boid spawn for monoliths is almost instant, but afterwards, it will be the correct amount of time
        yield return new WaitForSeconds(timeBetweenSpawns / 4f);
        for (int spawnCount = 0; spawnCount < normalBoidSpawnAmt; spawnCount++)//normal spawn
        {
            Instantiate(normalBoid, transform.position, Quaternion.identity);
            if (LevelModifierManager.instance.doubleEnemies)
                Instantiate(normalBoid, transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)), Quaternion.identity);
        }

        for (int spawnCount = 0; spawnCount < angryBoidSpawnAmt; spawnCount++)//angry spawn
        {
            Instantiate(angryBoid, transform.position, Quaternion.identity);
            if (LevelModifierManager.instance.doubleEnemies)
                Instantiate(angryBoid, transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)), Quaternion.identity);
        }
        yield return new WaitForSeconds(timeBetweenSpawns * (3f / 4f));

        isSpawning = false;
        StartCoroutine(ShakePos(0.2f, 0.1f));
        StartCoroutine(ShakeSize(0.2f, 0.1f));
    }

    void movement()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, rotationSpeed  * Time.deltaTime);//allows the monolith to spin around the center point
        transform.Rotate(new Vector3(0, rotationSpeed , 0) * Time.deltaTime);//allows the monolith to rotate around the y axis.
        //fix
    }
}
