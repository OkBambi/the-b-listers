using System.Collections;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Goliath : EnemyBase
{
    /*
     * Make random Vector3 to move to 
     * Move in direction for like.. 1 second or smth
     * Random.insideUnitCircle smth smth
     * yes
     * 
     */
    public enum State
    {
        Roam,
        Diving,
        Swimming,
        Breach,
    }

    [SerializeField] State currentState = State.Roam;
    [Space]
    [Header("Timers")]
    [SerializeField] float topRoamTime;
    [SerializeField] float swimTime; //time under the map
    [SerializeField] float swapTime;
    [Space]
    [Header("Roam Parameters")]
    [SerializeField] float roamSpeed;
    [SerializeField] int roamDistance;
    [SerializeField] float roamStopTimer;
    float roamTime;
    float remainingDistance;
    Vector3 roamPosition;


    [Space]
    [Header("Dive Parameters")]
    [SerializeField] float diveSpeed;
    [SerializeField] float radiusOfDiveLocation;
    [SerializeField] GameObject map;
    [SerializeField] GameObject goliathHitLocation;
    [SerializeField] float mapRadius;
    [Space]
    [Header("Swim Parameters")]
    [SerializeField] float swimSpeed;

    [SerializeField] Transform playerTransform;

    [Header("Breach Parameters")]
    [SerializeField] float breachSpeed;
    [SerializeField] float timeBeforeBreach;

    float stateTimer;
    Vector3 startPos;
    int enemiesSpawned;

    protected override void Start()
    {
        if (LevelModifierManager.instance.lessHealth)
        {
            int NewHP = hp;
            NewHP = hp / 2;
            hp = NewHP;
        }

        goliathHitLocation = GameObject.Find("GoliathHitMarker");
        map = GameObject.Find("Boss_Map");
        bossName = GameObject.Find("Enemy Name").GetComponent<TextMeshProUGUI>();
        bossHPBar = GameObject.Find("EnemyHPBar").GetComponent<Image>();

        ColorSelection(setColor);
        startPos = transform.position;

        PickRoamLocation();

        //for swimming
        playerTransform = GameManager.instance.player.transform;

        //for diving
        goliathHitLocation.transform.localScale = new Vector3(radiusOfDiveLocation, 0.1f, radiusOfDiveLocation);

        StartCoroutine(SwapColors());

        //For boss bar
        bossHPOrig = hp;
        gameObject.name = "Goliath";
        bossName.text = gameObject.name;
        bossHPBar.gameObject.transform.parent.gameObject.SetActive(true);
    }

    void Update()
    {
        StateCheck();
    }

    void StateCheck()
    {
        if (currentState == State.Roam)
        {

            if (remainingDistance < 1f)
            {
                roamTime += Time.deltaTime;
                if (roamTime >= roamStopTimer)
                {
                    Debug.Log("Roam");
                    PickRoamLocation();
                }
            }
            else
            {
                RoamToLocation();
            }


            stateTimer += Time.deltaTime;

            if (stateTimer > topRoamTime)
            {
                currentState = State.Diving;
                AudioManager.instance.Play("Goliath_Dive");
                roamTime = 0;
                Debug.Log("Goliath is diving!");
            }
        }
        else if (currentState == State.Diving)
        {
            stateTimer = 0;
            Diving();
        }
        else if (currentState == State.Swimming)
        {

            Swimming();
        }
        else if (currentState == State.Breach)
        {
            Breach();
        }
    }

    void PickRoamLocation()
    {
        print("Choosing new location");
        roamTime = 0f;
        Vector3 ranPos = Random.insideUnitCircle * roamDistance;
        ranPos.y = 0f;
        ranPos += startPos;
        roamPosition = ranPos;
        remainingDistance = (transform.position - roamPosition).normalized.magnitude;
    }

    void RoamToLocation()
    {
        remainingDistance = (roamPosition - transform.position).normalized.magnitude;

        Vector3 direction = (roamPosition - transform.position).normalized;
        transform.Translate(direction * roamSpeed * Time.deltaTime);
    }
    
    void Diving()
    {
        StartCoroutine(WhaleSplashsNoise());
        if (goliathHitLocation.transform.position == Vector3.zero)
        {
            Vector3 divePos = new Vector3(Random.Range(-1f, 1f) * mapRadius, 0.1f, Random.Range(-1f, 1f) * mapRadius);
            goliathHitLocation.transform.position = divePos;
            goliathHitLocation.GetComponent<Renderer>().enabled = true;
        }

        StartCoroutine(WhaleSplashsNoise());
        stateTimer += Time.deltaTime;

        //go down to indicator and through the map
        Vector3 goToPos = new Vector3(goliathHitLocation.transform.position.x, 
            transform.position.y, goliathHitLocation.transform.position.z);
        Vector3 horizontalDirection = (goToPos - transform.position).normalized;

        transform.Translate(horizontalDirection * roamSpeed * Time.deltaTime);
        
        if (Vector3.Distance(goToPos, transform.position) < 0.1f)
        {
            transform.Translate(Vector3.down * diveSpeed * Time.deltaTime, Space.World);

            if (transform.position.y < -20f)
            {
                currentState = State.Swimming;
                goliathHitLocation.GetComponent<Renderer>().enabled = false;
                goliathHitLocation.transform.position = Vector3.zero;
                EnemyManager.instance.AEC = 5;
                while (enemiesSpawned <= 5)
                {
                    EnemyManager.instance.SpawnEnemy();
                    enemiesSpawned += 1;
                }

            }
        }        
    }

    void Swimming()
    {
        //track the player and move towards it under the ground
        goliathHitLocation.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        goliathHitLocation.GetComponent<Renderer>().enabled = true;

        Vector3 playerRelativePos = new Vector3(playerTransform.position.x, 
            transform.position.y, playerTransform.position.z);

        Vector3 horizontalDirection = (playerRelativePos - transform.position).normalized;

        //jaws music...

        transform.Translate(horizontalDirection * swimSpeed * Time.deltaTime);
        stateTimer += Time.deltaTime;

        EnemyManager.instance.AEC = 0;


        if (stateTimer > swimTime)
        {
            stateTimer = 0;
            currentState = State.Breach;
            enemiesSpawned = 0;
        }
    }

    void Breach()
    {
        //UP.
        StartCoroutine(WhaleSplashsNoise());
        stateTimer += Time.deltaTime;
        if (stateTimer > timeBeforeBreach)
        {
            transform.Translate(Vector3.up * breachSpeed * Time.deltaTime, Space.World);
        }

        if (transform.position.y > 26f)
        {
            stateTimer = 0;
            currentState = State.Roam;
            goliathHitLocation.GetComponent<Renderer>().enabled = false;
            goliathHitLocation.transform.position = Vector3.zero;
        }
    }

    IEnumerator SwapColors()
    {
        while(true)
        {
            yield return new WaitForSeconds(swapTime);
            //change color
            setColor = (PrimaryColor)Random.Range(0, 3);

            ColorSelection(setColor);
        }
    }

    IEnumerator WhaleSplashsNoise()
    {
        yield return new WaitForSeconds(0.1f);
        AudioManager.instance.Play("Goliath_Splash");
    }

    public override void DeathCheck()
    {
        if (hp <= 0)
        {
            GameManager.instance.isWon = true;
            GameManager.instance.OnEndCondition();
            bossHPBar.gameObject.transform.parent.gameObject.SetActive(false);
        }
        base.DeathCheck();
    }

    public override void takeDamage(PrimaryColor hitColor, int amount)
    {
        if (hitColor == setColor || hitColor == PrimaryColor.OMNI || setColor == PrimaryColor.OMNI)
        {
            hp -= amount;
            if (isAlive)
                DeathCheck();

            spawnHitColorParticles();
            updateBossHPBar();
        }
    }
}
