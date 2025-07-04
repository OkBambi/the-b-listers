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

    private void Awake()
    {
        instance = this;
    }

    #region Resolution
    public Resolution Resolution()
    {
        return settings.resolution;
    }
    public void Resolution(Resolution _resolution)
    {
        settings.resolution = _resolution;
    }
    #endregion

    #region Quality
    public SettingsObject.QualityTypes Quality()
    {
        return settings.quality;
    }
    public void Quality(SettingsObject.QualityTypes _quality)
    {
        settings.quality = _quality;
    }
    #endregion

    #region Window Type
    public SettingsObject.WindowType WindowType()
    {
        return settings.windowType;
    }
    public void WindowType(SettingsObject.WindowType _windowType)
    {
        settings.windowType = _windowType;
    }
    #endregion

    #region FOV
    public int FOV()
    {
        return settings.FOV;
    }
    public void FOV(int _FOV)
    {
        settings.FOV = _FOV;
    }

    public void FOV(Slider _FOV)
    {
        settings.FOV = (int)_FOV.value;
    }
    #endregion

    #region isReducedCameraShake
    public bool isReducedCameraShake()
    {
        return settings.isReducedCameraShake;
    }
    public void isReducedCameraShake(bool _isReducedCameraShake)
    {
        settings.isReducedCameraShake = _isReducedCameraShake;
    }

    public void isReducedCameraShake(Toggle _isReducedCameraShake)
    {
        settings.isReducedCameraShake = _isReducedCameraShake.isOn;
    }
    #endregion

    #region isInvertY
    public bool isInvertY()
    {
        return settings.isReducedCameraShake;
    }
    public void isInvertY(bool _isInvertY)
    {
        settings.isInvertY = _isInvertY;
    }

    public void isInvertY(Toggle _isInvertY)
    {
        settings.isInvertY = _isInvertY.isOn;
    }
    #endregion

    #region isArcadeFilter
    public bool isArcadeFilter()
    {
        return settings.isArcadeFilter;
    }
    public void isArcadeFilter(bool _isArcadeFilter)
    {
        settings.isArcadeFilter = _isArcadeFilter;
    }

    public void isArcadeFilter(Toggle _isArcadeFilter)
    {
        settings.isArcadeFilter = _isArcadeFilter.isOn;
    }
    #endregion

    #region isOutlineFilter
    public bool isOutlineFilter()
    {
        return settings.isOutlineFilter;
    }
    public void isOutlineFilter(bool _isOutlineFilter)
    {
        settings.isOutlineFilter = _isOutlineFilter;
    }

    public void isOutlineFilter(Toggle _isOutlineFilter)
    {
        settings.isOutlineFilter = _isOutlineFilter.isOn;
    }
    #endregion

    #region isReducedCameraShake
    public bool isColourBlindnessMode()
    {
        return settings.isColourBlindnessMode;
    }
    public void isColourBlindnessMode(bool _isColourBlindnessMode)
    {
        settings.isColourBlindnessMode = _isColourBlindnessMode;
    }

    public void isColourBlindnessMode(Toggle _isColourBlindnessMode)
    {
        settings.isColourBlindnessMode = _isColourBlindnessMode.isOn;
    }
    #endregion

    #region VFXVolume
    public float VFXVolume()
    {
        return settings.VFXVolume;
    }
    public void VFXVolume(float _VFXVolume)
    {
        settings.VFXVolume = _VFXVolume;
    }

    public void VFXVolume(Slider _VFXVolume)
    {
        settings.VFXVolume = _VFXVolume.value;
    }
    #endregion

    #region MusicVolume
    public float MusicVolume()
    {
        return settings.MusicVolume;
    }
    public void MusicVolume(float _MusicVolume)
    {
        settings.MusicVolume = _MusicVolume;
    }

    public void MusicVolume(Slider _MusicVolume)
    {
        settings.MusicVolume = _MusicVolume.value;
    }
    #endregion

    #region mouseSensitivity
    public float mouseSensitivity()
    {
        return settings.mouseSensitivity;
    }
    public void mouseSensitivity(float _mouseSensitivity)
    {
        settings.mouseSensitivity = _mouseSensitivity;
    }

    public void mouseSensitivity(Slider _mouseSensitivity)
    {
        settings.mouseSensitivity = _mouseSensitivity.value;
    }
    #endregion


}
