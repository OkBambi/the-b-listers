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


        //initialises the slider and display value based on current mixer value
        float currentVolume;
        if (audioMixer.GetFloat("Volume", out currentVolume))
        {
            //if using logarithmic values (e.g., -80 to 0 dB)
            float normalised = Mathf.InverseLerp(-80f, 0f, currentVolume);
            float percent = normalised * 100f;
            volumeSlider.value = percent;

            if (volText != null)
                volText.text = Mathf.RoundToInt(percent).ToString();
        }
    }


    //area for vol
    public void SetVolume(float volume)
    {
        //converts slider percentage (0–100) to mixer value (log scale)
        float volumeDB = Mathf.Lerp(-80f, 0f, volume / 100f);

        audioMixer.SetFloat("Volume", volume);

        if (volText != null)
        {
            volText.text = Mathf.RoundToInt(volume).ToString();
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

}
