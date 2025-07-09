using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Monk_MiniBoss : EnemyBase
{
    [Header("Casting")]
    [SerializeField] GameObject Wave;
    [SerializeField] float pauseToCastTimer;
    [SerializeField] float waveSize;
    private Vector3 waveSizeOriginal;
    [SerializeField] float waveGrowthSpeed;
    float waveGrowthTimer = 0;


    [Header("roaming movement")]
    [SerializeField] int faceTargetSpeed;
    [SerializeField] NavMeshAgent agent;

    float CastTimer;
    [SerializeField] float CastElapse;

    [Header("Seperation")]
    [SerializeField] float minSeperationDistance;
    [SerializeField] GameObject SeperationDome;

    [SerializeField] PrimaryColor PrimaryColor;
    [SerializeField] int ColorChangeIndex;

    private PrimaryColor[] colorRoutine = { PrimaryColor.RED, PrimaryColor.BLUE, PrimaryColor.YELLOW };
    private int currenColor;


    //detectors 
    bool isCasting = false;
    Color colorOriginal;


    private void Awake()
    {
        OnAECAwake();
        RandomizeColor();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        ColorSelection(setColor);
        agent = GetComponent<NavMeshAgent>();
        colorOriginal = model.material.color;
        if (Wave != null)
        {
            waveSizeOriginal = Wave.transform.localScale;
        }
        name = "Monk Elite";
        StartCoroutine(ChangeColors());
    }

    // Update is called once per frame
    void Update()
    {

        if (CastTimer <= CastElapse && !isCasting)
        {
            CastTimer += Time.deltaTime;

            StartCoroutine(Cast());

        }
        FaceTarget();
    }

    //movement
    public void PauseForAMoment()
    {
        agent.isStopped = true;
        StartCoroutine(isroaming(pauseToCastTimer));
    }
    IEnumerator isroaming(float pauseDuration)
    {
        yield return new WaitForSeconds(pauseDuration);
        agent.isStopped = false;
    }

    void FaceTarget()
    {
        Vector3 direction = GameManager.instance.player.transform.position - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.001)
        {
            Quaternion rot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
        }
    }

    //casting
    IEnumerator Cast()
    {
        isCasting = true;
        PauseForAMoment();

        for (int i = 0; i < 2; i++)
        {
            AudioManager.instance.Play("Monk_Blinker");
            model.material.color = Color.white;
            yield return new WaitForSeconds(0.05f);
            model.material.color = colorOriginal;
            yield return new WaitForSeconds(0.05f);
        }
        AudioManager.instance.Play("Monk_Cast");

        yield return new WaitForSeconds(0.20f);
        Wave.SetActive(true);
        Wave.transform.localScale = Vector3.zero;
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
    IEnumerator ChangeColors()
    {
        while (true)
        {
            yield return new WaitForSeconds(7);

            PrimaryColor newColor = colorRoutine[ColorChangeIndex];
            setColor = newColor;
            ColorSelection(newColor);

            ColorChangeIndex = (ColorChangeIndex + 1) % colorRoutine.Length;
        }
    }
}


