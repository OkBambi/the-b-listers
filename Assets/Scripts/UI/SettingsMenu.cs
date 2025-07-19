using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    //audio variables
    public AudioMixer audioMixer;
    public Slider sfxSlider;
    public TextMeshProUGUI sfxText;
    public Slider musicSlider;
    public TextMeshProUGUI musicText;
    public Slider mouseSensitivitySlider;
    public TextMeshProUGUI mouseSensitivityText;

    public Button resetSettingsButton;


    //res stuff
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public TMP_Dropdown quality;
    public TMP_Dropdown window;


    void Start()
    {
        //resolution
        resolutions = Screen.resolutions;
        //beginning of the volume stuff
        //gets the initial volume from the AudioMixer and set the slider and text
        if (audioMixer != null && sfxSlider != null && sfxText != null)
        {
            float currentVolume;
            //trying to get the current vol from the AudioMixer
            if (audioMixer.GetFloat("Volume", out currentVolume))
            {
                //if we keep our AudioMixer parameter "Volume" set to receive values like -80 to 0 dB,
                // and our slider is still set to this -80 to 0, keep the code.
                sfxSlider.value = currentVolume;
                //sfxText.text = ConvertDbToPercentage(currentVolume).ToString("F0") + "%"; // Convert dB to a more readable percentage
                sfxText.text = ((sfxSlider.value + 80f) * 1.25f).ToString("F0") + "%";
            }
            else
            {
                //fallback if parameter not found or other issue
                Debug.LogWarning("AudioMixer parameter 'Volume' not found or could not be retrieved. Ensure it's being called correctly.");
                sfxSlider.value = 0; //defaults to 0
                sfxText.text = "0%";
            }
        }
        else
        {
            Debug.LogWarning("AudioMixer, Volume Slider, or Volume Text is not assigned in the Inspector.");
        }

        sfxSlider.onValueChanged.AddListener(delegate { AudioManager.instance.Play("Setting_SFX"); });
        sfxSlider.onValueChanged.AddListener(delegate { AudioManager.instance.UpdateVFXVolume(); });

        musicSlider.onValueChanged.AddListener(delegate { AudioManager.instance.Play("Setting_Music"); });
        musicSlider.onValueChanged.AddListener(delegate { AudioManager.instance.UpdateMusicVolume(); });

        resetSettingsButton.onClick.AddListener(delegate { SettingsManager.instance.ResetSettings(); });
    }


    //area for sfx
    public void SetSFXVolume(float volume)
    {
        //Debug.Log("Setting Volume to: {volume}"); //uncomment to see the slider's val
        if (audioMixer != null)
        {
            //sets the exposed parameter in the AudioMixer
            //"Volume" must match the name of the exposed parameter in your AudioMixer
            audioMixer.SetFloat("Volume", sfxSlider.value); //og
        }

        if (sfxText != null)
        {
            //yoga's version of the vfx text update
            sfxText.text = ((sfxSlider.value + 80f) * 1.25f).ToString("F0") + "%";
        }
    }

    public void SetSFX(float volume)
    {
        if (audioMixer != null)
        {
            //sets the exposed parameter in the AudioMixer
            //"Volume" must match the name of the exposed parameter in your AudioMixer
            audioMixer.SetFloat("Volume", volume); //og
        }

        if (sfxText != null)
        {
            sfxSlider.value = (volume * 80f) - 80f;

            //yoga's version of the vfx text update
            sfxText.text = ((sfxSlider.value + 80f) * 1.25f).ToString("F0") + "%";

        }
    }

    public void UpdateMusicText()
    {
        if (musicText != null)
        {
            musicText.text = ((musicSlider.value + 80f) * 1.25f).ToString("F0") + "%";
        }
    }

    public void SetMusic(float volume)
    {
        if (musicSlider != null)
        {
            musicSlider.value = (volume * 80f) - 80f;
            UpdateMusicText();
        }
    }

    public void UpdateMouseSensitivityText()
    {
        if (mouseSensitivityText != null)
        {
            mouseSensitivityText.text = (mouseSensitivitySlider.value * 100f).ToString("F0") + "%";
        }
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            GameManager.instance.playerScript.GetComponentInChildren<PlayerCamera>().sens = mouseSensitivitySlider.value;
        }
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        if (mouseSensitivitySlider != null)
        {
            mouseSensitivitySlider.value = sensitivity;
            UpdateMouseSensitivityText();
        }
    }


    //if wanting graphics
    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(quality.value);
    }
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        quality.value = index;
    }

    //this sets the resolution when the clicked value from the dropdown changes
    public void SetResolution()
    {
        //reads an input and sets the window resolution
        if (resolutions == null)
            resolutions = Screen.resolutions;

        if (resolutionDropdown.value < 26)
        {
            Resolution resolution = resolutions[resolutionDropdown.value];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
        }
        else
        {
            Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, Screen.fullScreenMode);
        }
    }

    public void SetResolution(int res)
    {
        if (res < 26)
        {
            Resolution resolution = Screen.resolutions[res];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
        else
        {
            Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, Screen.fullScreen);
        }
        resolutionDropdown.value = res;
    }

    public void SetWindowSetting()
    {
        switch (window.value)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
            case 3:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }

    public void SetWindowSetting(FullScreenMode mode)
    {
        Screen.fullScreenMode = mode;
        switch (mode)
        {
            case FullScreenMode.ExclusiveFullScreen:
                window.value = 0;
                break;
            case FullScreenMode.FullScreenWindow:
                window.value = 1;
                break;
            case FullScreenMode.MaximizedWindow:
                window.value = 2;
                break;
            case FullScreenMode.Windowed:
                window.value = 3;
                break;
        }
    }

    //nom nom. this is a helper function that converts the dB to be more friendly for the player (0-100)
    //our 0dB is at max vol (100%) and our -80dB is the min vol (0%)
    private float ConvertDbToPercentage(float db)
    {
        //linear 0-1 from dB. the formula will work since our mixer param is set to -80 to 0
        //formula in question; 10^(dB/20). this gives the linear vol from 0 to 1
        //we then want to multiply by 100 for the percentage
        float linear = Mathf.Pow(10, db / 20);
        return linear * 100;
    }
}
