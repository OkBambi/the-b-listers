using System.Collections;
using UnityEngine;

public class Schmoves : MonoBehaviour
{
    [Header("Red Schmove")] // Hookshot slam
    public float cooldownRed;
    public float maxCooldownRed;
    [SerializeField] RedSchmove redSchmover;

    [Header("Yellow Schmove")] // Railgun
    public float cooldownYel;
    public float maxCooldownYel;
    [SerializeField] float chargeTime;
    [SerializeField] float slowMod;
    [SerializeField] float railDist;
    [SerializeField] int railgunDmg;
    [SerializeField] YellowSchmove yellowSchmover;

    [Header("Blue Schmove")] // Pulse Charge
    public float cooldownBlue;
    public float maxCooldownBlue;
    [SerializeField] BlueSchmove blueSchmover;

    public void UpdateInput(PrimaryColor playerColor)
    {
        if (Input.GetMouseButtonDown(1))
        {
            switch(playerColor)
            {
                case PrimaryColor.RED:
                    if (cooldownRed <= 0)
                    {
                        redSchmover.Activate();
                        cooldownRed = maxCooldownRed;
                        StartCoroutine(UpdateCoolDownUI());
                    }
                    break;
                case PrimaryColor.BLUE:
                    if (cooldownBlue <= 0)
                    {
                        blueSchmover.Activate();
                        cooldownBlue = maxCooldownBlue;
                        StartCoroutine(UpdateCoolDownUI());
                    }
                    break;
                default:
                    if (cooldownYel <= 0)
                    {
                        yellowSchmover.Activate();
                        cooldownYel = maxCooldownYel;
                        //set the cooldown meter as the charge starts
                        YellowCD = 50f + (200f * (cooldownYel / maxCooldownYel));
                        YellowCD_UI.sizeDelta = new Vector3(YellowCD, 34.41f);
                        //the coroutine starts when you release the railgun
                    }
                    break;

            }
        }
    }

    //UI
    [Header("CoolDownBars")]
    [SerializeField] RectTransform RedCD_UI;
    [SerializeField] RectTransform YellowCD_UI;
    [SerializeField] RectTransform BlueCD_UI;

    [Header("CoolDownLerps")]
    [SerializeField] float RedCD;
    [SerializeField] float YellowCD;
    [SerializeField] float BlueCD;

    private bool isUpdating;

    private void Awake()
    {
        RedCD = RedCD_UI.localScale.x;
        YellowCD = YellowCD_UI.localScale.x;
        BlueCD = BlueCD_UI.localScale.x;
    }

    public IEnumerator UpdateCoolDownUI()
    {
        if (isUpdating) yield return null;
        do
        {
            isUpdating = true;
            if (cooldownRed > 0)
            {
                cooldownRed = Mathf.Clamp(cooldownRed - Time.deltaTime, 0f, 100f);
            }
            if (cooldownBlue > 0)
            {
                cooldownBlue = Mathf.Clamp(cooldownBlue - Time.deltaTime, 0, 100);
            }
            if (cooldownYel > 0)
            {
                cooldownYel = Mathf.Clamp(cooldownYel - Time.deltaTime, 0, 100);
            }

            RedCD = 50f + (200f * (cooldownRed / maxCooldownRed));
            RedCD_UI.sizeDelta = new Vector3(RedCD, 34.41f);

            YellowCD = 50f + (200f * (cooldownYel / maxCooldownYel));
            YellowCD_UI.sizeDelta = new Vector3(YellowCD, 34.41f);

            BlueCD = 50f + (200f * (cooldownBlue / maxCooldownBlue));
            BlueCD_UI.sizeDelta = new Vector3(BlueCD, 34.41f);

            yield return null;
        } while (RedCD_UI.sizeDelta.x > 50 || YellowCD_UI.sizeDelta.x > 50 || BlueCD_UI.sizeDelta.x > 50);
        isUpdating = false;
    }
}
