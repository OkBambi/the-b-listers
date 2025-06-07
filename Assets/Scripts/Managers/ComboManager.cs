using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
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

    bool popTime;
    float popTimer = 0f;
    float popDuration = 0.05f;
    bool isPopping = false;

    ComboGrade previousGrade;

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

    //THANK YOU cjddmut
    public static float EaseInQuint(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value * value * value + start;
    }

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

        if (popTime)
        {
            //we just want to call popTime once, so immediately turn it to false
            popTime = false;
            isPopping = true;
            popTimer = 0f;
        }

        //roblox has spoiled me with EasingType.Quint, now i must google how to do so on my own
        //nvm github to the rescue (but yes i understand how Quint actually works, its just t^5)
        if (isPopping)
        {
            //smth smth shootTimer
            popTimer += Time.deltaTime;

            //first half: go up
            if (popTimer <= popDuration)
            {
                float t = popTimer / popDuration;
                float scale = EaseInQuint(1f, 1.1f, t);
                comboGradeUGUI.rectTransform.localScale = new Vector3(scale, scale, scale);
                comboMultUGUI.rectTransform.localScale = new Vector3(scale, scale, scale);
            }
            //go back down
            else if (popTimer <= popDuration * 2)
            {
                float t = (popTimer - popDuration) / popDuration;
                float scale = EaseInQuint(1.1f, 1f, t);
                comboGradeUGUI.rectTransform.localScale = new Vector3(scale, scale, scale);
                comboMultUGUI.rectTransform.localScale = new Vector3(scale, scale, scale);
            }
            //WE'RE FREEEE
            else
            {
                isPopping = false;
                comboGradeUGUI.rectTransform.localScale = Vector3.one;
            }
        }

        //TESTING STUFF
        if (Input.GetKeyDown(KeyCode.G))
        {
            print("Adding score");
            AddScore(100);
        }
    }

    void CheckGrade()
    {
        ComboGrade newGrade = comboGrade;

        for (int floor = 0; floor < comboFloors.Count(); floor++) //something something Guilty Gear Strive
        {
            if(currentComboScore >= comboFloors[floor])
            {
                newGrade = (ComboGrade)floor;
            }
        }

        if (newGrade != comboGrade)
        {
            previousGrade = comboGrade;
            comboGrade = newGrade;

            //pop the size of the grade text if its new for flair
            popTime = true;
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
        comboMultUGUI.text = (comboMults[(int)comboGrade].ToString("0.00") + "x");
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
