using UnityEngine;
using UnityEngine.Animations;
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
            if(other.CompareTag("groundTag")) //makes the sticky not get squashed when touching the floor
            {
                transform.SetParent(other.transform.parent);
            }
            else
            {
                transform.SetParent(other.transform);

                //allows the sticky to stay with the enemy
                transform.GetComponent<PositionConstraint>().constraintActive = true;
            }
            stuck = true;
            Rigidbody rb = transform.GetComponent<Rigidbody>();
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
