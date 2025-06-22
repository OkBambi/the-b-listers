using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    /*
     * To change settings, use the corresponding method and pass in the value you want the settings to change to
        * E.g SettingsManager.instance.FOV(90);
        
     * To get settings, use the corresponding method and use the method's return value
        * E.g int settingsFOV = SettingsManager.instance.FOV();
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
    #endregion

    #region VFXVolume
    public float MusicVolume()
    {
        return settings.MusicVolume;
    }
    public void MusicVolume(float _MusicVolume)
    {
        settings.MusicVolume = _MusicVolume;
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
    #endregion


}
