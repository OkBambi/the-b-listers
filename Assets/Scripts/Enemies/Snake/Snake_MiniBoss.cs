using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Snake_MiniBoss : Snake
{
    //so for the snake miniboss, it needs to further challenge the player's precision so i reckon:
    //normal snake, but when you kill the head it breaks into 3 orbs.
    //if the player doesnt break the 3 orbs in time, it gets re absorbed by the snake to regrow the head

    [SerializeField] GameObject bossHpBg;
    [SerializeField] Image bossHpBar;
    [SerializeField] TextMeshProUGUI bossHpName;
    public float offsetRange = 3f;
    [SerializeField] Vector3 offset;

    float distanceToPlayer;

    private void Awake()
    {
        EnemyManager.instance.AEC = 0;
        EnemyManager.instance.ticker = 0;
        EnemyManager.instance.tickerLimit = 100;
        for (int headIndex = 0; headIndex < theBois.Count; headIndex++)
        {
            int rand = Random.Range(0, colourIndexes.Count - 1);
            theBois[headIndex].setColor = (PrimaryColor)colourIndexes[rand];
            colourIndexes.Remove(colourIndexes[rand]);
        }

        Snakeagent = GetComponent<NavMeshAgent>();
        Snakeagent.speed = movementSpeed;
        Snakeagent.baseOffset = startHeight;
    }

    protected override void Start()
    {
        player = GameManager.instance.player.transform;
        bossHpBar = FindFirstObjectByType<BossHpBar_Marker>(FindObjectsInactive.Include).GetComponent<Image>();
        bossHpBg = bossHpBar.transform.parent.gameObject;
        bossHpName = bossHpBg.GetComponentInChildren<TextMeshProUGUI>();

        bossHpName.text = "Naga";
        bossHpBar.fillAmount = 0f;
        StartCoroutine(FillHpBar());
        bossHpBg.SetActive(true);

        FindFirstObjectByType<Timer>().textForTimer.gameObject.SetActive(false);

        name = "Naga";

        if (LevelModifierManager.instance.smallFastEnemies)
        {
            movementSpeed = movementSpeed * 2f;
            Snakeagent.speed = movementSpeed;
        }

        if (LevelModifierManager.instance.lessHealth)
        {
            int NewHP = hp;
            NewHP = hp / 2;
            hp = NewHP;
        }
        AudioManager.instance.Play("Snake");
        StartCoroutine(RandomizeDestinationOffset());
    }

    private void Update()
    {
        //the boss will have infinite vision, meaning it will always try to chase the player
        //MOVEMENT
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= offsetRange) 
            Snakeagent.destination = player.position;
        else
            Snakeagent.destination = player.position + offset;
    }

    IEnumerator RandomizeDestinationOffset()
    {
        while (true)
        {
            offset = new Vector3(Random.Range(0f, offsetRange), 0, Random.Range(0f, offsetRange));
            
            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
        
    }

    public override void DeathCheck()
    {
        //Debug.Log(hp);
        bossHpBar.fillAmount = hp / 3f;
        if (hp <= 0)
        {
            isAlive = false;
            RemoveSelfFromTargetList();
            AudioManager.instance.Play("Enemy_Death");
            if (SceneManager.GetActiveScene().name != "Level_Showcase")
                GameManager.instance.OnEndCondition();
            OnAECDestroy();
            Destroy(gameObject);
            return;
        }
    }

    IEnumerator FillHpBar()
    {
        float timer = 0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime;
            bossHpBar.fillAmount = timer / 1f;
            yield return new WaitForFixedUpdate();
        }
        bossHpBar.fillAmount = 1f;
    }
}
