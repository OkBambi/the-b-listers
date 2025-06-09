using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [Space]
    //this is the thing that shows where an enemy will spawn in a few moments

    //Enemy model is spawned, and they are visible. But they are pure WHITE, with a glossy material (make a temp one, ill shader the hell out of it later)
    //Enemy model currently lacks any AI, is static, and you can move through it(no collider)
    //Other enemies will still try to avoid it though, so boids, monks, etc
    //Enemy model will begin with blink, flashing 3 times before

    [SerializeField] GameObject spawnIndicator;

    [Space]
    //so we have a list of enemies that spawn in a repeating order
    [SerializeField] List<GameObject> spawnList;

    //this will track what enemy to spawn
    [SerializeField] int spawnIndex;

    [SerializeField] bool isSpawningEnemies = true;
    [Space]

    //and we have a spawn limit on the enemies
    [SerializeField] int AEC;
    [SerializeField] int currentEC;

    [Space]

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
        SpawnEnemy();
        if (ticker >= tickerLimit)
        {
            ResetTicker();
            IncrementAEC();
            SpawnEnemy();
        }
    }

    public void OnAECAwake()
    {
        IncrementCurrentEC();
    }
    #endregion


    #region EnemySpawning
    //i still need to add the spawning buffer/animation

    //call this to actually start the enemies commin (like starting the game or picking up the dagger
    public void SpawnFirstEnemy()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        //safety checks
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
        SpawnIndicator sp = Instantiate(spawnIndicator, spawnLocation, Quaternion.identity).GetComponent<SpawnIndicator>();
        sp.enemyToSpawn = spawnList[spawnIndex];
        sp.enemyMesh = spawnList[spawnIndex].GetComponentInChildren<MeshFilter>().sharedMesh;
        sp.SetMesh(spawnList[spawnIndex].GetComponentInChildren<MeshFilter>().sharedMesh);

        //adjust visuals to make it look good for specific enemies
        if (spawnList[spawnIndex].name == "AngryBoid")
        {
            sp.modelFrame.transform.localScale = new Vector3(50f, 50f, 50f);
            sp.modelFrame.transform.rotation = Quaternion.Euler(-60f, 0, 45);
            
        }


        if (spawnIndex < spawnList.Count)
            ++spawnIndex;
        else
            spawnIndex = 0;
    }

    public Vector3 RandomizeSpawnLocation()
    {
        return new Vector3(Random.Range(-100f, 100f), 40f, Random.Range(-100f, 100f));
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


}
