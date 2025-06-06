using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public static ComboManager instance;

    [SerializeField] float BASE_COMBO_DECAY = 20;
    [Space]
    [SerializeField] float totalScore;
    [SerializeField] float currentComboScore;
    [SerializeField] ComboGrade comboGrade;
    [SerializeField] float comboHoldTimer;
    [SerializeField] float currentMult;

    static List<int> comboFloors = new List<int>() //score required for each grade
    {
        0,      //none
        200,    //D
        300,    //C
        400,    //B
        500,    //A
        700,    //S
        850,    //SS
        1000,   //SSS
        1500    //P
    };

    static List<float> comboMults = new List<float>() //used for both score mult and decay mult
    {
        0f,    //none
        1f,    //D
        1.25f, //C
        1.5f,  //B
        2f,    //A
        3f,    //S
        4f,    //SS
        6f,    //SSS
        8f     //P
    };

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        CheckGrade();

        if (currentComboScore > 0)
        {
            float mult = comboMults[(int)comboGrade];

            if (mult > 0)
            {
                float decayAmount = BASE_COMBO_DECAY * mult * Time.deltaTime;
                currentComboScore = Mathf.Clamp(currentComboScore, 0, currentComboScore);
            }
        }
    }

    void CheckGrade()
    {
        for (int floor = 0; floor < comboFloors.Count(); floor++) //something something Guilty Gear Strive
        {
            if(currentComboScore >= comboFloors[floor])
            {
                comboGrade = (ComboGrade)floor;
            }
        }
    }

    public void AddScore(float amount)
    {
        currentComboScore += amount * comboMults[(int)comboGrade];
        currentComboScore = Mathf.Clamp(currentComboScore, 0, 1500);
        totalScore += amount * comboMults[(int)comboGrade];
    }
}
public enum ComboGrade
{
    None = 0,
    D = 1,      //DEADLY
    C = 2,      //CHROMATIC
    B = 3,      //BLOODTHIRSTY
    A = 4,      //ANNIHILATE
    S = 5,      //SPECTRAL
    SS = 6,     //SSUPREME
    SSS = 7,    //SSSADISTIC
    P = 8       //PRIMARY
}
