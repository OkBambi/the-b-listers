using UnityEngine;

[CreateAssetMenu(fileName = "SettingsObject", menuName = "Settings", order = 1)]
public class SettingsObject : ScriptableObject
{
    [Header("Graphics")]
    [Range(85, 110)] public int FOV;
    [Range(0f, 1f)] public float cameraShake;
    public string resolution;
    public string quality;

    public enum WindowType { Windowed, Fullscreen, FullscreenWindowed }
    public WindowType windowType;
    [Space]
    [Header("Sounds")]
    [Range(0f, 1f)] public float VFX;

}
