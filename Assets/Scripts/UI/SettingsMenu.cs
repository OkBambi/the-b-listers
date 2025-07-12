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
    public Slider vfxSlider;
    public TextMeshProUGUI vfxText;


    //res stuff
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public TMP_Dropdown quality;
    public TMP_Dropdown window;


    void Start()
    {
        //resolution
        resolutions = Screen.resolutions;

        //maura's code I used to
        //resolutionDropdown.ClearOptions();  //clear all existing options from the dropdown
        
        //List<string> options = new List<string>();  //create a list of options to populate the dropdown

        //int currentResolutionIndex = 0; //track the index of the current screen resolution
        ////loop for each element in our array
        //for (int i = 0; i < resolutions.Length; i++)
        //{
        //    //string option = "width" + " x " + "height";
        //    string option = resolutions[i].width + " x " + resolutions[i].height;   //a nicely formated string will be crated for them
        //    options.Add(option);    //then gets added to the list

        //    if (resolutions[i].width == Screen.currentResolution.width &&
        //        resolutions[i].height == Screen.currentResolution.height)
        //    {
        //        currentResolutionIndex = i;
        //    }
        //}
        //resolutionDropdown.AddOptions(options); //once done, it gets back added to the res dropdown
        //resolutionDropdown.value = currentResolutionIndex;
        //resolutionDropdown.RefreshShownValue();

        //beginning of the volume stuff
        //gets the initial volume from the AudioMixer and set the slider and text
        if (audioMixer != null && vfxSlider != null && vfxText != null)
        {
            float currentVolume;
            //trying to get the current vol from the AudioMixer
            if (audioMixer.GetFloat("Volume", out currentVolume))
            {
                //if we keep our AudioMixer parameter "Volume" set to receive values like -80 to 0 dB,
                // and our slider is still set to this -80 to 0, keep the code.
                vfxSlider.value = currentVolume;
                vfxText.text = ConvertDbToPercentage(currentVolume).ToString("F0") + "%"; // Convert dB to a more readable percentage
            }
            else
            {
                //fallback if parameter not found or other issue
                Debug.LogWarning("AudioMixer parameter 'Volume' not found or could not be retrieved. Ensure it's being called correctly.");
                vfxSlider.value = 0; //defaults to 0
                vfxText.text = "0%";
            }
        }
        else
        {
            Debug.LogWarning("AudioMixer, Volume Slider, or Volume Text is not assigned in the Inspector.");
        }
    }


    //area for vfx
    public void SetVFXVolume(float volume)
    {
        //Debug.Log("Setting Volume to: {volume}"); //uncomment to see the slider's val
        if (audioMixer != null)
        {
            //sets the exposed parameter in the AudioMixer
            //"Volume" must match the name of the exposed parameter in your AudioMixer
            audioMixer.SetFloat("Volume", volume); //og
        }

        if (vfxText != null)
        {
            //updates the text display
            //the slider val will be in dB if you set your slider from -80 to 0. which it is unless someone changes it *stare* ._.
            //since it is, we're displaying it as a percentage for readability
            //volText.text = ConvertDbToPercentage(volume).ToString("F0") + "%";

            //yoga's version of the vfx text update
            vfxText.text = ((vfxSlider.value + 80f) * 1.25f).ToString("F0") + "%";

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
        Resolution resolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetResolution(Resolution newResolution)
    {
        //reads an input and sets the window resolution
        Resolution resolution = newResolution;
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        for (int resolutionIndex = 0;  resolutionIndex < Screen.resolutions.Length; ++resolutionIndex)
        {
            if (Screen.resolutions[resolutionIndex].Equals(newResolution))
            {
                resolutionDropdown.value = resolutionIndex;
                Debug.Log(resolutionDropdown.value);
                break;
            }
        }
        
    }

    public void SetWindowSetting()
    {
        switch (window.value)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
        }
    }

    public void SetWindowSetting(FullScreenMode mode)
    {
        Screen.fullScreenMode = mode;
        switch (mode)
        {
            case FullScreenMode.Windowed:
                window.value = 0;
                break;
            case FullScreenMode.FullScreenWindow:
                window.value = 1;
                break;
            case FullScreenMode.ExclusiveFullScreen:
                window.value = 2;
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
