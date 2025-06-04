using UnityEngine;

public class pauseMenu : MonoBehaviour
{

    [SerializeField] bool paused = false;
    [SerializeField] GameObject pauseMenuUI;

    private void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
                GameObject.FindFirstObjectByType<Player>().canAction = true;
                GameObject.FindFirstObjectByType<Player>().canCam = true;
                GameObject.FindFirstObjectByType<Player>().canAction = true;

            }
            else
            {
                Paused();
                GameObject.FindFirstObjectByType<Player>().canAction = false;
                GameObject.FindFirstObjectByType<Player>().canCam = false;
                GameObject.FindFirstObjectByType<Player>().canAction = false;
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        paused = false;
    }

    public void Paused()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
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
