using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    public static ComboManager instance;

    [Header("Stats")]
    [SerializeField] float BASE_COMBO_DECAY = 20;
    [Space]
    [SerializeField] float totalScore;
    [SerializeField] float currentComboScore;
    [SerializeField] ComboGrade comboGrade;
    //[SerializeField] float comboHoldTimer;
    //[SerializeField] float currentMult;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI comboGradeUGUI;
    [SerializeField] TextMeshProUGUI comboMultUGUI;
    [SerializeField] TextMeshProUGUI totalScoreUGUI;
    [SerializeField] Image comboBar;

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
        1f,    //none
        1f,    //D
        1.25f, //C
        1.5f,  //B
        2f,    //A
        3f,    //S
        4f,    //SS
        6f,    //SSS
        8f     //P
    };

    static Dictionary<string, string> comboWords = new Dictionary<string, string>()
    {
        {"", ""},
        {"D", "EADLY"},
        {"C", "HROMATIC"},
        {"B", "LOODTHIRSTY"},
        {"A", "NNIHILATOR"},
        {"S", "PECTRAL"},
        {"SS", "UPREME"},
        {"SSS", "ADISTIC"},
        {"P", "RIMARY"}
    };

    void Start()
    {
        instance = this;

        comboGradeUGUI.text = "";
    }

    void Update()
    {
        CheckGrade();
        UIShenanigans();

        if (currentComboScore > 0)
        {
            float decayAmount = BASE_COMBO_DECAY * comboMults[(int)comboGrade] * Time.deltaTime;
            currentComboScore = Mathf.Clamp(currentComboScore - decayAmount, 0, currentComboScore);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            print("Adding score");
            AddScore(100);
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
        currentComboScore = Mathf.Clamp(currentComboScore, 0, 2500);
        totalScore += amount * comboMults[(int)comboGrade];
    }

    void UIShenanigans()
    {
        //combo grade. we need to separate the ComboGrade letter from the rest of the word
        if(comboGrade > 0)
        {
            string start = comboWords.ElementAt((int)comboGrade).Key;
            string rest = comboWords.ElementAt((int)comboGrade).Value;

            //lets fancy this up
            comboGradeUGUI.text = "<size=120><color=red>"+start+
                "</size></color><size=90><color=black>"+rest+"</size></color>";
        }

        totalScoreUGUI.text = totalScore.ToString();
        comboMultUGUI.text = (comboMults[(int)comboGrade].ToString() + "x");
    }
}
public enum ComboGrade
{
    None = 0,
    D = 1,      //DEADLY
    C = 2,      //CHROMATIC
    B = 3,      //BLOODTHIRSTY
    A = 4,      //ANNIHILATOR
    S = 5,      //SPECTRAL
    SS = 6,     //SSUPREME
    SSS = 7,    //SSSADISTIC
    P = 8       //PRIMARY
}
