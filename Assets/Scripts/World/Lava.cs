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
                if (!LevelModifierManager.instance.savingGrace)
                {
                    model.material.color = Color.red;
                }
                other.transform.position = teleportPosition;
                StartCoroutine(Cooldown());
                return;
            }
            if (!LevelModifierManager.instance.savingGrace)
            {
                dmg.takeDamage(PrimaryColor.OMNI, 100);
                ComboFeed.theInstance.PlayerWasKilledBy("The Void");
            }
            else
            {
                other.transform.position = teleportPosition;
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
