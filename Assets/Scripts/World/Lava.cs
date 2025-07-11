using System.Collections;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] Renderer model;
    [SerializeField] int cooldown;
    [SerializeField] bool isOnCooldown;
    [SerializeField] Vector3 teleportPosition;

    private void OnTriggerEnter(Collider other)
    {

        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null && other.CompareTag("Player"))
        {
            if (!isOnCooldown)
            {
                isOnCooldown = true;
                model.material.color = Color.red;

                other.transform.position = teleportPosition;

                    StartCoroutine(Cooldown());
                
            }
            else
            {
                dmg.takeDamage(PrimaryColor.OMNI, 100);
                ComboFeed.theInstance.PlayerWasKilledBy("The Void");
            }
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        model.material.color = Color.black;
        isOnCooldown = false;
    }
}
