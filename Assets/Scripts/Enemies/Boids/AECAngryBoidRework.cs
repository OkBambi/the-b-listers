using UnityEngine;

public class AECAngryBoidRework : AngryBoidRework
{

    protected override void Start()
    {
        scaleOriginal = model.transform.localScale;
        ColorSelection(setColor);
        switch (setColor)
        {
            case PrimaryColor.RED:
                trail.material.color = Color.red;
                break;
            case PrimaryColor.YELLOW:
                trail.material.color = Color.yellow;
                break;
            case PrimaryColor.BLUE:
                trail.material.color = Color.blue;
                break;
        }
        base.UpdateBoidAwareness();
        StartCoroutine(NoiseWeights());

        name = "Angry Boid";

        if (LevelModifierManager.instance.lowEnemyCooldowns)
        {
            chargeCooldown[0] = chargeCooldown[0] * 0.25f;
            chargeCooldown[1] = chargeCooldown[1] * 0.25f;
        }

        if (LevelModifierManager.instance.smallFastEnemies)
        {
            model.transform.localScale = model.transform.localScale * 0.75f;
            maxSpeed = maxSpeed * 2f;
            stageWeight = stageWeight * 1.5f;
            playerWeight = playerWeight * 1.5f;
        }

        StartCoroutine(SwitchAIMode());
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
