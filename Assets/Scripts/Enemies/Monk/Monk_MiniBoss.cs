using System.Collections;
using UnityEngine;

public class Monk_MiniBoss : Monk
{
    [SerializeField] PrimaryColor PrimaryColor;
    [SerializeField] int ColorChangeIdex;

    private PrimaryColor[] colorRoutine = { PrimaryColor.RED, PrimaryColor.BLUE, PrimaryColor.YELLOW };
    private int currenColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ChangeColors());
    }

    IEnumerator ChangeColors()
    {
        while (true)
        {
            yield return new WaitForSeconds(7);

            PrimaryColor newColor =  colorRoutine[ColorChangeIdex];
            setColor = newColor;
            ColorSelection(newColor);
            
            ColorChangeIdex++;
            if(ColorChangeIdex >= colorRoutine.Length)
            {
                ColorChangeIdex = 0;
            }
        }
    }
}
