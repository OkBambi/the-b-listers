using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Monk_MiniBoss : EnemyBase
{
  float waveGrowthTimer = 0;


    [Header("roaming movement")]
    [SerializeField] int faceTargetSpeed;

    float CastTimer;
    [SerializeField] float CastElapse;

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

        colorOriginal = model.material.color;
      
        name = "Monk Elite";
        StartCoroutine(ChangeColors());
    }

    // Update is called once per frame
    void Update()
    {

        FaceTarget();
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


