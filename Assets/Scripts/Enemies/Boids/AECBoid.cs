using UnityEngine;

public class AECBoid : BoidAI
{
    public override void DeathCheck()
    {
        if (hp <= 0)
        {
            isAlive = false;
            OnAECDestroy();
            RemoveSelfFromTargetList();
            AudioManager.instance.Play("Enemy_Death");
            Destroy(gameObject);
            return;
        }
    }
}
