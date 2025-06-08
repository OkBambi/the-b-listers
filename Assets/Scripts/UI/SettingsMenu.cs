using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    //audio variables
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public TextMeshProUGUI volText;

    //res stuff
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;


    void Start()
    {
        //resolution
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();  //clear all existing options from the dropdown

        List<string> options = new List<string>();  //create a list of options to populate the dropdown

        int currentResolutionIndex = 0; //track the index of the current screen resolution
        //loop for each element in our array
        for (int i = 0; i < resolutions.Length; i++)
        {
            //string option = "width" + " x " + "height";
            string option = resolutions[i].width + " x " + resolutions[i].height;   //a nicely formated string will be crated for them
            options.Add(option);    //then gets added to the list

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options); //once done, it gets back added to the res dropdown
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        if (audioMixer != null && volumeSlider != null && volText != null)
        {
            float currentVolume;
            if (audioMixer.GetFloat("Volume", out currentVolume))
            {
                volumeSlider.value = currentVolume;
                volText.text = ConvertDbToPercentage(currentVolume).ToString("F0") + "%"; // Convert dB to a more readable percentage
            }
            else
            {
                //fallback if parameter not found or other issue
                Debug.LogWarning("AudioMixer parameter 'Volume' not found or could not be retrieved. Ensure it's exposed correctly.");
                volumeSlider.value = 0; //defaults to 0
                volText.text = "0%";
            }
        }
        else
        {
            Debug.LogWarning("AudioMixer, Volume Slider, or Volume Text is not assigned in the Inspector.");
        }
    }


    //area for vol
    public void SetVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("Volume", volume); //og
        }

        if (volText != null)
        {
            volText.text = ConvertDbToPercentage(volume).ToString("F0") + "%";
        }
    }

    //if wanting graphics
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //this sets the resolution when the clicked value from the dropdown changes
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private float ConvertDbToPercentage(float db)
    {
        float linear = Mathf.Pow(10, db / 20);
        return linear * 100;
    }
}
