using UnityEngine;
using UnityEngine.InputSystem.HID;

public class BoidKillHitbox : MonoBehaviour
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
        }
    }
}
