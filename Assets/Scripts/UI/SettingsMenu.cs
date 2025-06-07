using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    //area for fvx vol


    //for the resolution
    //low, medium, high
    //other res can be added to the dropdown if wanted, bit bugged
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
