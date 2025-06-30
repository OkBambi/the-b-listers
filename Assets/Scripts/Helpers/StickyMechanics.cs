using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem.HID;
using System.Collections;

public class StickyMechanics : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    float blueWindup;
    float stickySpeed;
    float timeBetweenPulses;
    float pulseMaxRadius;
    float pulseSpeed;
    float amountOfPulses;
    int pulseDmg;

    bool activated, startPulseTimer, isStuck, primed;
    int pulsesDone;
    float origRadius;
    float currentWindUp;
    SphereCollider sphereCollider;
    GameObject stickyParent;
    Transform shootingPoint;
    int dmgAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            currentWindUp += Time.deltaTime;
            if (!primed)
            {
                if (currentWindUp > blueWindup)
                {
                    WindUp();
                }
            }
            else
            {
                if (sphereCollider != null) // for incase the enemy it is attached to is killed.
                {
                    if (sphereCollider.radius < pulseMaxRadius && !startPulseTimer)
                    {
                        sphereCollider.radius += pulseSpeed * Time.deltaTime;
                        transform.GetChild(0).localScale += new Vector3(pulseSpeed * Time.deltaTime, pulseSpeed * Time.deltaTime, pulseSpeed * Time.deltaTime);
                    }
                    else
                    {
                        if (amountOfPulses > pulsesDone)
                        {
                            if (!startPulseTimer)
                            {
                                pulsesDone++;
                                StartCoroutine(Pulse());
                                gameObject.GetComponent<StickyMechanics>().DmgParent();
                            }
                        }
                        else
                        {
                            Destroy(gameObject);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        if(!isStuck)
        {
            if (other.CompareTag("groundTag")) //makes the sticky not get squashed when touching the floor
            {
                transform.SetParent(other.transform.parent);
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                transform.SetParent(other.transform);

                //allows the sticky to stay with the enemy
                transform.GetComponent<PositionConstraint>().constraintActive = true;
            }
            Destroy(stickyParent);
            isStuck = true;
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            DmgOnEnter(other);
        }
    }

    private void DmgOnEnter(Collider other)
    {
        IDamage dmg = other.gameObject.GetComponent<IDamage>();
        if (dmg != null)
        {
            dmg.takeDamage(PrimaryColor.OMNI, dmgAmount);
        }
    }

    public void DmgParent()
    {
        if (!transform.parent) return;
        IDamage dmg = transform.parent.GetComponent<IDamage>();
        if (dmg != null)
        {
            dmg.takeDamage(PrimaryColor.OMNI, dmgAmount);
        }
    }

    public void SetPulseDmg(int dmg)
    {
        dmgAmount = dmg;
    }

    private void Reset()
    {
        pulsesDone = 0;
        currentWindUp = 0;
        primed = false;
        activated = false;
        isStuck = false;
    }

    private void WindUp()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        sphereCollider = gameObject.GetComponent<SphereCollider>();
        origRadius = sphereCollider.radius;
        gameObject.GetComponent<StickyMechanics>().SetPulseDmg(pulseDmg);
        primed = true;
    }

    public void setActive(Transform _shootingPoint, GameObject _stickyParent, float _blueWindup, float _stickySpeed, float _timeBetweenPulses, float _pulseMaxRadius, float _pulseSpeed, float _amountOfPulses, int _pulseDmg)
    {
        activated = true;
        shootingPoint = _shootingPoint;
        stickyParent = _stickyParent;
        blueWindup = _blueWindup;
        stickySpeed = _stickySpeed;
        timeBetweenPulses = _timeBetweenPulses;
        pulseMaxRadius = _pulseMaxRadius;
        pulseSpeed = _pulseSpeed;
        amountOfPulses = _amountOfPulses;
        pulseDmg = _pulseDmg;
        gameObject.GetComponent<Rigidbody>().linearVelocity = -shootingPoint.forward * stickySpeed;
    }

    IEnumerator Pulse()
    {
        startPulseTimer = true;
        sphereCollider.radius = origRadius;
        gameObject.transform.GetChild(0).localScale = new Vector3(origRadius, origRadius, origRadius);
        yield return new WaitForSeconds(timeBetweenPulses);
        startPulseTimer = false;
    }
}
