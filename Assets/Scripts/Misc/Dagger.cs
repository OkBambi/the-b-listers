using UnityEngine;

public class Dagger : MonoBehaviour
{
    //we're gonna use raycasts instead of colliders
    //have the bullet travel forwards. Send a raycast between where it was last frame to where it is this frame.
    //if that raycast hits anything, bingo.

    [SerializeField] float speed;
    [SerializeField] LayerMask ignoreMask;
    [SerializeField] PrimaryColor projColor;
    [SerializeField] GameObject deflectParticle;
    private ParticleSystem.TrailModule trail;

    Vector3 lastPos;
    bool reflected;

    public void Initialize(PrimaryColor _color, float _speed, float _lifeTime, LayerMask _ignoreMask, Gradient _trailGradient)
    {
        projColor = _color;
        speed = _speed;
        ignoreMask = _ignoreMask;
        trail = deflectParticle.GetComponent<ParticleSystem>().trails;
        trail.colorOverLifetime = _trailGradient;
        trail.colorOverTrail = _trailGradient;

        Destroy(gameObject, _lifeTime);
    }

    void Update()
    {
        transform.position += -transform.forward * speed * Time.deltaTime;

        Vector3 rayDir = transform.position - lastPos;
        float rayDist = rayDir.magnitude;

        RaycastHit hit;
        if (Physics.Raycast(lastPos, rayDir, out hit, rayDist, ~ignoreMask))
        {
            Debug.Log(hit.collider.name);

            if(hit.collider.CompareTag("groundTag") && !reflected)
            {
                //REFLECT
                reflected = true;
                Vector3 reflectionDir = Vector3.Reflect(transform.forward, hit.normal);

                transform.position = hit.point;
                transform.forward = reflectionDir.normalized;
                Instantiate(deflectParticle, transform.position, transform.rotation);
            }

            //check for damage
            IDamage dmg = hit.collider.GetComponent<IDamage>();

            if (dmg != null)
            {
                //HIT EM
                dmg.takeDamage(projColor, 1);
            }
        }

        lastPos = transform.position;
    }
}
