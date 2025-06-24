using UnityEngine;

public class SnakeHead : EnemyBase
{
    [SerializeField] Snake snakeBody;


    protected virtual void Start()
    {
        ColorSelection(setColor);
    }

    public override void DeathCheck()
    {
        if (hp <= 0)
        {
            isAlive = false;
            ComboManager.instance.AddScore(score);
            float scoreWithMult = ComboManager.instance.getScoreTimesMult(score);
            ComboFeed.theInstance.AddNewComboFeed("+ " + scoreWithMult.ToString() + " " + transform.name, scoreWithMult);
            snakeBody.takeDamage(PrimaryColor.OMNI, 1);
            Destroy(gameObject);
            return;
        }
    }
}
