using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("Settings");
    }

    public void QuitButton()
    {
        Debug.Log("Quitting..");
        Application.Quit();
    }

}
