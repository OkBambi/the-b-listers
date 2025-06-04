using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RedSchmove : MonoBehaviour, ISchmove
{
    [SerializeField] Rigidbody rb;
    bool activated;

    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] float launchForce;
    [SerializeField] float hangTime;
    [SerializeField] float hookDist;
    [SerializeField] float hookForce;

    float holdTime;
    bool timeToSlam;

    void Start()
    {
        
    }

    void Update()
    { 

        if (activated)
        {
            holdTime += Time.deltaTime;

            //launch up
            if (holdTime <= Time.deltaTime) 
            {
                rb.AddForce(transform.up * launchForce, ForceMode.Impulse);
            }

            //start sending out raycast to where you're looking. And put a sphere indicator for landing
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, hookDist, ~ignoreLayer))
            {
                Debug.Log(hit.collider.name);
                //Gizmos.DrawSphere(hit.normal, 30f);
            }

            if (holdTime >= hangTime)
            {
                activated = false;
                //if raycast returned something, YOU. GO. THERE. NOW.
                if (hit.collider)
                {
                    Vector3 whereTo = (rb.transform.position - hit.point).normalized;

                    //rb.AddForce(-whereTo * hookForce, ForceMode.Impulse);
                    rb.MovePosition(transform.position + whereTo * Time.deltaTime * hookForce);
                }
                else
                {
                    //if nothing was returned, slam directly down. if you fall into the void this way, so be it.
                    rb.AddForce(-transform.up * hookForce, ForceMode.Impulse);
                }
            }
        }
    }

    public void Activate()
    {
        holdTime = 0;
        activated = true;
    }
}
