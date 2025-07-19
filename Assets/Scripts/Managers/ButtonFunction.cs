using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Toggle ArcadeToggler;
    public Toggle OutlineToggler;

    public void onResume()
    {
        GameManager.instance.stateUnPause();
    }

    public void onRestart()
    {
        GameManager.instance.statePause();
        SceneManager.LoadScene(0);
        ComboFeed.theInstance.clearFinalScore();
    }
    public void onSettings()
    {
        GameManager.instance.StateSettings();
    }

    public void onCredits()
    {
        GameManager.instance.stateUnPause();
    }

    public void onNextStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ontoggleArcade()
    { 
        if (ArcadeToggler.isOn)
        {
            SettingsManager.instance.pixelCamera.gameObject.SetActive(true);
        }
        else
        {
            SettingsManager.instance.pixelCamera.gameObject.SetActive(false);
        }
    }

    public void ontoggleArcade(bool toggle)
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (toggle)
            {
                SettingsManager.instance.pixelCamera.gameObject.SetActive(true);
            }
            else
            {
                SettingsManager.instance.pixelCamera.gameObject.SetActive(false);
            }
        }
        ArcadeToggler.isOn = toggle;
    }

    public void BackButtonClick()
    {
        GameManager.instance.BackButton();
    }

    public void OnButtonClicked()
    {
        AudioManager.instance.Play("Menu_Button_Click");
    }

    public void onQuit()
    {
#if !UNITY_EDITOR
Application.Quit();
#else 
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
