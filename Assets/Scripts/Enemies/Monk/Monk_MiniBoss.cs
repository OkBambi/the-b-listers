using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Monk_MiniBoss : EnemyBase
{
    [Header("roaming movement")]
    [SerializeField] int faceTargetSpeed;
    [SerializeField] NavMeshAgent agent;
    Vector3 startingPOS;

    [SerializeField] PrimaryColor PrimaryColor;
    [SerializeField] int ColorChangeIndex;
    [SerializeField] private ChainUIMonkBoss chainUI;
    Monk MonkBoss;

    private PrimaryColor[] colorRoutine = { PrimaryColor.RED, PrimaryColor.BLUE, PrimaryColor.YELLOW };
    private int currenColor;

    Color colorOriginal;

    [Header("Casting")]
    [SerializeField] GameObject Wave;
    [SerializeField] float pauseToCastTimer;
    [SerializeField] float waveSize;
    private Vector3 waveSizeOriginal;
    [SerializeField] float waveGrowthSpeed;
    float waveGrowthTimer = 0;

    [SerializeField] float initialDelay;
    [SerializeField] float CastingDelay;


    [SerializeField] float StopTime;
    float roamTimer;


    bool isCasting = false;

    private void Awake()
    {
        //OnAECAwake();
        RandomizeColor();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        if (LevelModifierManager.instance.lessHealth)
        {
            int NewHP = hp;
            NewHP = hp / 2;
            hp = NewHP;
        }

        agent = GetComponent<NavMeshAgent>();

        chainUI = FindFirstObjectByType<ChainUIMonkBoss>();
        ColorSelection(setColor);

        colorOriginal = model.material.color;

        if (Wave != null)
        {
            waveSizeOriginal = Wave.transform.localScale;
        }
        name = "Maestro";
        StartCoroutine(ChangeColors());
        //For boss bar
        bossHPOrig = hp;
        bossName.text = gameObject.name;
        bossHPBar.gameObject.transform.parent.gameObject.SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {
        //face targrt
        Vector3 direction = GameManager.instance.player.transform.position - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.001)
        {
            Quaternion rot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
        }
        if (!isCasting)
        {
            StartCoroutine(Cast());
        }
    }
    //casting
    IEnumerator Cast()
    {
        isCasting = true;

        yield return new WaitForSeconds(CastingDelay);
        for (int i = 0; i < 2; i++)
        {
            AudioManager.instance.Play("Monk_Blinker");
            model.material.color = Color.white;
            yield return new WaitForSeconds(0.05f);
            ColorSelection(setColor);
            yield return new WaitForSeconds(0.05f);
        }
        AudioManager.instance.Play("Monk_Cast");

        yield return new WaitForSeconds(0.20f);
        Wave.SetActive(true);
        waveGrowthTimer = 0;
        while (waveGrowthTimer < waveGrowthSpeed)
        {
            float growthRate = waveGrowthTimer / waveGrowthSpeed;
            Wave.transform.localScale = Vector3.Lerp(Vector3.zero, waveSizeOriginal * waveSize, growthRate);
            waveGrowthTimer += Time.deltaTime;
            yield return null;
        }
        Wave.transform.localScale = waveSizeOriginal;

        isCasting = false;
    }
    //casting

    IEnumerator ChangeColors()
    {
        while (true)
        {
            yield return new WaitForSeconds(initialDelay);
            // change color

            setColor = colorRoutine[currenColor];
            currenColor = (currenColor + 1) % colorRoutine.Length;
            ColorSelection(setColor);

            colorOriginal = model.material.color;

        }
    }

    public override void DeathCheck()
    {
        if (hp <= 0)
        {
            base.DeathCheck();
            bossHPBar.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    public override void takeDamage(PrimaryColor hitColor, int amount)
    {
        if (hitColor == setColor || hitColor == PrimaryColor.OMNI || setColor == PrimaryColor.OMNI)
        {
            hp -= amount;
            if (isAlive)
                DeathCheck();

            StartCoroutine(ShakePos(0.2f, 0.5f));
            StartCoroutine(ShakeSize(0.2f, 0.1f));

            spawnHitColorParticles();
            updateBossHPBar();
        }
    }


}



