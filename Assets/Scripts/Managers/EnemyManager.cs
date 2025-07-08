using NUnit.Framework.Internal;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [Space]

    [SerializeField] float mapSize;

    [Space]


    [SerializeField] GameObject spawnIndicator;
    [Space]

    //so we have a list of enemies that spawn in a repeating order
    [SerializeField] List<GameObject> spawnList;
    [SerializeField] List<Mesh> enemyMeshList;

    //this will track what enemy to spawn
    [SerializeField] int spawnIndex;

    [SerializeField] bool isSpawningEnemies = true;

    public event Action StopwatchTrigger;

    [Header("Boid Stuff")]
    //so we stop getting stuff on awake
    //this is the list of objects tht boids should consider
    [SerializeField] public List<Rigidbody> boidReferences;
    [SerializeField] public GameObject stage;

    //and we have a spawn limit on the enemies
    [SerializeField] int AEC;
    [SerializeField] int currentEC;

    //boids do not count towards AEC, but every other enemy counts as one AEC
    //we will also have a ticker that tracks the number of enemy kills.
    [SerializeField] int ticker;
    [SerializeField] int tickerLimit = 5;
    //when the ticker reaches 5 enemy kills, it will increase the AEC by 1;

    public Material flashMat;
    public Material spawnMat;
    private void Awake()
    {
        instance = this;
        GameObject[] objects = FindObjectsByType<GameObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (GameObject potentialGround in objects)
        {
            if (potentialGround.layer == LayerMask.NameToLayer("Ground") || potentialGround.CompareTag("groundTag"))
            {
                stage = potentialGround;
                break;
            }
        }
    }

    #region AEC
    private void IncrementAEC()
    {
        ++AEC;
    }

    public void ResetAEC()
    {
        //this is for restarting the game
        AEC = 1;
    }
    #endregion

    #region CurrentEC
    public void IncrementCurrentEC()
    {
        ++currentEC;
    }

    public void DecrementCurrentEC()
    {
        --currentEC;
    }
    #endregion

    #region Ticker
    private void IncrementTicker()
    {
        ++ticker;
    }

    private void ResetTicker()
    {
        ticker = 0;
    }
    #endregion

    #region IAEC Interface Methods()
    public void OnAECDestroy()
    {
        IncrementTicker();
        DecrementCurrentEC();
        Invoke("SpawnEnemy", 1.0f);
        if (ticker >= tickerLimit)
        {
            ResetTicker();
            IncrementAEC();
            Invoke("SpawnEnemy", 1.0f);
        }
    }

    public void OnAECAwake()
    {
        IncrementCurrentEC();
    }
    #endregion


    #region EnemySpawning

    public void SpawnFirstEnemy()
    {
        //safety checks
        if (!isSpawningEnemies) return;
        if (currentEC >= AEC) return;

        Vector3 spawnLocation = new Vector3(0, 6f, 15f);


        //retry in one second if theres no good spots right now
        if (spawnLocation == Vector3.zero)
        {
            Invoke("SpawnEnemy", 1f);
            return;
        }

        //spawn the indicator which will telegraph the enemy spawn
        SpawnIndicator sp = Instantiate(spawnIndicator, spawnLocation, Quaternion.identity).GetComponent<SpawnIndicator>();
        sp.enemyToSpawn = spawnList[spawnIndex];

        //what the spawn indicator will actually show
        switch (sp.enemyToSpawn.name)
        {
            case "Monolith_Enemy":
                sp.enemyMesh = enemyMeshList[0];
                sp.SetMesh(enemyMeshList[0]);
                sp.modelFrame.transform.localScale = new Vector3(162f, 67f, 322f);
                sp.modelFrame.transform.rotation = Quaternion.Euler(-90f, 0f, 180f);
                break;
            case "StopWatch":
                sp.enemyMesh = enemyMeshList[1];
                sp.SetMesh(enemyMeshList[1]);
                break;
            case "Monk_Enemy":
                sp.enemyMesh = enemyMeshList[2];
                sp.SetMesh(enemyMeshList[2]);
                sp.modelFrame.transform.localScale = new Vector3(20f, 20f, 20f);
                sp.modelFrame.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
                break;
            case "Snake":
                sp.enemyMesh = enemyMeshList[3];
                sp.SetMesh(enemyMeshList[3]);
                //sp.modelFrame.transform.localScale = new Vector3(162f, 67f, 322f);
                //sp.modelFrame.transform.rotation = Quaternion.Euler(-90f, 0f, 180f);
                break;
        }

        if (spawnIndex < spawnList.Count - 1)
            ++spawnIndex;
        else
            spawnIndex = 0;
    }

    public void SpawnEnemy()
    {
        //safety checks
        if (GameManager.instance.playerScript.isDead) return;
        if (!isSpawningEnemies) return;
        if (currentEC >= AEC) return;

        Vector3 spawnLocation = FindAndValidateSpawnLocation();


        //retry in one second if theres no good spots right now
        if (spawnLocation == Vector3.zero)
        {
            Invoke("SpawnEnemy", 1f);
            return;
        }

        //spawn the indicator which will telegraph the enemy spawn
        SpawnIndicator sp = Instantiate(spawnIndicator, spawnLocation + new Vector3(0f, 4f, 0f), Quaternion.identity).GetComponent<SpawnIndicator>();
        sp.enemyToSpawn = spawnList[spawnIndex];

        //what the spawn indicator will actually show
        switch (sp.enemyToSpawn.name)
        {
            case "Monolith_Enemy":
                sp.enemyMesh = enemyMeshList[0];
                sp.SetMesh(enemyMeshList[0]);
                sp.modelFrame.transform.localScale = new Vector3(220f, 100f, 450f);
                sp.modelFrame.transform.rotation = Quaternion.Euler(-90f, UnityEngine.Random.Range(0, 360), 0);
                break;
            case "Snake":
                //sp.enemyMesh = enemyMeshList[0];
                //sp.SetMesh(enemyMeshList[0]);
                //sp.modelFrame.transform.localScale = new Vector3(220f, 100f, 450f);
                //sp.modelFrame.transform.rotation = Quaternion.Euler(-90f, UnityEngine.Random.Range(0, 360), 0);
                sp.transform.position -= new Vector3(0f, -0.5f, 0f);
                break;
        }

        if (spawnIndex < spawnList.Count - 1)
            ++spawnIndex;
        else
            spawnIndex = 0;
    }

    public Vector3 RandomizeSpawnLocation()
    {
        return new Vector3(UnityEngine.Random.Range(-mapSize, mapSize), 40f, 
            UnityEngine.Random.Range(-mapSize, mapSize));
    }

    public Vector3 FindAndValidateSpawnLocation()
    {
        bool isValidSpawn = false;
        RaycastHit hit;

        int attemptCount = 0;
        do
        {
            ++attemptCount;
            //sphere cast down from the sky to check what we hit
            if (Physics.SphereCast(RandomizeSpawnLocation(), 3f, -Vector3.up, out hit))
            {
                //if we hit the ground, then there is nothing is that space, so spawn
                if (hit.collider.CompareTag("groundTag") ||
                    hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    isValidSpawn = true;
                    return hit.point + new Vector3(0, 2f, 0);
                }
                //else we miss the platform or hit an enemy, try a different spot
            }
            if (attemptCount > 1000) break;
        }
        while (!isValidSpawn);

        return Vector3.zero;
    }
    #endregion

    public void TriggerStopwatch()
    {
        if(StopwatchTrigger != null)
        {
            StopwatchTrigger();

        }
    }
}
