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

    [SerializeField] AudioSource growl;
    [SerializeField] AudioSource pop;
    [SerializeField] AudioSource ambiance;

    private Rigidbody rb;
    bool isSpawning;

    private void Awake()
    {
        //RandomizeColor();
        OnAECAwake();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
        ColorSelection(setColor);
        UpdateBoidAwareness();
        name = "Monolith";

        if (LevelModifierManager.instance.lowEnemyCooldowns)
            timeBetweenSpawns = timeBetweenSpawns * 0.25f;

        if (LevelModifierManager.instance.smallFastEnemies)
        {
            model.transform.localScale = model.transform.localScale * 0.75f;
            rotationSpeed = rotationSpeed * 2f;
        }

        if (LevelModifierManager.instance.lessHealth)
        {
            int NewHP = hp;
            NewHP = hp / 2;
            hp = NewHP;
        }

        AudioManager.instance.Play("Enemy_Ambiance", 0.5f, ambiance);
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
        
        //thiss should make it so that the first boid spawn for monoliths is almost instant, but afterwards, it will be the correct amount of time
        yield return new WaitForSeconds(timeBetweenSpawns / 4f);
        AudioManager.instance.Play("Monolith_Growl", Random.Range(0.9f, 1.1f), growl);
        for (int spawnCount = 0; spawnCount < normalBoidSpawnAmt; spawnCount++)//normal spawn
        {
            yield return new WaitForSeconds(0.1f);
            Instantiate(normalBoid, transform.position, Quaternion.identity);
            AudioManager.instance.Play("Boid_Spawn", Random.Range(0.8f, 1.2f), pop);
            StartCoroutine(ShakePos(0.2f, 0.05f));
            StartCoroutine(ShakeSize(0.2f, 0.05f));
            StartCoroutine(CameraShake.instance.ShakeWithDistance(0.1f, 0.2f, gameObject));
            if (LevelModifierManager.instance.doubleEnemies)
            {
                yield return new WaitForSeconds(0.05f);
                Instantiate(normalBoid, transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)), Quaternion.identity);
                AudioManager.instance.Play("Boid_Spawn", Random.Range(0.8f, 1.2f), pop);
                StartCoroutine(ShakePos(0.2f, 0.05f));
                StartCoroutine(ShakeSize(0.2f, 0.05f));
                StartCoroutine(CameraShake.instance.ShakeWithDistance(0.1f, 0.2f, gameObject));
            }
        }

        for (int spawnCount = 0; spawnCount < angryBoidSpawnAmt; spawnCount++)//angry spawn
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(angryBoid, transform.position, Quaternion.identity);
            AudioManager.instance.Play("Boid_Spawn", Random.Range(0.8f, 1.2f), pop);
            StartCoroutine(ShakePos(0.4f, 0.05f));
            StartCoroutine(ShakeSize(0.4f, 0.05f));
            StartCoroutine(CameraShake.instance.ShakeWithDistance(0.1f, 0.2f, gameObject));
            if (LevelModifierManager.instance.doubleEnemies)
            {
                yield return new WaitForSeconds(0.05f);
                Instantiate(angryBoid, transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)), Quaternion.identity);
                AudioManager.instance.Play("Boid_Spawn", Random.Range(0.8f, 1.2f), pop);
                StartCoroutine(ShakePos(0.4f, 0.05f));
                StartCoroutine(ShakeSize(0.4f, 0.05f));
                StartCoroutine(CameraShake.instance.ShakeWithDistance(0.1f, 0.2f, gameObject));
            }
        }
        yield return new WaitForSeconds(timeBetweenSpawns * (3f / 4f));

        isSpawning = false;
        //StartCoroutine(ShakePos(0.2f, 0.1f));
        //StartCoroutine(ShakeSize(0.2f, 0.1f));
    }

    void movement()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, rotationSpeed  * Time.deltaTime);//allows the monolith to spin around the center point
        transform.Rotate(new Vector3(0, rotationSpeed , 0) * Time.deltaTime);//allows the monolith to rotate around the y axis.
        //fix
    }
}
