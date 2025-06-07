using UnityEngine;

public class BoidSpawnTest : MonoBehaviour
{
    public GameObject boids;
    public EnemyBase target;
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
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            target.takeDamage(PrimaryColor.RED, 2);
        }

    }
}
