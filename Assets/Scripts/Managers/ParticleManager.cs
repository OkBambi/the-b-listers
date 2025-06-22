using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;

    public Gradient DeflectRed;
    public Gradient DeflectYellow;
    public Gradient DeflectBlue;
    public Gradient DeflectBlack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;
    }
}
