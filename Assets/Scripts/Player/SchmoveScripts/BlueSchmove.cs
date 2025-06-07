using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class BlueSchmove : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    //[SerializeField] float blueWindup;
    [SerializeField] float stickySpeed;
    [SerializeField] float timeBetweenPulses;
    [SerializeField] float pulseMaxRadius;
    [SerializeField] float pulseSpeed;
    [SerializeField] float amountOfPulses;
    [SerializeField] int pulseDmg;
    [SerializeField] GameObject sticky;

    [SerializeField] Transform shootingPoint;

    bool activated, startPulseTimer, isStuck;
    int pulsesDone;
    float origRadius;
    //float currentWindUp;
    SphereCollider sphereCollider;
    GameObject stickyCopy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if (!isStuck)
            {
                if (rb.gameObject.GetComponent<StickyMechanics>().GetStuckVal())
                {
                    isStuck = true;
                }
            }
            else
            {
                if (sphereCollider.radius < pulseMaxRadius && !startPulseTimer)
                {
                    
                    sphereCollider.radius += pulseSpeed * Time.deltaTime;
                }
                else
                {
                    if(amountOfPulses > pulsesDone)
                    {
                        if (!startPulseTimer)
                        {
                            rb.gameObject.GetComponent<StickyMechanics>().DmgParent();
                            pulsesDone++;
                            StartCoroutine(Pulse());
                        }
                    }
                    else
                    {
                        activated = false;
                        Destroy(rb.gameObject);
                    }
                }
            }
        }
    }

    public void Activate()
    {
        stickyCopy = Instantiate(sticky, shootingPoint.position, Quaternion.identity);
        rb = stickyCopy.GetComponent<Rigidbody>();
        sphereCollider = rb.gameObject.GetComponent<SphereCollider>();
        origRadius = sphereCollider.radius;
        rb.gameObject.GetComponent<StickyMechanics>().SetPulseDmg(pulseDmg);
        pulsesDone = 0;
        rb.linearVelocity = -shootingPoint.forward * stickySpeed;
        activated = true;

    }

    IEnumerator Pulse()
    {
        startPulseTimer = true;
        sphereCollider.radius = origRadius;
        yield return new WaitForSeconds(timeBetweenPulses);
        startPulseTimer = false;
    }
}
