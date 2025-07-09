using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        instance = this;
        mainCamera = FindFirstObjectByType<MainCamera_Marker>(FindObjectsInactive.Include).GetComponent<Camera>();
        pixelCamera = FindFirstObjectByType<PixelCamera_Marker>(FindObjectsInactive.Include).GetComponent<Camera>();
    }

    private void Start()
    {
        //on Start, read the settings object and apply the current settings to the settings screen in the scene

        FOVSlider slider = FindFirstObjectByType<FOVSlider>(FindObjectsInactive.Include);
        slider.SetFOV(GetFOV());

        //will need to be reworked because FindWithTag doesn't work on inactive objects
        FindFirstObjectByType<ReducedCameraShake_Marker>(FindObjectsInactive.Include).GetComponent<Toggle>().isOn = GetisReducedCameraShake();

        FindFirstObjectByType<ButtonFunction>(FindObjectsInactive.Exclude).GetComponent<ButtonFunction>().ontoggleArcade(GetisArcadeFilter());
        //FindFirstObjectByType<PixelCamera_Marker>(FindObjectsInactive.Include).GetComponent<Toggle>().isOn = GetisArcadeFilter();

        SettingsMenu settingsMenu = FindFirstObjectByType<SettingsMenu>(FindObjectsInactive.Include);

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

        settingsMenu.SetVFXVolume((GetVFXVolume() * 80f) - 80f);

    }

    #region Resolution
    public Resolution GetResolution()
    {
        return settings.resolution;
    }
    public void SetResolution(Resolution _resolution)
    {
        settings.resolution = _resolution;
    }
    #endregion

    #region Quality
    public SettingsObject.QualityTypes GetQuality()
    {
        return settings.quality;
    }
    public void SetQuality(SettingsObject.QualityTypes _quality)
    {
        settings.quality = _quality;
    }
    #endregion

    #region Window Type
    public SettingsObject.WindowType GetWindowType()
    {
        return settings.windowType;
    }
    public void SetWindowType(SettingsObject.WindowType _windowType)
    {
        settings.windowType = _windowType;
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
    }

    public void SetFOV(Slider _FOV)
    {
        settings.FOV = (int)_FOV.value;
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
    }

    public void SetisReducedCameraShake(Toggle _isReducedCameraShake)
    {
        settings.isReducedCameraShake = _isReducedCameraShake.isOn;
    }
    #endregion

    #region isInvertY
    public bool GetisInvertY()
    {
        return settings.isReducedCameraShake;
    }
    public void SetisInvertY(bool _isInvertY)
    {
        settings.isInvertY = _isInvertY;
    }

    public void SetisInvertY(Toggle _isInvertY)
    {
        settings.isInvertY = _isInvertY.isOn;
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
    }

    public void SetisArcadeFilter(Toggle _isArcadeFilter)
    {
        settings.isArcadeFilter = _isArcadeFilter.isOn;
    }
    #endregion

    #region isOutlineFilter
    public bool GetisOutlineFilter()
    {
        return settings.isOutlineFilter;
    }
    public void SetisOutlineFilter(bool _isOutlineFilter)
    {
        settings.isOutlineFilter = _isOutlineFilter;
    }

    public void SetisOutlineFilter(Toggle _isOutlineFilter)
    {
        settings.isOutlineFilter = _isOutlineFilter.isOn;
    }
    #endregion

    #region isReducedCameraShake
    public bool GetisColourBlindnessMode()
    {
        return settings.isColourBlindnessMode;
    }
    public void SetisColourBlindnessMode(bool _isColourBlindnessMode)
    {
        settings.isColourBlindnessMode = _isColourBlindnessMode;
    }

    public void SetisColourBlindnessMode(Toggle _isColourBlindnessMode)
    {
        settings.isColourBlindnessMode = _isColourBlindnessMode.isOn;
    }
    #endregion

    #region VFXVolume
    public float GetVFXVolume()
    {
        return settings.VFXVolume;
    }
    public void SetVFXVolume(float _VFXVolume)
    {
        settings.VFXVolume = _VFXVolume;
    }

    public void SetVFXVolume(Slider _VFXVolume)
    {
        settings.VFXVolume = _VFXVolume.value;
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
    }

    public void SetMusicVolume(Slider _MusicVolume)
    {
        settings.MusicVolume = _MusicVolume.value;
    }
    #endregion

    #region mouseSensitivity
    public float GetmouseSensitivity()
    {
        return settings.mouseSensitivity;
    }
    public void SetmouseSensitivity(float _mouseSensitivity)
    {
        settings.mouseSensitivity = _mouseSensitivity;
    }

    public void SetmouseSensitivity(Slider _mouseSensitivity)
    {
        settings.mouseSensitivity = _mouseSensitivity.value;
    }
    #endregion


}
