using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static EasingLibrary;

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
                                GameManager.instance.colorSwapper.m2.color = Color.grey;
                                ComboManager.instance.RemoveScore(100);
                                ComboFeed.theInstance.AddNewComboFeed("- 100 redSchmove", -100);
                                StartCoroutine(UpdateCoolDownUIRed());
                                StartCoroutine(CooldownStartFlash(RedCD_UI));
                            }
                            break;
                        case PrimaryColor.BLUE:
                            if (cooldownBlue <= 0)
                            {
                                blueSchmover.Activate();
                                cooldownBlue = maxCooldownBlue;
                                GameManager.instance.colorSwapper.m2.color = Color.grey;
                                ComboManager.instance.RemoveScore(100);
                                ComboFeed.theInstance.AddNewComboFeed("- 100 blueSchmove", -100);
                                StartCoroutine(UpdateCoolDownUIBlue());
                                StartCoroutine(CooldownStartFlash(BlueCD_UI));
                            }
                            break;
                        default:
                            if (cooldownYel <= 0)
                            {
                                yellowSchmover.Activate();
                                cooldownYel = maxCooldownYel;
                                //YellowCD_M2.color = Color.gray;
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
        AudioManager.instance.Play("Invalid");
        yield return new WaitForSeconds(0.4f);
        invalidScore.SetActive(false);
    }

    IEnumerator DisabledFlash()
    {
        disabledSchmoves.SetActive(true);
        AudioManager.instance.Play("Invalid");
        yield return new WaitForSeconds(0.4f);
        disabledSchmoves.SetActive(false);
    }

    [Header("Informational UI")]
    [SerializeField] GameObject invalidScore;
    [SerializeField] GameObject disabledSchmoves;

    //UI
    [Header("CoolDownBars")]
    [SerializeField] Image RedCD_UI;
    public Image YellowCD_UI;
    [SerializeField] Image BlueCD_UI;

    [Header("CoolDownLerps")]
    public float RedCD;
    public float YellowCD;
    public float BlueCD;

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
        AudioManager.instance.Play("Fizzle");
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
                AudioManager.instance.Stop("Fizzle");
                break;
            }

            RedCD = 50f + (200f * (cooldownRed / maxCooldownRed));
            //size.x = EaseOutBack(size.x, RedCD, 0.1f);
            RedCD_UI.rectTransform.sizeDelta = new Vector2(EaseOutBack(RedCD_UI.rectTransform.sizeDelta.x, RedCD, 0.1f), RedCD_UI.rectTransform.sizeDelta.y);

            if (RedCD == 50)
            {
                Debug.Log("break out");
                break;
            }

            yield return new WaitForFixedUpdate();
        } while (true);

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator UpdateCoolDownUIYellow()
    {
        AudioManager.instance.Play("Fizzle");
        bool yellowIsCD = false;
        //Vector2 size = new Vector2(50f, 34.41f);

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
                AudioManager.instance.Stop("Fizzle");
                break;
            }

            YellowCD = 50f + (200f * (cooldownYel / maxCooldownYel));

            YellowCD_UI.rectTransform.sizeDelta = new Vector2(EaseOutBack(YellowCD_UI.rectTransform.sizeDelta.x, YellowCD, 0.1f), YellowCD_UI.rectTransform.sizeDelta.y);

            if (YellowCD == 50)
            {
                Debug.Log("break out");
                break;
            }

            yield return new WaitForFixedUpdate();
        } while (true);

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator UpdateCoolDownUIBlue()
    {
        AudioManager.instance.Play("Fizzle");
        bool blueIsCD = false;
        //Vector2 size = new Vector2(50f, 34.41f);

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
                AudioManager.instance.Stop("Fizzle");
                break;
            }

            BlueCD = 50f + (200f * (cooldownBlue / maxCooldownBlue));
            //size.x = EaseOutBack(size.x, BlueCD, 0.1f);
            //BlueCD_UI.rectTransform.sizeDelta = size;
            BlueCD_UI.rectTransform.sizeDelta = new Vector2(EaseOutBack(BlueCD_UI.rectTransform.sizeDelta.x, BlueCD, 0.1f), BlueCD_UI.rectTransform.sizeDelta.y);

            if (BlueCD == 50)
            {
                Debug.Log("break out");
                break;
            }

            yield return new WaitForFixedUpdate();
        } while (true);

        yield return new WaitForFixedUpdate();
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

        bool isPlayedSound = true;
        StartCoroutine(CameraShake.instance.Shake(0.5f, 0.08f));
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
                //rectX = Mathf.Lerp(rectX, 400f, finishSpeed * 2);
                //rectY = Mathf.Lerp(rectY, 3f, finishSpeed / 2);

                rectX = EaseOutBack(rectX, 600f, finishSpeed);
                rectY = EaseOutBack(rectY, 3f, finishSpeed);
                if (isPlayedSound)
                {
                    isPlayedSound = false;
                    AudioManager.instance.Play("Ding");
                }
            }
            else
            {
                rectX = EaseOutBack(rectX, 50f, finishSpeed);
                rectY = EaseOutBack(rectY, 34.41f, finishSpeed);

                //rectX = Mathf.Lerp(rectX, 50f, finishSpeed);
                //rectY = Mathf.Lerp(rectY, 34.41f, finishSpeed / 2);
            }

            colourBar.rectTransform.sizeDelta = new Vector2(rectX, rectY);
            ++currentDuration;
            yield return new WaitForFixedUpdate();
        }
        Debug.Log(originalColour);
        colourBar.color = originalColour;


        yield return new WaitForFixedUpdate();
    }

    public IEnumerator CooldownStartFlash(Image colourBar)
    {
        Color originalColour = colourBar.color;
        colourBar.color = Color.white;
        yield return new WaitForSecondsRealtime(0.2f);
        colourBar.color = originalColour;
        //int animationPhase = 0;
        //int currentDuration = 0;
        //float rectX = 50f;
        //float rectY = 34.41f;

        //Color originalColour = colourBar.color;
        //colourBar.color = Color.black;

        //switch (colourBar.name)
        //{
        //    case "Red":
        //        RedCD_M2.color = Color.white;
        //        break;
        //    case "Yellow":
        //        YellowCD_M2.color = Color.white;
        //        break;
        //    case "Blue":
        //        BlueCD_M2.color = Color.white;
        //        break;
        //}

        //while (true)
        //{
        //    if (animationPhase == 2) break;
        //    if (currentDuration >= animationDurations[animationPhase])
        //    {
        //        currentDuration = 0;
        //        ++animationPhase;
        //    }

        //    if (animationPhase == 0)
        //    {
        //        //rectX = Mathf.Lerp(rectX, 400f, finishSpeed * 2);
        //        //rectY = Mathf.Lerp(rectY, 3f, finishSpeed / 2);

        //        rectX = EaseOutBack(rectX, 600f, finishSpeed);
        //        rectY = EaseOutBack(rectY, 3f, finishSpeed);
        //    }
        //    else
        //    {
        //        rectX = EaseOutBack(rectX, 50f, finishSpeed);
        //        rectY = EaseOutBack(rectY, 34.41f, finishSpeed);

        //        //rectX = Mathf.Lerp(rectX, 50f, finishSpeed);
        //        //rectY = Mathf.Lerp(rectY, 34.41f, finishSpeed / 2);
        //    }

        //    colourBar.rectTransform.sizeDelta = new Vector2(rectX, rectY);
        //    ++currentDuration;
        //    yield return new WaitForFixedUpdate();
        //}
        //Debug.Log(originalColour);
        //colourBar.color = originalColour;




    }
    #endregion


}
