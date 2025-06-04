using System;
using System.Collections;
using UnityEngine;

public class BlueSchmove : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] Renderer model;

    [SerializeField] float blueWindup;
    [SerializeField] float timeBetweenPulses;
    [SerializeField] float pulseRadius;
    [SerializeField] float pulseSpeed;
    [SerializeField] float stickySpeed;

    bool activated, attached;
    int pulsesDone;
    int maxPulses = 3;
    Rigidbody holderRb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activated = true;//test for now
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            RaycastHit hit;
            if (!attached)
            {
                rb.AddForce(transform.forward * stickySpeed * Time.deltaTime, ForceMode.Impulse);
                if (Physics.Raycast(rb.transform.position, rb.transform.forward, out hit, 1, ~ignoreLayer))//distance is just that it needs to be close
                {
                    attached = true;
                    holderRb = rb;
                }
            }
            else
            {
                if (Physics.Raycast(holderRb.transform.position, holderRb.transform.forward, out hit, pulseRadius, ~ignoreLayer))
                {
                    rb.transform.position = hit.transform.position;
                    IDamage dmg = hit.collider.GetComponent<IDamage>();
                    StickToHit();

                    if (dmg != null)
                    {
                        dmg.takeDamage(PrimaryColor.OMNI, 2);
                    }
                }
                StartCoroutine(Pulse());
                activated = false;
                attached = false;
            }
        }
    }

    public void Activate()
    {
        pulsesDone = 0;
        activated = true;
    }

    IEnumerator Pulse()
    {
        while (pulsesDone < maxPulses)
        {
            model.material.color = Color.red;
            yield return new WaitForSeconds(timeBetweenPulses);
            model.material.color = Color.white;
            yield return new WaitForSeconds(timeBetweenPulses);
            pulsesDone++;
        }
    }

    void StickToHit()
    {

        
    }
}
