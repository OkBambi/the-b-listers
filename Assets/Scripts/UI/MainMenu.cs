using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    public void PlayButton()
    {
        //when play is pressed, the game will start. currently is using a temp scene name
        SceneManager.LoadScene("SampleScene");
    }

    //not really needed, but its for the settings page
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
