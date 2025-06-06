using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

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


    bool activated, attached, isStuck;
    int pulsesDone;
    Rigidbody holderRb;
    int maxPulses = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activated = true;//test for now
        rb.linearVelocity = new Vector3(0,0,stickySpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if(!isStuck)
            {
                if (rb.gameObject.GetComponent<StickToObject>().stuck)
                {
                    isStuck = true;
                }
            }
            else
            {
                rb.gameObject.GetComponent<SphereCollider>().radius += pulseSpeed * Time.deltaTime;
                //IDamage dmg = hit.collider.GetComponent<IDamage>();
                //if (dmg != null)
                //{
                //    Debug.Log("NO");
                //    dmg.takeDamage(PrimaryColor.OMNI, 2);
                //}
                StartCoroutine(Pulse());
                //activated = false;
            }
        }
            //if (!attached)
            //{
            //    RaycastHit hit;
            //    if (Physics.SphereCast(rb.transform.position, rb.transform.localScale.x, rb.transform.forward, out hit, 1, ~ignoreLayer))//distance is just that it needs to be close
            //    {
            //        attached = true;
            //        rb.linearVelocity = new Vector3(0, 0, 0);
            //        rb.transform.SetParent(hit.transform);
            //    }
            //}
            //else
            //{

            //}
            //if (attached)
            //{
            //    rb.linearVelocity = new Vector3(0, 0, 0);
            //}
            //RaycastHit hit;
            //if (!attached)
            //{
            //    rb.AddForce(transform.forward * stickySpeed * Time.deltaTime, ForceMode.Impulse);
            //    if (Physics.Raycast(rb.transform.position, rb.transform.forward, out hit, 1, ~ignoreLayer))//distance is just that it needs to be close
            //    {
            //        attached = true;
            //        holderRb = rb;
            //        //rb.isKinematic = false;
            //        rb.transform.parent = hit.transform;
            //    }
            //}
            //else
            //{
            //    if (Physics.SphereCast(holderRb.transform.position, pulseRadius, holderRb.transform.forward, out hit, pulseRadius, ~ignoreLayer))
            //    {
            //        IDamage dmg = hit.collider.GetComponent<IDamage>();
            //        StickToHit();
            //        if (dmg != null)
            //        {
            //            Debug.Log("NO");
            //            dmg.takeDamage(PrimaryColor.OMNI, 2);
            //        }
            //    }
            //    isStuck = true;
            //    StartCoroutine(Pulse());
            //    activated = false;
            //    attached = false;
            //}
        //if (isStuck)
        //{
        //    rb.transform.position = rb.transform.parent.localPosition;
        //}
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
        Destroy(rb.gameObject);
        isStuck = false;
    }
}
