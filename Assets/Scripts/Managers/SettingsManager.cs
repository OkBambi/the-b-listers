using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    //basically this is the middleware to interact with the settings,
    //including reading data from the object and writing data into the object
    //(get and set)
    [SerializeField] SettingsObject settings;

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

}
