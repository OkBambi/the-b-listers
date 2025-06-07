using UnityEngine;
using UnityEngine.InputSystem.HID;

public class StickyMechanics : MonoBehaviour
{
    public bool stuck;
    int dmgAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stuck = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetStuckVal()
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
            rb.angularVelocity = Vector3.zero;
            Debug.Log("6");
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
}
