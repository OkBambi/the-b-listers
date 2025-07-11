using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartDagger : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Renderer model;

    public bool DaggerGot= false;

    private void Start()
    {
        player = GameManager.instance.playerScript;
    }

    private void Update()
    {

        switch (player.GetPlayerColor())
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
        DaggerGot = true;
        other.GetComponentInParent<Player>().canAction = true;
        other.GetComponentInParent<Player>().canColor = true;
        GameManager.instance.timer.isCounting = true;
        EnemyManager.instance.SpawnFirstEnemy();
        GameManager.instance.PlayLevelMusic();
        Destroy(gameObject);

    }
}
