using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsObject", menuName = "Settings", order = 1)]
public class SettingsObject : ScriptableObject
{
    //public enum WindowType { Windowed, Fullscreen, FullscreenWindowed }

    [Header("Graphics")]

    public int resolution;
    public int quality;
    public FullScreenMode windowType;

    [Space]
    [Range(60, 110)] public int FOV;

    [Space]
    public bool isReducedCameraShake;
    public bool isInvertY;
    public bool isArcadeFilter;
    public bool isOutlineFilter;
    public bool isColourBlindnessMode;

    [Space]
    [Header("Sounds")]
    [Range(0f, 1f)] public float VFXVolume;
    [Range(0f, 1f)] public float MusicVolume;

    [Space]
    [Header("Controls")]
    [Range(0f, 1f)] public float mouseSensitivity;

}
