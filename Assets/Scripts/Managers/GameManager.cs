using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject MenuActive;
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
    public bool isWon;

    public Scene currentLevel;


    //chain ui
    [Space]
    [Header("ChainUI")]
    [SerializeField] WaveCollider ColorLockTimer;
    [SerializeField] ChainUIMonk LockColorChange;
    public RawImage lockMarker;
    public RawImage unlockedMarker;


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
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MainMenu")
        {
            PlayLevelMusic();
        }

        currentLevel = SceneManager.GetActiveScene();

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
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            SceneManager.LoadScene("Level_Showcase");
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene("Level_1");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene("Level_2");
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SceneManager.LoadScene("Level_3");
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            SceneManager.LoadScene("Level_4");
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            SceneManager.LoadScene("Level_Bonus");
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            SceneManager.LoadScene("Level_Boss");
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            SceneManager.LoadScene("MainMenu");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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

    public GameObject GetActiveMenu()
    {
        return MenuActive;
    }


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

    //CHAINUI
    public void ChainScreen(int ColorLockTimer)
    {

    lockMarker.gameObject.SetActive(true);
        unlockedMarker.gameObject.SetActive(false);
        AudioManager.instance.Play("Monk_Wave_Hit");
        StartCoroutine(ExitChainScreen(ColorLockTimer));
    }

    IEnumerator ExitChainScreen(int timer)
    {
        yield return new WaitForSeconds(timer);
        AudioManager.instance.Play("Monk_Wave_End");
      unlockedMarker.gameObject.SetActive(true);
        lockMarker.gameObject.SetActive(false);
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
                AudioManager.instance.Play("Level_2");
                break;

            case "Level_3":
                AudioManager.instance.Play("Level_3");
                break;

            case "Level_4":
                AudioManager.instance.Play("Level_4");
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
