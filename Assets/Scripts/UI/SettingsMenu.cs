using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    //audio variable

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;


    void Start()
    {
        //resolution
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();


    }


    //area for fvx vol


    //if wanting graphics
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

}
