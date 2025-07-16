using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    /*
     * To change settings, use the corresponding method and pass in the value you want the settings to change to
        * E.g SettingsManager.instance.FOV(90);
        
     * To get settings, use the corresponding method and use the method's return value
        * E.g int settingsFOV = SettingsManager.instance.FOV();
        
        
        or drag in the UI specific function into the UI element
     */

    public static SettingsManager instance;

    [SerializeField] SettingsObject settings;

    public Camera mainCamera;
    public Camera pixelCamera;

    public ButtonFunction button;

    private void Awake()
    {
        instance = this;
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            mainCamera = FindFirstObjectByType<MainCamera_Marker>(FindObjectsInactive.Include).GetComponent<Camera>();
            pixelCamera = FindFirstObjectByType<PixelCamera_Marker>(FindObjectsInactive.Include).GetComponent<Camera>();
        }

        if (PlayerPrefs.HasKey("resolution"))
        {
            settings.resolution = PlayerPrefs.GetInt("resolution");
            settings.quality = PlayerPrefs.GetInt("quality");
            settings.windowType = (FullScreenMode)PlayerPrefs.GetInt("windowType");
            settings.FOV = PlayerPrefs.GetInt("fov");
            settings.isReducedCameraShake = (PlayerPrefs.GetInt("reducedCameraShake") == 1 ? true : false);
            settings.isInvertY = (PlayerPrefs.GetInt("invertY") == 1 ? true : false);
            settings.isArcadeFilter = (PlayerPrefs.GetInt("arcade") == 1 ? true : false);
            settings.SFXVolume = PlayerPrefs.GetFloat("sfx");
            settings.MusicVolume = PlayerPrefs.GetFloat("music");
            settings.mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity");
        }
        else
        {
            ResetSettings();
        }
    }

    private void Start()
    {
        //on Start, read the settings object and apply the current settings to the settings screen in the scene

        FOVSlider slider = FindFirstObjectByType<FOVSlider>(FindObjectsInactive.Include);
        slider.SetFOV(GetFOV());

        SettingsMenu settingsMenu = FindFirstObjectByType<SettingsMenu>(FindObjectsInactive.Include);

        settingsMenu.SetResolution(Screen.resolutions[GetResolution()]);

        settingsMenu.SetQuality(GetQuality());

        settingsMenu.SetWindowSetting(GetWindowType());

        settingsMenu.SetSFX(GetSFXVolume());

        settingsMenu.SetMusic(GetMusicVolume());

        //will need to be reworked because FindWithTag doesn't work on inactive objects
        FindFirstObjectByType<ReducedCameraShake_Marker>(FindObjectsInactive.Include).GetComponent<Toggle>().isOn = GetisReducedCameraShake();

        
        button = FindFirstObjectByType<ButtonFunction>(FindObjectsInactive.Exclude).GetComponent<ButtonFunction>();
        button.ontoggleArcade(GetisArcadeFilter());
        //FindFirstObjectByType<PixelCamera_Marker>(FindObjectsInactive.Include).GetComponent<Toggle>().isOn = GetisArcadeFilter();

        FindFirstObjectByType<InvertY_Marker>(FindObjectsInactive.Include).GetComponent<Toggle>().isOn = GetisInvertY();
        ApplyInvertY();

        settingsMenu.SetMouseSensitivity(GetmouseSensitivity());

        ////Resolution
        //Dropdown dropDown = GameObject.FindWithTag("Resolution").GetComponent<Dropdown>();
        //var options = dropDown.options;

        //for (int resIndex = 0; resIndex < options.Count; ++resIndex)
        //{
        //    if (options[resIndex].text == GetResolution().ToString())
        //    {
        //        dropDown.value = resIndex;
        //        settingsMenu.SetResolution(resIndex);
        //    }
        //}

        ////Quality
        //dropDown = GameObject.FindWithTag("Quality").GetComponent<Dropdown>();
        //options = dropDown.options;

        //for (int qualityIndex = 0; qualityIndex < options.Count; ++qualityIndex)
        //{
        //    if (options[qualityIndex].text == GetResolution().ToString())
        //    {
        //        dropDown.value = qualityIndex;
        //        settingsMenu.SetQuality(qualityIndex);
        //    }
        //}

        ////WindowSettings
        //dropDown = GameObject.FindWithTag("WindowSetting").GetComponent<Dropdown>();
        //options = dropDown.options;

        //for (int winIndex = 0; winIndex < options.Count; ++winIndex)
        //{
        //    if (options[winIndex].text == GetResolution().ToString())
        //    {
        //        dropDown.value = winIndex;
        //        //settingsMenu.SetWindow(winIndex);
        //    }
        //}



    }

    #region Resolution
    public int GetResolution()
    {
        //Debug.Log(settings.resolution);
        return settings.resolution;
    }
    public void SetResolution(Resolution _resolution)
    {
        for (int resIndex = 0; resIndex < Screen.resolutions.Length; resIndex++)
        {
            if (Screen.resolutions[resIndex].Equals(_resolution))
                settings.resolution = resIndex;
        }
        PlayerPrefs.SetInt("resolution", settings.resolution);
        PlayerPrefs.Save();
    }
    public void SetResolution(TMP_Dropdown _resolution)
    {
        settings.resolution = _resolution.value;
        PlayerPrefs.SetInt("resolution", settings.resolution);
        PlayerPrefs.Save();
    }
    #endregion

    #region Quality
    public int GetQuality()
    {
        return settings.quality;
    }
    public string GetQualityName()
    {
        return QualitySettings.names[settings.quality];
    }
    public void SetQuality(int _quality)
    {
        settings.quality = _quality;
        PlayerPrefs.SetInt("quality", settings.quality);
        PlayerPrefs.Save();
    }
    public void SetQuality(TMP_Dropdown _quality)
    {
        settings.quality = _quality.value;
        PlayerPrefs.SetInt("quality", settings.quality);
        PlayerPrefs.Save();
    }
    #endregion

    #region Window Type
    public FullScreenMode GetWindowType()
    {
        return settings.windowType;
    }
    public void SetWindowType(FullScreenMode _windowType)
    {
        settings.windowType = _windowType;
        PlayerPrefs.SetInt("windowType", (int)_windowType);
        PlayerPrefs.Save();
    }
    public void SetWindowType(TMP_Dropdown _windowType)
    {
        settings.windowType = (FullScreenMode)_windowType.value;
        PlayerPrefs.SetInt("windowType", _windowType.value);
        PlayerPrefs.Save();
    }
    #endregion

    #region FOV
    public int GetFOV()
    {
        return settings.FOV;
    }
    public void SetFOV(int _FOV)
    {
        settings.FOV = _FOV;
        PlayerPrefs.SetInt("fov", settings.FOV);
        PlayerPrefs.Save();
    }

    public void SetFOV(Slider _FOV)
    {
        settings.FOV = (int)_FOV.value;
        PlayerPrefs.SetInt("fov", settings.FOV);
        PlayerPrefs.Save();
    }
    #endregion

    #region isReducedCameraShake
    public bool GetisReducedCameraShake()
    {
        return settings.isReducedCameraShake;
    }
    public void SetisReducedCameraShake(bool _isReducedCameraShake)
    {
        settings.isReducedCameraShake = _isReducedCameraShake;
        PlayerPrefs.SetInt("reducedCameraShake", (settings.isReducedCameraShake ? 1: 0));
        PlayerPrefs.Save();
    }

    public void SetisReducedCameraShake(Toggle _isReducedCameraShake)
    {
        settings.isReducedCameraShake = _isReducedCameraShake.isOn;
        PlayerPrefs.SetInt("reducedCameraShake", (settings.isReducedCameraShake ? 1 : 0));
        PlayerPrefs.Save();
    }
    #endregion

    #region isInvertY
    public bool GetisInvertY()
    {
        return settings.isInvertY;
    }
    public void SetisInvertY(bool _isInvertY)
    {
        settings.isInvertY = _isInvertY;
        PlayerPrefs.SetInt("invertY", (settings.isInvertY ? 1 : 0));
        PlayerPrefs.Save();
    }

    public void SetisInvertY(Toggle _isInvertY)
    {
        settings.isInvertY = _isInvertY.isOn;
        PlayerPrefs.SetInt("invertY", (settings.isInvertY ? 1 : 0));
        PlayerPrefs.Save();
    }
    #endregion

    #region isArcadeFilter
    public bool GetisArcadeFilter()
    {
        return settings.isArcadeFilter;
    }
    public void SetisArcadeFilter(bool _isArcadeFilter)
    {
        settings.isArcadeFilter = _isArcadeFilter;
        PlayerPrefs.SetInt("arcade", (settings.isArcadeFilter ? 1 : 0));
        PlayerPrefs.Save();
    }

    public void SetisArcadeFilter(Toggle _isArcadeFilter)
    {
        settings.isArcadeFilter = _isArcadeFilter.isOn;
        PlayerPrefs.SetInt("arcade", (settings.isArcadeFilter ? 1 : 0));
        PlayerPrefs.Save();
    }
    #endregion

    //#region isOutlineFilter
    //public bool GetisOutlineFilter()
    //{
    //    return settings.isOutlineFilter;
    //}
    //public void SetisOutlineFilter(bool _isOutlineFilter)
    //{
    //    settings.isOutlineFilter = _isOutlineFilter;
    //}

    //public void SetisOutlineFilter(Toggle _isOutlineFilter)
    //{
    //    settings.isOutlineFilter = _isOutlineFilter.isOn;
    //}
    //#endregion

    //#region isColourBlindnessMode
    //public bool GetisColourBlindnessMode()
    //{
    //    return settings.isColourBlindnessMode;
    //}
    //public void SetisColourBlindnessMode(bool _isColourBlindnessMode)
    //{
    //    settings.isColourBlindnessMode = _isColourBlindnessMode;
    //}

    //public void SetisColourBlindnessMode(Toggle _isColourBlindnessMode)
    //{
    //    settings.isColourBlindnessMode = _isColourBlindnessMode.isOn;
    //}
    //#endregion

    #region SFXVolume
    public float GetSFXVolume()
    {
        return settings.SFXVolume;
    }
    public void SetSFXVolume(float _SFXVolume)
    {
        settings.SFXVolume = _SFXVolume;
        PlayerPrefs.SetFloat("sfx", settings.SFXVolume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(Slider _SFXVolume)
    {
        settings.SFXVolume = (_SFXVolume.value + 80f) / 80f;
        PlayerPrefs.SetFloat("sfx", settings.SFXVolume);
        PlayerPrefs.Save();
    }
    #endregion

    #region MusicVolume
    public float GetMusicVolume()
    {
        return settings.MusicVolume;
    }
    public void SetMusicVolume(float _MusicVolume)
    {
        settings.MusicVolume = _MusicVolume;
        PlayerPrefs.SetFloat("music", settings.MusicVolume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(Slider _MusicVolume)
    {
        settings.MusicVolume = (_MusicVolume.value + 80f) / 80f;
        PlayerPrefs.SetFloat("music", settings.MusicVolume);
        PlayerPrefs.Save();
    }
    #endregion

    #region MouseSensitivity
    public float GetmouseSensitivity()
    {
        return settings.mouseSensitivity;
    }
    public void SetmouseSensitivity(float _mouseSensitivity)
    {
        settings.mouseSensitivity = _mouseSensitivity;
        PlayerPrefs.SetFloat("mouseSensitivity", settings.mouseSensitivity);
        PlayerPrefs.Save();
    }

    public void SetmouseSensitivity(Slider _mouseSensitivity)
    {
        settings.mouseSensitivity = _mouseSensitivity.value;
        PlayerPrefs.SetFloat("mouseSensitivity", settings.mouseSensitivity);
        PlayerPrefs.Save();
    }
    #endregion

    public void ApplyInvertY()
    {
        //Debug.Log(GetisInvertY());
        if (GameManager.instance)
            GameManager.instance.playerScript.GetComponentInChildren<PlayerCamera>().invertY = GetisInvertY();
    }

    public void ResetSettings()
    {
        SetResolution(Screen.resolutions[10]);
        SetQuality(3);
        SetWindowType(0);
        SetFOV(90);
        SetisReducedCameraShake(true);
        SetisInvertY(false);
        SetisArcadeFilter(false);
        SetSFXVolume(0f);
        SetMusicVolume(0f);
        SetmouseSensitivity(1f);
    }
}
