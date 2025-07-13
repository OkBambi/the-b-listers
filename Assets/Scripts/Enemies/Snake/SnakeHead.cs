using UnityEngine;

public class SnakeHead : EnemyBase
{
    [SerializeField] Snake snakeBody;
    [SerializeField] TrailRenderer trail;


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
