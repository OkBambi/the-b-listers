using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

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
            if (!LevelModifierManager.instance.daggersOnly)
            {
                if (ComboManager.instance.GetScore() >= 100)
                {
                    switch (playerColor)
                    {
                        case PrimaryColor.RED:
                            if (cooldownRed <= 0)
                            {
                                redSchmover.Activate();
                                cooldownRed = maxCooldownRed;
                                RedCD_M2.color = Color.gray;
                                ComboManager.instance.RemoveScore(100);
                                ComboFeed.theInstance.AddNewComboFeed("- 100 redSchmove", 100);
                                StartCoroutine(UpdateCoolDownUIRed());
                            }
                            break;
                        case PrimaryColor.BLUE:
                            if (cooldownBlue <= 0)
                            {
                                blueSchmover.Activate();
                                cooldownBlue = maxCooldownBlue;
                                BlueCD_M2.color = Color.gray;
                                ComboManager.instance.RemoveScore(100);
                                ComboFeed.theInstance.AddNewComboFeed("- 100 blueSchmove", 100);
                                StartCoroutine(UpdateCoolDownUIBlue());
                            }
                            break;
                        default:
                            if (cooldownYel <= 0)
                            {
                                yellowSchmover.Activate();
                                cooldownYel = maxCooldownYel;
                                YellowCD_M2.color = Color.gray;
                                //the coroutine starts when you release the railgun
                            }
                            break;
                    }
                }
                else
                {
                    //invalid score
                    StartCoroutine(InvalidFlash());
                }
            }
            else
            {
                //level modifier diable
                StartCoroutine(DisabledFlash());
            }
        }
    }

    IEnumerator InvalidFlash()
    {
        invalidScore.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        invalidScore.SetActive(false);
    }

    IEnumerator DisabledFlash()
    {
        disabledSchmoves.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        disabledSchmoves.SetActive(false);
    }

    [Header("Informational UI")]
    [SerializeField] GameObject invalidScore;
    [SerializeField] GameObject disabledSchmoves;

    //UI
    [Header("CoolDownBars")]
    [SerializeField] Image RedCD_UI;
    [SerializeField] Image YellowCD_UI;
    [SerializeField] Image BlueCD_UI;

    [Header("CoolDownLerps")]
    [SerializeField] float RedCD;
    [SerializeField] float YellowCD;
    [SerializeField] float BlueCD;

    [Header("CoolDownText")]
    [SerializeField] TextMeshProUGUI RedCD_M2;
    [SerializeField] TextMeshProUGUI YellowCD_M2;
    [SerializeField] TextMeshProUGUI BlueCD_M2;

    [Header("CoolDownFinish")]
    [SerializeField] float finishSpeed;
    [SerializeField] List<int> animationDurations;

    private void Start()
    {
        RedCD = RedCD_UI.rectTransform.sizeDelta.x;
        YellowCD = YellowCD_UI.rectTransform.sizeDelta.x;
        BlueCD = BlueCD_UI.rectTransform.sizeDelta.x;
    }

    #region Animations
    public IEnumerator UpdateCoolDownUIRed()
    {
        bool redIsCD = false;

        do
        {
            if (cooldownRed > 0)
            {
                cooldownRed = Mathf.Clamp(cooldownRed - Time.deltaTime, 0f, 100f);
                redIsCD = true;
            }


            if (redIsCD && cooldownRed == 0)
            {
                redIsCD = false;
                Debug.Log("twice?");
                StartCoroutine(CooldownComplete(RedCD_UI));
            }

            RedCD = 50f + (200f * (cooldownRed / maxCooldownRed));
            RedCD_UI.rectTransform.sizeDelta = new Vector2(RedCD, 34.41f);

            if (RedCD_UI.rectTransform.sizeDelta.x == 50)
            {
                Debug.Log("break out");
                break;
            }

            yield return null;
        } while (true);

        yield return null;
    }

    public IEnumerator UpdateCoolDownUIYellow()
    {
        bool yellowIsCD = false;

        do
        {
            if (cooldownYel > 0)
            {
                cooldownYel = Mathf.Clamp(cooldownYel - Time.deltaTime, 0, 100);
                yellowIsCD = true;
            }

            if (yellowIsCD && cooldownYel == 0)
            {
                yellowIsCD = false;
                StartCoroutine(CooldownComplete(YellowCD_UI));
            }

            YellowCD = 50f + (200f * (cooldownYel / maxCooldownYel));
            YellowCD_UI.rectTransform.sizeDelta = new Vector2(YellowCD, 34.41f);

            if (YellowCD_UI.rectTransform.sizeDelta.x == 50)
            {
                Debug.Log("break out");
                break;
            }

            yield return null;
        } while (true);

        yield return null;
    }

    public IEnumerator UpdateCoolDownUIBlue()
    {
        bool blueIsCD = false;


        do
        {
            if (cooldownBlue > 0)
            {
                cooldownBlue = Mathf.Clamp(cooldownBlue - Time.deltaTime, 0, 100);
                blueIsCD = true;
            }

            if (blueIsCD && cooldownBlue == 0)
            {
                blueIsCD = false;
                StartCoroutine(CooldownComplete(BlueCD_UI));
            }

            BlueCD = 50f + (200f * (cooldownBlue / maxCooldownBlue));
            BlueCD_UI.rectTransform.sizeDelta = new Vector2(BlueCD, 34.41f);

            if (BlueCD_UI.rectTransform.sizeDelta.x == 50)
            {
                Debug.Log("break out");
                break;
            }

            yield return null;
        } while (true);

        yield return null;
    }

    IEnumerator CooldownComplete(Image colourBar)
    {
        int animationPhase = 0;
        int currentDuration = 0;
        float rectX = 50f;
        float rectY = 34.41f;

        Color originalColour = colourBar.color;
        colourBar.color = Color.black;

        switch (colourBar.name)
        {
            case "Red":
                RedCD_M2.color = Color.white;
                break;
            case "Yellow":
                YellowCD_M2.color = Color.white;
                break;
            case "Blue":
                BlueCD_M2.color = Color.white;
                break;
        }

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

            colourBar.rectTransform.sizeDelta = new Vector2(rectX, rectY);
            ++currentDuration;
            yield return null;
        }
        Debug.Log(originalColour);
        colourBar.color = originalColour;

        

        yield return null;
    }
    #endregion


}
