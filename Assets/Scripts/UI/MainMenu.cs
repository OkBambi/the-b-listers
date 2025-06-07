using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//Placed in order of how they look on tbe main menu
public class MainMenuScript : MonoBehaviour
{
    //not really needed, but its for the settings page
    public void SettingsButton()
    {
        SceneManager.LoadScene("Settings");
    }

    public void PlayButton()
    {
        //when play is pressed, the game will start. currently is using a temp scene name
        SceneManager.LoadScene("SampleScene");
    }

    //button thing for credits
    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }

    //another for the game info
    public void GameInfoButton()
    {
        SceneManager.LoadScene("Game Info");
    }

    public void QuitButton()
    {
        Debug.Log("Quitting..");
        Application.Quit();
    }

}
