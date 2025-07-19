using System.Collections;
using UnityEngine;
using static EasingLibrary;

public class StopWatch : EnemyBase
{
    [SerializeField] GameObject ShockWave;

    int counter;
    [SerializeField] int counterLimit = 3;

    public float slamDuration;
    public float endPosition;

    private float startPosition;
    private bool isSlamming;
    private Rigidbody rb;

    Vector3 StartPos;

    private void Awake()
    {
        OnAECAwake();
        startPosition = transform.position.y;
        isSlamming = false;
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        counter = 0;
        ColorSelection(setColor);
        rb = GetComponent<Rigidbody>();
        // Keep gravity off initially
        rb.useGravity = false;
        StartPos = transform.position;

        EnemyManager.instance.StopwatchTrigger += CountDownTimer; // Subscribe to the stopwatch trigger event
        name = "Stop Watch";
        if (LevelModifierManager.instance.lowEnemyCooldowns)
            counterLimit = Mathf.Clamp(Mathf.CeilToInt((float)counterLimit * 0.25f), 1, 100);

        if (LevelModifierManager.instance.smallFastEnemies)
            model.transform.localScale = model.transform.localScale * 0.75f;

        if (LevelModifierManager.instance.lessHealth)
        {
            int NewHP = hp;
            NewHP = hp / 2;
            hp = NewHP;
        }
    }

    void CountDownTimer()
    {
        counter++;
        if(counter >= counterLimit && !isSlamming)
        {
            counter = 0;
            isSlamming = true;
            StartCoroutine(slamer()); // Start the coroutine for slamming
        }
        
    }

    

    IEnumerator slamer() {         float elapsedTime = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(transform.position.x, endPosition, transform.position.z);
        AudioManager.instance.Play("Stopwatch_Indicator");
        while (elapsedTime < slamDuration)
        {
            float t = elapsedTime / slamDuration;
            t = Mathf.Clamp01(t); // Ensure t is between 0 and 1
            float easedT = EaseInBack(0, 1, t); 
            transform.position = Vector3.Lerp(startPos, endPos, easedT);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos; // Ensure we end at the exact position
        isSlamming = false;
        Instantiate(ShockWave, transform.position - new Vector3(0f,1f,0f), Quaternion.identity).GetComponent<stopWatchShockWave>(); // Instantiate shockwave effect
        StartCoroutine(ReturnToStart()); // Return to start position after slamming 
    }

    IEnumerator ReturnToStart()
    {
        float elapsedTime = 0f;
        Vector3 returnToStartPos = transform.position;
        Vector3 returnToEndPos = StartPos;
        while (elapsedTime > slamDuration)
        {
            float t = elapsedTime / slamDuration;
            t = Mathf.Clamp01(t); // Ensure t is between 0 and 1
            float easedT = EaseInBack(0, 1, t); 
            transform.position = Vector3.Lerp(returnToStartPos, returnToEndPos, easedT);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = returnToEndPos; // Ensure we end at the exact position
    }


    public override void DeathCheck()
    {
        if (hp <= 0)
        {
            isAlive = false;
            OnAECDestroy();
            RemoveSelfFromTargetList();
            EnemyManager.instance.StopwatchTrigger -= CountDownTimer; // unSubscribe to the stopwatch trigger event
            Destroy(gameObject);
            return;
        }
    }
}
