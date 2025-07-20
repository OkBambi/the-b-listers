using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class MainMenuScript : MonoBehaviour
{

    public void Start()
    {
        StartCoroutine(AudioIsPlaying());
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Level_1");
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

    public IEnumerator AudioIsPlaying()
    {
        AudioManager.instance.Play("Main_Menu");
        yield return new WaitForSecondsRealtime(0.01f);
    }
}
