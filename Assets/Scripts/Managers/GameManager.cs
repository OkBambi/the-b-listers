using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject MenuActive;
    [SerializeField] GameObject MenuPause;
    [SerializeField] GameObject MenuWin;
    [SerializeField] GameObject MenuLose;
    [SerializeField] GameObject MenuSettings;
    [SerializeField] GameObject MenuCredits;
    [SerializeField] GameObject MenuGameInfo;

    public GameObject player;
    public PlayerMovement PlayerMovement;
    public bool isPaused;
    float TimeScaleOrigin;
    int gameGoalCount;

    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement = player.GetComponent<PlayerMovement>();
        TimeScaleOrigin = Time.timeScale;
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
        GameObject.FindFirstObjectByType<Player>().canAction = false;
        GameObject.FindFirstObjectByType<Player>().canCam = false;
        GameObject.FindFirstObjectByType<Player>().canAction = false;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void stateUnPause()
    {
        isPaused = !isPaused;

        GameObject.FindFirstObjectByType<Player>().canAction = true;
        GameObject.FindFirstObjectByType<Player>().canCam = true;
        GameObject.FindFirstObjectByType<Player>().canAction = true;

        Time.timeScale = TimeScaleOrigin;
        Cursor.visible = false;
        MenuActive.SetActive(false);
        MenuActive = null;
    }

    public void LoadSettings()
    {
        

    }

    public void OnLoseCondition()
    {
        statePause();
        //turn on the lose menu
    }

    public void OnWinCondition()
    {

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

    public int LoadHighscore()
    {
        return PlayerPrefs.GetInt("Highscore");
    }

    public void SaveHighscore(int _highscore)
    {
        PlayerPrefs.SetInt("Highscore", _highscore);
    }
}
