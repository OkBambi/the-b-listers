using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    //audio variable

    //res stuff
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start()
    {
        //resolution
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();  //clear out all the optins in the resolution dropdown

        List<string> options = new List<string>();  //creat a list of strings that will be our options
        //loop for each element in our array
        for (int i = 0; i < resolutions.Length; i++)
        {
            //string option = "width" + " x " + "height";
            string option = resolutions[i].width + " x " + resolutions[i].height;   //a nicely formated string will be crated for them
            options.Add(option);    //then gets added to the list
        }
        resolutionDropdown.AddOptions(options); //once done, it gets back added to the res dropdown
    }


    //area for fvx vol


    //if wanting graphics
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
