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
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, rayDist, ~ignoreMask))
        {
            Debug.Log(hit.collider.name);

            //IDamage dmg = hit.collider.GetComponent<IDamage>();

            //if (dmg != null)
            //{
            //    //HIT EM
            //    dmg.takeDamage(shootDamage);
            //}
        }

        lastPos = transform.position;
    }
}
