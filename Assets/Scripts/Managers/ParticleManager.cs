using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;

    public Gradient DeflectRed;
    public Gradient DeflectYellow;
    public Gradient DeflectBlue;
    public Gradient DeflectBlack;
    public GameObject RedSchmoveSlamEffect;

    //This is for those color splashes
    public GameObject colorParticlesGameObject;
    public ParticleSystem colorParticles;
    public int hitParticleAmt;
    public int deathParticleAmt;
    public GameObject absorbEffect;
    public GameObject geyserEffect;
    public GameObject MonkEffect;

    public GameObject yellowSchmoveExplosion;
    public GameObject yellowSchmoveHit;
    public GameObject yellowSchmoveRailgunCircles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;
        //Debug.Log("color spawn");
        colorParticles = Instantiate(colorParticlesGameObject, new Vector3(100, 100, 100), Quaternion.identity).GetComponent<ParticleSystem>();
        colorParticles.name = colorParticlesGameObject.name;
    }
}
