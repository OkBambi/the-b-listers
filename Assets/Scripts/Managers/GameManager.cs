using UnityEngine;

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

    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.transform.parent.GetComponent<Player>();
        shootingPoint = GameObject.FindGameObjectWithTag("ShootingPoint").transform;
        //TimeScaleOrigin = Time.timeScale;
        TimeScaleOrigin = 1f;
        Time.timeScale = TimeScaleOrigin;
        Cursor.lockState=CursorLockMode.Locked;
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
}
