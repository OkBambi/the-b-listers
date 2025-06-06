using UnityEngine;

public class BoidSpawnTest : MonoBehaviour
{
    public GameObject boids;
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
