using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class MainMenuScript : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    //not really needed, but its for the settings page
    public void SettingsButton()
    {
        SceneManager.LoadScene("Settings");
    }

}
