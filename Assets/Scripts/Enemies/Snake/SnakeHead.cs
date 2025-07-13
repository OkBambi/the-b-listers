using UnityEngine;

public class SnakeHead : EnemyBase
{
    [SerializeField] protected Snake snakeBody;
    [SerializeField] public TrailRenderer trail;


    protected override void Start()
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

        name = "Snake Head";
        if (LevelModifierManager.instance.smallFastEnemies)
            model.transform.localScale = model.transform.localScale * 0.75f;

        if (LevelModifierManager.instance.lessHealth)
        {
            hp = hp / 2;
        }
    }

    public override void DeathCheck()
    {
        if (hp <= 0)
        {
            isAlive = false;
            float scoreWithMult = ComboManager.instance.getScoreTimesMult(score);
            ComboManager.instance.AddScore(score);
            ComboFeed.theInstance.AddNewComboFeed("+ " + scoreWithMult.ToString() + " " + transform.name, scoreWithMult);
            snakeBody.takeDamage(PrimaryColor.OMNI, 1);
            Destroy(gameObject);
            return;
        }
    }
}
