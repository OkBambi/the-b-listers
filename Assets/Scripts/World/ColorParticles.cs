using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ColorParticles : MonoBehaviour
{
    public ParticleSystem colorParticles;
    public GameObject splatsPrefab;
    public Transform splatsHolder;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    public Color startColor;

    private void Start()
    {
        splatsHolder = GameObject.Find("TheSplatHolder").transform;
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(colorParticles, other, collisionEvents);

        int count = collisionEvents.Count;

        for (int index = 0; index < count; index++)
        {
            GameObject theSplat = Instantiate(splatsPrefab, collisionEvents[index].intersection, Quaternion.identity);
            theSplat.transform.SetParent(splatsHolder, true);
            var colorParticles = EnemyManager.instance.colorParticles.main;
            theSplat.GetComponent<Splats>().Initialize(startColor);
        }
    }
}
