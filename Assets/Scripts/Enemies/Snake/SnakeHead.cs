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
            ComboFeed.theInstance.AddNewComboFeed("+ " + score.ToString() + " " + transform.name);
            snakeBody.takeDamage(PrimaryColor.OMNI, 1);
            Destroy(gameObject);
            return;
        }
    }
}
