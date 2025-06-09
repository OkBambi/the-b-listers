using System.Collections;
using UnityEngine;

public class GameStartDagger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponentInParent<Player>().canAction = true;
        other.GetComponentInChildren<Timer>().isCounting = true;
        EnemyManager.instance.SpawnFirstEnemy();
        Destroy(gameObject);
    }
}
