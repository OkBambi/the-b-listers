using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Renderer model;
    public PrimaryColor setColor;
    [Space]
    public int hp;
    public int score = 50;

    string nameStr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ColorSelection(setColor);
    }

    protected void ColorSelection(PrimaryColor newColor)
    {
        setColor = newColor;
        switch (setColor)
        {
            case PrimaryColor.RED:
                model.material.color = Color.red;
                nameStr = "Red";
                break;
            case PrimaryColor.YELLOW:
                model.material.color = Color.yellow;
                nameStr = "Yellow";
                break;
            case PrimaryColor.BLUE:
                model.material.color = Color.blue;
                nameStr = "Blue";
                break;
            case PrimaryColor.OMNI:
            default:
                model.material.color = Color.black;
                nameStr = "Omni";
                break;
        }
    }
}
