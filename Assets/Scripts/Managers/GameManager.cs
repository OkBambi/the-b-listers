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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
      instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement = player.GetComponent<PlayerMovement>();

        TimeScaleOrigin = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
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

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void OnLoseCondition()
    {

    }
    
    public void OnWinCondition()
    {

    }

    public void OnCreditInfo()
    {
        isPaused = true;

    }

    public void OnGameInfo()
    {
        isPaused = true;

    }

}
