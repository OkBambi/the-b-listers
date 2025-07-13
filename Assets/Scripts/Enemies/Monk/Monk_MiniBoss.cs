using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Monk_MiniBoss : MonoBehaviour
{
    float waveGrowthTimer = 0;


    [Header("roaming movement")]
    [SerializeField] int faceTargetSpeed;


    [SerializeField] PrimaryColor PrimaryColor;
    [SerializeField] int ColorChangeIndex;
    Monk MonkBoss;

    private PrimaryColor[] colorRoutine = { PrimaryColor.RED, PrimaryColor.BLUE, PrimaryColor.YELLOW };
    private int currenColor;

    Color colorOriginal;

    void Start()
    {
        MonkBoss = GetComponent<Monk>();
        colorOriginal = MonkBoss.model.material.color;

        StartCoroutine(ChangeColors());
    }

    // Update is called once per frame
    void Update()
    {

    }


    //casting

    IEnumerator ChangeColors()
    {
        while (true)
        {
            yield return new WaitForSeconds(7);

            PrimaryColor newColor = colorRoutine[ColorChangeIndex];
            MonkBoss.setColor = newColor;
            MonkBoss.ColorSelection(newColor);

            ColorChangeIndex = (ColorChangeIndex + 1) % colorRoutine.Length;
        }
    }
}


