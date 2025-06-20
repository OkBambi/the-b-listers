using UnityEngine;

[CreateAssetMenu(fileName = "SettingsObject", menuName = "Settings", order = 1)]
public class SettingsObject : ScriptableObject
{
    //public enum ResolutionTypes 
    //{ 
    //    _640x480, _800x600, _1024x768, _1152x864,
    //    _1280x600, _1280x720, _1280x768, _1280x800, _1280x960, _1280x1024,
    //    _1360x768, _1366x768, _1400x1050, _1440x900,
    //    _1600x900, _1600x1200, _1680x1050,
    //    _1792x1344, _1856x1392, _1920x1080, _1920x1200, _1920x1440,
    //    _2048x1152, _2048x1536, _2560x1440, _2560x1600
    //}
    public enum QualityTypes { Low, Medium, High }
    public enum WindowType { Windowed, Fullscreen, FullscreenWindowed }

    [Header("Graphics")]
    
    public Resolution resolution;
    public QualityTypes quality;
    public WindowType windowType;

    [Space]
    [Range(85, 110)] public int FOV;

    [Space]
    public bool isReducedCameraShake;
    public bool isArcadeFilter;
    public bool isOutlineFilter;
    public bool isColourBlindnessMode;

    [Space]
    [Header("Sounds")]
    [Range(0f, 1f)] public float VFX;

    [Space]
    [Header("Controls")]
    [Range(0f, 1f)] public float mouseSensitivity;

}
