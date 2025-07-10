using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class LevelModifierManager : MonoBehaviour
{
    //when you make a new modifier, create a LevelModifier Object and then create the function that will apply the changes.
    public static LevelModifierManager instance;

    GameObject modifierCardSelectionUI;
    [SerializeField] string functionName;
    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Button button3;


    [Inspectable] List<LevelModifier> modifiers;

    [SerializeField] DifficultyObject difficulty;

    public bool schmoveOnly;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        switch (difficulty.difficulty)
        {
            case DifficultyObject.DifficultyType.Normal:
                switch (sceneName)
                {
                    case "Level_2":
                    case "Level_3":
                    case "Level4":
                        ShowCardUI();
                        break;
                }
                break;
            case DifficultyObject.DifficultyType.Easy:
                switch (sceneName)
                {
                    case "Level_2":
                    case "Level_3":
                    case "Level4":
                        ShowCardUI();
                        break;
                }
                break;
            case DifficultyObject.DifficultyType.Hard:
                switch (sceneName)
                {
                    case "Level_1":
                    case "Level_2":
                    case "Level_3":
                    case "Level4":
                    case "BossDev":
                        ShowCardUI();
                        break;
                }
                break;
        }



    }

    

    public void ClearCurrentModifiers()
    {
        modifiers.Clear();
    }

    #region Cards
    public void RandomizeCards()
    {
        #region Error Checking
        switch (difficulty.difficulty)
        {
            case DifficultyObject.DifficultyType.Normal:
                if (difficulty.normalModifiers.Count < 3)
                {
                    Debug.LogWarning("Unable to randomize Normal Modifiers: Not enough modifiers");
                    return;
                }
                break;
            case DifficultyObject.DifficultyType.Easy:
                if (difficulty.easyModifiers.Count < 3)
                {
                    Debug.LogWarning("Unable to randomize Easy Modifiers: Not enough modifiers");
                    return;
                }
                break;
            case DifficultyObject.DifficultyType.Hard:
                if (difficulty.hardModifiers.Count < 3)
                {
                    Debug.LogWarning("Unable to randomize Hard Modifiers: Not enough modifiers");
                    return;
                }
                break;
        }
        #endregion

        while (modifiers.Count < 3)
        {
            int rand = 0;
            switch (difficulty.difficulty)
            {
                case DifficultyObject.DifficultyType.Normal:
                    while (true)
                    {
                        rand = Random.Range(0, difficulty.normalModifiers.Count);
                        LevelModifier newModifier = difficulty.normalModifiers[rand];
                        if (!modifiers.Contains(newModifier))
                        {
                            modifiers.Add(newModifier);
                            break;
                        }
                    }
                    break;
                case DifficultyObject.DifficultyType.Easy:
                    while (true)
                    {
                        rand = Random.Range(0, difficulty.easyModifiers.Count);
                        LevelModifier newModifier = difficulty.easyModifiers[rand];
                        if (!modifiers.Contains(newModifier))
                        {
                            modifiers.Add(newModifier);
                            break;
                        }
                    }
                    break;
                case DifficultyObject.DifficultyType.Hard:
                    while (true)
                    {
                        rand = Random.Range(0, difficulty.hardModifiers.Count);
                        LevelModifier newModifier = difficulty.hardModifiers[rand];
                        if (!modifiers.Contains(newModifier))
                        {
                            modifiers.Add(newModifier);
                            break;
                        }
                    }
                    break;
            }
        }
    }
    public void InitializeCards()
    {
        //Card1.icon = modifiers[0].icon;
        //Card2.icon = modifiers[0].icon;
        //Card3.icon = modifiers[0].icon;
        //Card1.description = modifiers[0].description;
        //Card2.description = modifiers[0].description;
        //Card3.description = modifiers[0].description;
    }

    public void ShowCardUI()
    {
        modifierCardSelectionUI.SetActive(true);

        //this part will need to be adjusted depending on what the card ui looks like

        //button1.onClick.AddListener(delegate { SetFunctionToCall(button1.GetComponentInChildren<TextMeshProUGUI>()); });
        //button2.onClick.AddListener(delegate { SetFunctionToCall(button2.GetComponentInChildren<TextMeshProUGUI>()); });
        //button3.onClick.AddListener(delegate { SetFunctionToCall(button3.GetComponentInChildren<TextMeshProUGUI>()); });
    }
    #endregion

    #region Function Modularity
    public void SetFunctionToCall(string newFunctionName)
    {
        functionName = newFunctionName;
    }

    public void SetFunctionToCall(TextMeshProUGUI newFunctionName)
    {
        functionName = newFunctionName.text;
    }

    public void CallSelectedFunction()
    {
        if (functionName != null)
        {
            Invoke(functionName, 0f);
        }
    }
    #endregion


    //place modifier functions below of organization reasons
    #region Modifier Methods
    public void SchmovesOnly()
    {
        schmoveOnly = true;
        FindFirstObjectByType<GameStartDagger>().mod_SchmoveOnly = true;
        if (SceneManager.GetActiveScene().name == "Level_1")
            ComboManager.instance.AddScoreNoMult(1000f);
    }
    #endregion
}
