using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class BlueSchmove : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] float blueWindup;
    [SerializeField] float stickySpeed;
    [SerializeField] float timeBetweenPulses;
    [SerializeField] float pulseMaxRadius;
    [SerializeField] float pulseSpeed;
    [SerializeField] float amountOfPulses;
    [SerializeField] int pulseDmg;
    [SerializeField] GameObject sticky;

    [SerializeField] Transform shootingPoint;

    bool activated, startPulseTimer, isStuck, primed;
    int pulsesDone;
    float origRadius;
    float currentWindUp;
    SphereCollider sphereCollider;
    GameObject stickyParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Hello");
        if (activated)
        {
            Debug.Log("no no");
            currentWindUp += Time.deltaTime;
            if(!primed)
            {
                if(currentWindUp > blueWindup)
                {
                    Debug.Log("Getting there");
                    WindUp();
                }
            }
            else
            {
                if (!isStuck)
                {
                    if (rb.gameObject.GetComponent<StickyMechanics>().GetStuckVal())
                    {
                        Debug.Log("holy moly");
                        isStuck = true;
                        Destroy(stickyParent);
                    }
                }
                else
                {
                    if(sphereCollider != null) // for incase the enemy it is attached to is killed.
                    {
                        if (sphereCollider.radius < pulseMaxRadius && !startPulseTimer)
                        {

                            sphereCollider.radius += pulseSpeed * Time.deltaTime;
                        }
                        else
                        {
                            if (amountOfPulses > pulsesDone)
                            {
                                if (!startPulseTimer)
                                {
                                    pulsesDone++;
                                    StartCoroutine(Pulse());
                                    //stickyCopy.GetComponent<StickyMechanics>().DmgParent();
                                }
                            }
                            else
                            {
                                Destroy(rb.gameObject);
                                Reset();
                            }
                        }
                    }
                    else
                    {
                        Reset();
                    }
                }
            }
        }
    }

    public void Activate()
    {
        Debug.Log("i work");
        activated = true;
        if (stickyParent != null) //makes it so you are able to shoot another if it falls off the map and cooldown is over.
        {
            Debug.Log("kill");
            Destroy(rb.gameObject);
            Reset();
        }
    }

    private void WindUp()
    {
        stickyParent = Instantiate(sticky, shootingPoint.position, Quaternion.identity);
        rb = stickyParent.transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
        sphereCollider = rb.gameObject.GetComponent<SphereCollider>();
        origRadius = sphereCollider.radius;
        rb.gameObject.GetComponent<StickyMechanics>().SetPulseDmg(pulseDmg);
        rb.linearVelocity = -shootingPoint.forward * stickySpeed;
        primed = true;
    }

    private void Reset()
    {
        pulsesDone = 0;
        currentWindUp = 0;
        primed = false;
        activated = false;
        isStuck = false;
    }

    IEnumerator Pulse()
    {
        startPulseTimer = true;
        sphereCollider.radius = origRadius;
        yield return new WaitForSeconds(timeBetweenPulses);
        startPulseTimer = false;
    }
}
