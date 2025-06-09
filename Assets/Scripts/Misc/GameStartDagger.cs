using System.Collections;
using UnityEngine;

public class GameStartDagger : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Renderer model;

    private void Start()
    {
        player = GameManager.instance.playerScript;
    }

    private void Update()
    {

        switch(player.GetPlayerColor())
        {
            case PrimaryColor.RED:
                model.material.color = Color.red;
                break;
            case PrimaryColor.YELLOW:
                model.material.color = Color.yellow;
                break;
            case PrimaryColor.BLUE:
                model.material.color = Color.blue;
                break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponentInParent<Player>().canAction = true;
        other.GetComponentInParent<Player>().canColor = true;
        other.GetComponentInChildren<Timer>().isCounting = true;
        EnemyManager.instance.SpawnFirstEnemy();
        Destroy(gameObject);
    }
}
