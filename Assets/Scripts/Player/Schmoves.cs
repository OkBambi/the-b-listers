using UnityEngine;

public class Schmoves : MonoBehaviour
{
    [Header("Red Schmove")] // Hookshot slam
    [SerializeField] float cooldownRed;
    [SerializeField] RedSchmove redSchmover;

    [Header("Yellow Schmove")] // Railgun
    [SerializeField] float cooldownYel;
    [SerializeField] float chargeTime;
    [SerializeField] float slowMod;
    [SerializeField] float railDist;
    [SerializeField] int railgunDmg;
    [SerializeField] YellowSchmove yellowSchmover;

    [Header("Blue Schmove")] // Pulse Charge
    [SerializeField] float cooldownBlue;
    [SerializeField] BlueSchmove blueSchmover;

    public void UpdateInput(PrimaryColor playerColor)
    {
        if (Input.GetMouseButtonDown(1))
        {
            switch(playerColor)
            {
                case PrimaryColor.RED:
                    redSchmover.Activate();
                    break;
                case PrimaryColor.BLUE:
                    blueSchmover.Activate();
                    break;
                default:
                    yellowSchmover.Activate();
                    break;

            }
        }
    }
}
