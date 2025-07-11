using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject MenuActive;
    [SerializeField] GameObject MenuPause;
    [SerializeField] GameObject MenuEnd;
    [SerializeField] GameObject MenuSettings;
    [SerializeField] GameObject MenuCredits;
    [SerializeField] GameObject MenuGameInfo;
    [SerializeField] GameObject PlayerHUD;


    public GameObject player;
    public Player playerScript;
    public bool isPaused;
    float TimeScaleOrigin;
    public bool isDead;

    [Space]
    [Header("Player Stuff")]
    public Transform shootingPoint;
    public ColorSwapping colorSwapper;
    public Schmoves schmover;
    public Timer timer;


    //chain ui
    [Space]
    [Header("ChainUI")]
    [SerializeField] WaveColorLockMonk ColorLockTimer;
    [SerializeField] ChainMarker[] ChainStates;
    [SerializeField] RawImage[] ChainToggleables;

    [SerializeField] LockColorChange LockColorChange;

    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.transform.parent.GetComponent<Player>();
        shootingPoint = GameObject.FindGameObjectWithTag("ShootingPoint").transform;
        //TimeScaleOrigin = Time.timeScale;
        TimeScaleOrigin = 1f;
        Time.timeScale = TimeScaleOrigin;
        Cursor.lockState = CursorLockMode.Locked;
        ChainStates = FindObjectsByType<ChainMarker>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MainMenu")
        {
            PlayLevelMusic();
        }

        ChainToggleables = new RawImage[2];

        foreach (ChainMarker chainMarker in ChainStates)
        {
            if (chainMarker.chainType == ChainType.Lock)
                {
                    ChainToggleables[0] = chainMarker.GetComponent<RawImage>();
                }
            if (chainMarker.chainType == ChainType.Unlock)
            {
                ChainToggleables[1] = chainMarker.GetComponent<RawImage>();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuActive == null)
            {
                statePause();
                MenuActive = MenuPause;
                MenuActive.SetActive(true);
            }
            else if (MenuActive == MenuPause)
            {
                stateUnPause();
            }
        }
    }

    public void statePause()
    {
        isPaused = !isPaused;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void BackButton()
    {
        PlayerHUD.SetActive(true);
        MenuActive.SetActive(false);
        MenuActive = MenuPause;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


    }
    public void StateSettings()
    {
        PlayerHUD.SetActive(false);
        MenuActive = MenuSettings;
        MenuActive.SetActive(true);
    }
    public void stateUnPause()
    {
        isPaused = !isPaused;
        Time.timeScale = TimeScaleOrigin;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        MenuActive.SetActive(false);
        MenuActive = null;
    }

    public void LoadSettings()
    {


    }

    public void OnEndCondition()
    {
        //turn on the lose menu
        if (!isDead)
        {
            statePause();
            isDead = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            MenuActive = MenuEnd;
            MenuActive.SetActive(true);
            ComboFeed.theInstance.FinalScore();
        }
    }

    //public void OnWinCondition()
    //{
    //    statePause();
    //    GameObject.FindFirstObjectByType<Player>().Die();
    //    MenuActive.SetActive(false);//so lose menu from die does not appear
    //    MenuActive = MenuEnd;
    //    MenuActive.SetActive(true);
    //}

    public void OnCreditInfo()
    {
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnGameInfo()
    {
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void OnApplicationQuit()
    {
#if !UNITY_EDITOR
        Application.Quit();
#else
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public int LoadHighscore()
    {
        return PlayerPrefs.GetInt("Highscore");
    }

    public void SaveHighscore(int _highscore)
    {
        PlayerPrefs.SetInt("Highscore", _highscore);
    }


    //CHAINUI
    public void ChainScreen(int ColorLockTimer)
    {
        ChainStates[1].gameObject.SetActive(true);
        ChainStates[0].gameObject.SetActive(false);
        AudioManager.instance.Play("Monk_Wave_Hit");
        StartCoroutine(ExitChainScreen(ColorLockTimer));
    }

    IEnumerator ExitChainScreen(int timer)
    {
        Debug.Log("BEFORE EXIT CHAIN SCREEN TRIGGER");
        yield return new WaitForSeconds(timer);
        AudioManager.instance.Play("Monk_Wave_End");
        ChainStates[0].gameObject.SetActive(true);
        ChainStates[1].gameObject.SetActive(false);
        Debug.Log("IVE GONE THROUGH IT, IT SHOULD WORK");
    }

    public void PlayLevelMusic()
    {

        Scene scene = SceneManager.GetActiveScene();
        switch (scene.name)
        {
            case "Level_1":

                AudioManager.instance.Play("Level_1");
                break;

            case "Level_2":
                AudioManager.instance.Play("Level 2");
                break;

            case "Level_3":
                AudioManager.instance.Play("Level_3");
                break;

            case "Level_4":
                AudioManager.instance.Play("Level4");
                break;

            case "Bonus_Level":
                AudioManager.instance.Play("Bonus_Level");
                break;

            case "Boss_Level":
                AudioManager.instance.Play("Boss_Level");
                break;

            case "MainMenu":
                AudioManager.instance.Play("Main_Menu");
                break;

            case "Level_Showcase":
                AudioManager.instance.Play("Level_3");
                break;

        }

    }

}
