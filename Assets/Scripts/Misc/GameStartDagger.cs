using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

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
        Scene scene = SceneManager.GetActiveScene();
        other.GetComponentInParent<Player>().canAction = true;
        other.GetComponentInParent<Player>().canColor = true;
        GameManager.instance.timer.isCounting = true;
        AudioManager.instance.Play("GameMusic");
        EnemyManager.instance.SpawnFirstEnemy();
        switch (scene.name)
        {
            case "Level_1":

                AudioManager.instance.Play("Level_1");
                break;

            case "Level_2":
                AudioManager.instance.Play("Level_2");
                break;

            case "Level_3":
                AudioManager.instance.Play("Level_3");
                break;

            case "Level_4":
                AudioManager.instance.Play("Level_4");
                break;

            case "Bonus_Level":
                AudioManager.instance.Play("Bonus_Level");
                break;

            case "Boss_Level":
                AudioManager.instance.Play("Boss_Level");
                break;
        }
        Destroy(gameObject);

    }
}
