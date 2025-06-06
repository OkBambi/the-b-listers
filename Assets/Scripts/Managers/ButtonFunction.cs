using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void onResume()
    {
        GameManager.instance.stateUnPause();
    }

    public void onRestart()
    {
        Debug.Log("Reloading State...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.instance.stateUnPause();

    }
    public void onSettings()
    {
        GameManager.instance.stateUnPause();
    }

    public void onCredits()
    {
        GameManager.instance.stateUnPause();
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
