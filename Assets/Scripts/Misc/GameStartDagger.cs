using System.Collections;
using UnityEngine;

public class GameStartDagger : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Renderer model;
    public bool mod_SchmovesOnly = false;
    public bool mod_DaggersOnly = false;

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
        if (mod_SchmovesOnly)
            other.GetComponentInParent<Player>().canSchmove = true;

        else
        {
            other.GetComponentInParent<Player>().canAction = true;
            other.GetComponentInParent<Player>().canSchmove = true;
        }


            other.GetComponentInParent<Player>().canColor = true;
        GameManager.instance.timer.isCounting = true;
        AudioManager.instance.Play("GameMusic");
        EnemyManager.instance.SpawnFirstEnemy();
        Destroy(gameObject);
        
    }
}
