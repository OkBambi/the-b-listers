using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Toggle ArcadeToggler;
    public Toggle OutlineToggler;

    [SerializeField] GameObject MainCamera;
    [SerializeField] GameObject ArcadeCamera;

    public void onResume()
    {
        GameManager.instance.stateUnPause();
    }

    public void onRestart()
    {
        Debug.Log("Reloading State...");
        GameManager.instance.stateUnPause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void onSettings()
    {
        GameManager.instance.StateSettings();
    }

    public void onCredits()
    {
        GameManager.instance.stateUnPause();
    }

    public void ontoggleArcade(bool ison)
    { 
        if (ArcadeToggler.isOn)
        {
            Debug.Log("Filter On");
            MainCamera.SetActive(false);
            ArcadeCamera.SetActive(true);
        }
        else
        {
            Debug.Log("default Camera");
            MainCamera.SetActive(true);
            ArcadeCamera.SetActive(false);
        }
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
