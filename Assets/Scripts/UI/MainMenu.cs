using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class MainMenuScript : MonoBehaviour
{
    [SerializeField] AudioManager audioGuy;

    public void Start()
    {
        AudioIsPlaying();
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

    public void AudioIsPlaying()
    {
        Debug.Log("It is deffinetly playing Audio");
        AudioManager.instance.Play("Main_Menu");
    }
}
