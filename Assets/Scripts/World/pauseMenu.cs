using UnityEngine;

public class pauseMenu : MonoBehaviour
{

    [SerializeField] bool paused = false;
    [SerializeField] GameObject pauseMenuUI;

    void Start()
    {
        paused = false;
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();

            }
            else
            {
                Paused();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindFirstObjectByType<Player>().canAction = true;
        GameObject.FindFirstObjectByType<Player>().canCam = true;
        GameObject.FindFirstObjectByType<Player>().canAction = true;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        paused = false;
    }

    public void Paused()
    {
        pauseMenuUI.SetActive(true);
        GameObject.FindFirstObjectByType<Player>().canAction = false;
        GameObject.FindFirstObjectByType<Player>().canCam = false;
        GameObject.FindFirstObjectByType<Player>().canAction = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0.001f;
        paused = true;
    }

    public void LoadSetting()
    {
        Debug.Log("Loading Settings...");
    }

    public void QuitGame()
    {
        Debug.Log("Exiting Game..");
        Application.Quit();
    }

}
