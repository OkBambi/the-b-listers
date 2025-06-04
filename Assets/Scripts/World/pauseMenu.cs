using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    [SerializeField] bool paused = false;
    [SerializeField] GameObject pauseMenuUI;

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
        Time.timeScale = 1.0f;
        paused = false;
    }

    public void Paused()
    {
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
