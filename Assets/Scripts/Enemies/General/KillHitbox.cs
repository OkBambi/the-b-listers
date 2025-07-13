using UnityEngine;
using UnityEngine.InputSystem.HID;

public class KillHitbox : MonoBehaviour
{
    EnemyBase m_EnemyBase;

    private void Awake()
    {
        m_EnemyBase = GetComponentInParent<EnemyBase>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //check for damage
        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null)
        {
            //HIT EM
            dmg.takeDamage(m_EnemyBase.setColor, 1);
            if (m_EnemyBase.name != "Snake Head")
            {
                ComboFeed.theInstance.PlayerWasKilledBy(m_EnemyBase.name);
            }
            else
            {
                ComboFeed.theInstance.PlayerWasKilledBy("Snake");
            }

        }
    }
}
