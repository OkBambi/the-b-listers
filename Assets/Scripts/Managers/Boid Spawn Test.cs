using UnityEngine;

public class BoidSpawnTest : MonoBehaviour
{
    public GameObject boids;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Instantiate(boids);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            EnemyManager.instance.SpawnFirstEnemy();
        }
    }
}
