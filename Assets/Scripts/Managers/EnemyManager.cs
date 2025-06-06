using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
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

        Instantiate(spawnList[spawnIndex].transform, spawnLocation, Quaternion.identity);

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
                    return hit.point;
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
