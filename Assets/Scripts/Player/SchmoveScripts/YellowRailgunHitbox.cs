using UnityEngine;
using UnityEngine.InputSystem.HID;

public class YellowRailgunHitbox : MonoBehaviour
{
    public int railgunDmg;
    private void Awake()
    {
        Invoke("DestroySelf", 0.2f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        //check for damage
        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null)
        {
            //HIT EM
            dmg.takeDamage(PrimaryColor.OMNI, railgunDmg);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
