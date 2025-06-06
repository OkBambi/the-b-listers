using UnityEngine;
using UnityEngine.InputSystem.HID;

public class StickyMechanics : MonoBehaviour
{
    bool stuck;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stuck = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getStuckVal()
    {
        return stuck;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        if(!stuck)
        {
            transform.SetParent(other.transform);
            stuck = true;
            Rigidbody rb = transform.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
        }
        else
        {
            dmgOnEnter(other);
        }
    }

    private void dmgOnEnter(Collider other)
    {
        IDamage dmg = other.gameObject.GetComponent<IDamage>();
        if (dmg != null)
        {
            dmg.takeDamage(PrimaryColor.OMNI, 2);
        }
    }

    public void dmgParent()
    {
        IDamage dmg = transform.parent.GetComponent<IDamage>();
        if (dmg != null)
        {
            dmg.takeDamage(PrimaryColor.OMNI, 2);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
    }
}
