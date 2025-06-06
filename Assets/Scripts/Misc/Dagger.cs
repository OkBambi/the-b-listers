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
        if (Physics.Raycast(lastPos, transform.forward, out hit, rayDist, ~ignoreMask))
        {
            Debug.Log(hit.collider.name);

            if(hit.collider.CompareTag("groundTag"))
            {
                //REFLECT

                //step 1: get angle of incidence with Vector3.Up
                float dotProduct = Vector3.Dot(transform.forward, Vector3.up);
                float inverCos = Mathf.Acos(dotProduct);
                float angleOfIncidence = Mathf.Rad2Deg * inverCos;

                //step 2: calculate angle of reflection
                float reflectionAngle = 180f - angleOfIncidence;

                //step 3: rotate dagger
                transform.Rotate(Vector3.right, reflectionAngle);
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
