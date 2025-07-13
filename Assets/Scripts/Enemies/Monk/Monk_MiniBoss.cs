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
    [SerializeField] private LockColorChange chainUI;
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
    [SerializeField] float middleDelay;
    [SerializeField] float ResetTimer;

 [SerializeField] float StopTime;
    float roamTimer;


    bool isCasting = false;

    private void Awake()
    {
        OnAECAwake();
        RandomizeColor();
        agent = GetComponent<NavMeshAgent>();
        MonkBoss = GetComponent<Monk>();
        chainUI = FindFirstObjectByType<LockColorChange>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        startingPOS = transform.position;
        ColorSelection(setColor);

        colorOriginal = MonkBoss.model.material.color;
        if (Wave != null)
        {
            waveSizeOriginal = Wave.transform.localScale;
        }
        name = "Monk_Mini_Boss";
        StartCoroutine(ChangeColors());

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

    }
    //casting
    IEnumerator Cast(PrimaryColor waveColor)
    {
        isCasting = true;

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
    //casting

    IEnumerator ChangeColors()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            yield return StartCoroutine(Cast(colorRoutine[ColorChangeIndex]));

            yield return new WaitForSeconds(middleDelay);

            ColorChangeIndex = (ColorChangeIndex + 1) % colorRoutine.Length;
            PrimaryColor newColor = colorRoutine[ColorChangeIndex];
            MonkBoss.setColor = newColor;
            MonkBoss.ColorSelection(newColor);
            chainUI.SwapChainColor(newColor);
            yield return StartCoroutine(Cast(newColor));

            yield return new WaitForSeconds(ResetTimer);
        }
    }
}



