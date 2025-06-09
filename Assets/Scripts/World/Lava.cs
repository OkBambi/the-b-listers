using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    { 

        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null )
        {
            Debug.Log("Lava.");
            dmg.takeDamage(PrimaryColor.OMNI, 100);
        }
    }
}
