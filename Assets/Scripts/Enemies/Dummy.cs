using System.Collections;
using UnityEngine;

public class Dummy : MonoBehaviour, IDamage
{

    [SerializeField] Renderer model;
    [SerializeField] PrimaryColor setColor;
    [Space]
    [SerializeField] int hp;

    Color colorOriginal;
    string nameStr;

    void Start()
    {
        switch(setColor)
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

        colorOriginal = model.material.color;
        transform.name = nameStr + " Dummy";
    }


    public void takeDamage(PrimaryColor hitColor, int amount)
    {
        if(hitColor == setColor || hitColor == PrimaryColor.OMNI || setColor == PrimaryColor.OMNI)
        {
            hp -= amount;

            if (hp <= 0)
            {
                //FALL.
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(FlashWhite());
            }
        }
    }

    IEnumerator FlashWhite()
    {
        model.material.color = Color.white;
        yield return new WaitForSeconds(0.08f);
        model.material.color = colorOriginal;
    }
}
