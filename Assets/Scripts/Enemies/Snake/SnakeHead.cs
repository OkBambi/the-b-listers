using UnityEngine;

public class SnakeHead : EnemyBase
{
    [SerializeField] Snake snakeBody;
    [SerializeField] TrailRenderer trail;


    protected virtual void Start()
    {
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
