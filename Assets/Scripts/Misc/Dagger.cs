using UnityEngine;

public class Dagger : MonoBehaviour
{
    //we're gonna use raycasts instead of colliders
    //have the bullet travel forwards. Send a raycast between where it was last frame to where it is this frame.
    //if that raycast hits anything, bingo.

    [SerializeField] float speed;
    [SerializeField] LayerMask ignoreMask;
    [SerializeField] PrimaryColor projColor;

    Vector3 lastPos;
    bool reflected;

    [Space]
    public ParticleSystem colorParticles;
    public GameObject splatsPrefab;
    public Transform splatsHolder;

    private void Start()
    {
        colorParticles = GameObject.Find("Color Particles").GetComponent<ParticleSystem>();
        splatsHolder = GameObject.Find("TheSplatHolder").transform;
    }

    public void Initialize(PrimaryColor _color, float _speed, float _lifeTime, LayerMask _ignoreMask)
    {
        projColor = _color;
        speed = _speed;
        ignoreMask = _ignoreMask;

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
            }

            //check for damage
            IDamage dmg = hit.collider.GetComponent<IDamage>();

            if (dmg != null)
            {
                //HIT EM
                dmg.takeDamage(projColor, 1);

                //This is to cause color spray out of the enemy
                GameObject theSplat = Instantiate(splatsPrefab, hit.point, Quaternion.identity);
                theSplat.transform.SetParent(splatsHolder, true);
                theSplat.GetComponent<Splats>().Initialize();
                colorParticles.transform.position = hit.point;
                colorParticles.Play();
            }
        }

        lastPos = transform.position;
    }
}
