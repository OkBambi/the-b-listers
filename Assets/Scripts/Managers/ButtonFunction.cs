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
        Debug.Log("Reloading State...");
        GameManager.instance.stateUnPause();
        SceneManager.LoadScene(0);

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
            Debug.Log("Filter On");
            //SettingsManager.instance.mainCamera.gameObject.SetActive(false);
            SettingsManager.instance.pixelCamera.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("default Camera");
            //SettingsManager.instance.mainCamera.gameObject.SetActive(true);
            SettingsManager.instance.pixelCamera.gameObject.SetActive(false);
        }
    }

    public void ontoggleArcade(bool toggle)
    {
        if (toggle)
        {
            Debug.Log("Filter On");
            //SettingsManager.instance.mainCamera.gameObject.SetActive(false);
            SettingsManager.instance.pixelCamera.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("default Camera");
            //SettingsManager.instance.mainCamera.gameObject.SetActive(true);
            SettingsManager.instance.pixelCamera.gameObject.SetActive(false);
        }
        ArcadeToggler.isOn = toggle;
    }

    public void BackButtonClick()
    {
        GameManager.instance.BackButton();
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
