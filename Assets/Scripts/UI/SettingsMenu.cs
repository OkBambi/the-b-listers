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

        //beginning of the volume stuff
        //gets the initial volume from the AudioMixer and set the slider and text
        if (audioMixer != null && volumeSlider != null && volText != null)
        {
            float currentVolume;
            //trying to get the current vol from the AudioMixer
            if (audioMixer.GetFloat("Volume", out currentVolume))
            {
                //if we keep our AudioMixer parameter "Volume" set to receive values like -80 to 0 dB,
                // and our slider is still set to this -80 to 0, keep the code.
                volumeSlider.value = currentVolume;
                volText.text = ConvertDbToPercentage(currentVolume).ToString("F0") + "%"; // Convert dB to a more readable percentage
            }
            else
            {
                //fallback if parameter not found or other issue
                Debug.LogWarning("AudioMixer parameter 'Volume' not found or could not be retrieved. Ensure it's being called correctly.");
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
        //Debug.Log("Setting Volume to: {volume}"); //uncomment to see the slider's val
        if (audioMixer != null)
        {
            //sets the exposed parameter in the AudioMixer
            //"Volume" must match the name of the exposed parameter in your AudioMixer
            audioMixer.SetFloat("Volume", volume); //og
        }

        if (volText != null)
        {
            //updates the text display
            //the slider val will be in dB if you set your slider from -80 to 0. which it is unless someone changes it *stare*
            //since it is, we're displaying it as a percentage for readability
            //volText.text = ConvertDbToPercentage(volume).ToString("F0") + "%";

            //yoga's version of the vfx text update
            volText.text = (volumeSlider.value).ToString("F0") + "%";

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
