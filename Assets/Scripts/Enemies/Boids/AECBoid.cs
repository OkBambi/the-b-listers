using UnityEngine;

public class AECBoid : BoidAI
{
    protected override void Start()
    {
        ColorSelection(setColor);
        base.UpdateBoidAwareness();
        StartCoroutine(NoiseWeights());
        name = "Boid";

        if (LevelModifierManager.instance.smallFastEnemies)
        {
            model.transform.localScale = model.transform.localScale * 0.75f;
            maxSpeed = maxSpeed * 2f;
            stageWeight = stageWeight * 1.5f;
            playerWeight = playerWeight * 1.5f;

        }
        if (LevelModifierManager.instance.lessHealth)
        {
            int NewHP = hp;
            NewHP = hp / 2;
            hp = NewHP;
        }
    }

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
