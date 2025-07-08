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
            ComboManager.instance.AddScore(score);
            float scoreWithMult = ComboManager.instance.getScoreTimesMult(score);
            ComboFeed.theInstance.AddNewComboFeed("+ " + scoreWithMult.ToString() + " " + transform.name, scoreWithMult);
            Destroy(gameObject);
            return;
        }
    }
}
