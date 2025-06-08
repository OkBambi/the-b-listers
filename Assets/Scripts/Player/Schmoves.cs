using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [Header("CoolDownFinish")]
    [SerializeField] float finishSpeed;
    [SerializeField] List<int> animationDurations;

    private void Awake()
    {
        RedCD = RedCD_UI.localScale.x;
        YellowCD = YellowCD_UI.localScale.x;
        BlueCD = BlueCD_UI.localScale.x;
    }

    public IEnumerator UpdateCoolDownUI()
    {
        bool redIsCD = false;
        bool yellowIsCD = false;
        bool blueIsCD = false;


        if (isUpdating) yield return null;

        do
        {
            isUpdating = true;
            if (cooldownRed > 0)
            {
                cooldownRed = Mathf.Clamp(cooldownRed - Time.deltaTime, 0f, 100f);
                redIsCD = true;
            }
            if (cooldownYel > 0)
            {
                cooldownYel = Mathf.Clamp(cooldownYel - Time.deltaTime, 0, 100);
                blueIsCD = true;
            }
            if (cooldownBlue > 0)
            {
                cooldownBlue = Mathf.Clamp(cooldownBlue - Time.deltaTime, 0, 100);
                yellowIsCD = true;
            }


            if (redIsCD && cooldownRed == 0)
            {
                redIsCD = false;
                StartCoroutine(CooldownCompleteRed());
            }

            if (yellowIsCD && cooldownYel == 0)
            {
                yellowIsCD = false;
                StartCoroutine(CooldownCompleteYellow());
            }

            if (blueIsCD && cooldownBlue == 0)
            {
                blueIsCD = false;
                StartCoroutine(CooldownCompleteBlue());
            }

            RedCD = 50f + (200f * (cooldownRed / maxCooldownRed));
            RedCD_UI.sizeDelta = new Vector2(RedCD, 34.41f);

            YellowCD = 50f + (200f * (cooldownYel / maxCooldownYel));
            YellowCD_UI.sizeDelta = new Vector2(YellowCD, 34.41f);

            BlueCD = 50f + (200f * (cooldownBlue / maxCooldownBlue));
            BlueCD_UI.sizeDelta = new Vector2(BlueCD, 34.41f);

            if (RedCD_UI.sizeDelta.x == 50 && YellowCD_UI.sizeDelta.x == 50 && BlueCD_UI.sizeDelta.x == 50)
                break;

            yield return null;
        } while (true);

        isUpdating = false;
        yield return null;
    }

    IEnumerator CooldownCompleteRed()
    {
        int animationPhase = 0;
        int currentDuration = 0;
        float rectX = 50f;
        float rectY = 34.41f;

        while (true)
        {
            if (animationPhase == 2) break;
            if (currentDuration >= animationDurations[animationPhase])
            {
                currentDuration = 0;
                ++animationPhase;
            }

            if (animationPhase == 0)
            {
                rectX = Mathf.Lerp(rectX, 400f, finishSpeed * 2);
                rectY = Mathf.Lerp(rectY, 3f, finishSpeed / 2);
            }
            else
            {
                rectX = Mathf.Lerp(rectX, 50f, finishSpeed);
                rectY = Mathf.Lerp(rectY, 34.41f, finishSpeed / 2);
            }

            RedCD_UI.sizeDelta = new Vector2(rectX, rectY);
            ++currentDuration;
            yield return null;
        }
        yield return null;
    }

    IEnumerator CooldownCompleteYellow()
    {
        int animationPhase = 0;
        int currentDuration = 0;
        float rectX = 50f;
        float rectY = 34.41f;

        while (true)
        {
            if (animationPhase == 2) break;
            if (currentDuration >= animationDurations[animationPhase])
            {
                currentDuration = 0;
                ++animationPhase;
            }

            if (animationPhase == 0)
            {
                rectX = Mathf.Lerp(rectX, 400f, finishSpeed * 2);
                rectY = Mathf.Lerp(rectY, 3f, finishSpeed / 2);
            }
            else
            {
                rectX = Mathf.Lerp(rectX, 50f, finishSpeed);
                rectY = Mathf.Lerp(rectY, 34.41f, finishSpeed / 2);
            }

            BlueCD_UI.sizeDelta = new Vector2(rectX, rectY);
            ++currentDuration;
            yield return null;
        }
        yield return null;
    }

    IEnumerator CooldownCompleteBlue()
    {
        int animationPhase = 0;
        int currentDuration = 0;
        float rectX = 50f;
        float rectY = 34.41f;

        while (true)
        {
            if (animationPhase == 2) break;
            if (currentDuration >= animationDurations[animationPhase])
            {
                currentDuration = 0;
                ++animationPhase;
            }

            if (animationPhase == 0)
            {
                rectX = Mathf.Lerp(rectX, 400f, finishSpeed * 2);
                rectY = Mathf.Lerp(rectY, 3f, finishSpeed / 2);
            }
            else
            {
                rectX = Mathf.Lerp(rectX, 50f, finishSpeed);
                rectY = Mathf.Lerp(rectY, 34.41f, finishSpeed / 2);
            }

            YellowCD_UI.sizeDelta = new Vector2(rectX, rectY);
            ++currentDuration;
            yield return null;
        }
        yield return null;
    }
}
