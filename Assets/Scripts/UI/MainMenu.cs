using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//Placed in order of how they look on tbe main menu
public class MainMenuScript : MonoBehaviour
{
    public void PlayButton()
    {
        //Uncomment this when ready to use. And in the PLay bitton in unity, change from getactive to PlayButton()
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //when play is pressed, the game will start. currently is using a temp scene name
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitButton()
    {
        Debug.Log("Quitting..");
        Application.Quit();
    }

    //not really needed, but its for the settings page
    public void SettingsButton()
    {
        SceneManager.LoadScene("Settings");
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

}
