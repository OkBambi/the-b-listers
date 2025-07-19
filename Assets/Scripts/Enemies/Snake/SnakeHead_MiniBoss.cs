using UnityEngine;
using System.Collections.Generic;

public class SnakeHead_MiniBoss : SnakeHead
{
    public GameObject killBox;
    public Collider col;
    public int maxHp;
    [SerializeField] GameObject snakeOrb;
    [SerializeField] public List<GameObject> orbs;


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
            maxHp = maxHp / 2;
        }
        hp = maxHp;
    }

    public override void DeathCheck()
    {
        if (hp <= 0)
        {
            for (int orbNumber = 0; orbNumber < 3; ++orbNumber)
            {
                GameObject newOrb = Instantiate(snakeOrb, transform.position, Quaternion.identity);
                orbs.Add(newOrb);
                newOrb.GetComponent<SnakeOrb>().parentHead = this;
            }
            

            col.enabled = false;
            killBox.SetActive(false);
            model.gameObject.SetActive(false);
            trail.enabled = false;
            return;
        }
    }

    public void Death()
    {
        gameObject.SetActive(false);
        isAlive = false;
        snakeBody.takeDamage(PrimaryColor.OMNI, 1);
        Destroy(gameObject);
    }
}
