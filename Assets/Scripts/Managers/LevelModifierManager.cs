using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelModifierManager : MonoBehaviour
{
    //when you make a new modifier, create a LevelModifier Object and then create the function that will apply the changes.
    public static LevelModifierManager instance;

    [SerializeField] GameObject modifierCardSelectionUI;
    [SerializeField] string functionName;
    [SerializeField] List<TextMeshProUGUI> card1Texts;
    [SerializeField] List<TextMeshProUGUI> card2Texts;
    [SerializeField] List<TextMeshProUGUI> card3Texts;

    [SerializeField] Image card1Icon;
    [SerializeField] Image card2Icon;
    [SerializeField] Image card3Icon;

    [Inspectable] public List<LevelModifier> modifiers;

    [SerializeField] DifficultyObject difficulty;

    //hard
    public bool schmovesOnly = false;
    public bool daggersOnly = false;
    public bool doubleEnemies = false;
    public bool lowEnemyCooldowns = false;
    public bool smallFastEnemies = false;

    //normal
    public bool boogieWoogie = false;
    public bool shotgunOnly = false;
    public bool rapidFireOnly = false;

    //easy
    public bool largerStage = false;
    public bool lessHealth = false;
    public bool savingGrace = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Invoke("CardLogic", 0.01f);
    }

    public void CardLogic()
    {
        RandomizeCards();

        InitializeCards();


        string sceneName = SceneManager.GetActiveScene().name;
        switch (difficulty.difficulty)
        {
            case DifficultyObject.DifficultyType.Normal:
                switch (sceneName)
                {
                    case "Level_2":
                    case "Level_3":
                    case "Level_4":
                        ShowCardUI();
                        break;
                }
                break;
            case DifficultyObject.DifficultyType.Easy:
                switch (sceneName)
                {
                    case "Level_2":
                    case "Level_3":
                    case "Level_4":
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
                    case "Level_4":
                    case "Level_Boss":
                        ShowCardUI();
                        break;
                }
                break;
        }
        //DoubleEnemies();
        //LowEnemyCooldowns();
        //SmallFastEnemies();
        //ShowCardUI();
    }

    

    public void ClearCurrentModifiers()
    {
        modifiers.Clear();

        schmovesOnly = false;
        daggersOnly = false;
        doubleEnemies = false;
        lowEnemyCooldowns = false;
        smallFastEnemies = false;

        //
        boogieWoogie = false;

        shotgunOnly = false;
        rapidFireOnly = false;

        largerStage = false;
        lessHealth = false;
        savingGrace = false;

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
        card1Texts[0].text = modifiers[0].modifierName;
        card1Texts[1].text = modifiers[0].functionName;
        card1Texts[2].text = modifiers[0].description;

        card2Texts[0].text = modifiers[1].modifierName;
        card2Texts[1].text = modifiers[1].functionName;
        card2Texts[2].text = modifiers[1].description;

        card3Texts[0].text = modifiers[2].modifierName;
        card3Texts[1].text = modifiers[2].functionName;
        card3Texts[2].text = modifiers[2].description;

        card1Icon.sprite = modifiers[0].modifierIcon;
        card2Icon.sprite = modifiers[1].modifierIcon;
        card3Icon.sprite = modifiers[2].modifierIcon;
        //Card1.icon = modifiers[0].icon;
        //Card2.icon = modifiers[0].icon;
        //Card3.icon = modifiers[0].icon;
        //Card1.description = modifiers[0].description;
        //Card2.description = modifiers[0].description;
        //Card3.description = modifiers[0].description;
    }

    public void ShowCardUI()
    {
        GameManager.instance.statePause();
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameManager.instance.MenuActive = modifierCardSelectionUI;
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
        if (functionName != "")
        {
            Invoke(functionName, 0f);
            modifierCardSelectionUI.SetActive(false);
            GameManager.instance.stateUnPause();
        }
    }
    #endregion


    //place modifier functions below of organization reasons
    #region Modifier Methods
    #region Hard
    public void SchmovesOnly()
    {
        schmovesOnly = true;
        FindFirstObjectByType<GameStartDagger>().mod_SchmovesOnly = true;
        ComboManager.instance.AddScoreNoMult(1000f);
    }
    public void DaggersOnly()
    {
        daggersOnly = true;
    }

    public void DoubleEnemies()
    {
        doubleEnemies = true;
    }

    public void LowEnemyCooldowns()
    {
        lowEnemyCooldowns = true;
        //this method implementation is incomplete, it depends on the stopwatch being reworked to be on a time basis
        //I will still implement an adjusted implementation based on the trigger tho
    }

    public void SmallFastEnemies()
    {
        smallFastEnemies = true;
    }
    #endregion


    #region Normal
    public void BoogieWoogieShuffle()
    {
        boogieWoogie = true;
    }
    public void ShotgunOnly()
    {
        shotgunOnly = true;
    }

    public void RapidfireOnly()
    {
        rapidFireOnly = true;
    }
    #endregion


    #region Easy

    public void LargerStage()
    {
        largerStage = true;
    }

    public void LessHealth()
    {
        lessHealth = true;
    }

    public void SavingGrace()
    {
        savingGrace = true;
    }
    #endregion

    #endregion
}
