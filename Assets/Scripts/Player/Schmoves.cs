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

    [Header("Blue Schmove")] // Pulse Charge
    [SerializeField] float cooldownBlue;
    [SerializeField] float blueWindup;
    [SerializeField] float timeBetweenPulses;
    [SerializeField] float pulseRadius;

    public void UpdateInput(PrimaryColor playerColor)
    {
        if (Input.GetMouseButtonDown(1))
        {
            switch(playerColor)
            {
                case PrimaryColor.RED:
                    redSchmover.Activate();
                    break;
                default:
                    redSchmover.Activate();
                    break;
            }
        }
    }
}
