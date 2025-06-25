using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static EasingLibrary;

public class ColorSwapping : MonoBehaviour
{

    [SerializeField] GameObject red_m2;
    [SerializeField] GameObject yellow_m2;
    [SerializeField] GameObject blue_m2;
    [SerializeField] Image screenFlash;
    [SerializeField] float screenFlashDuration;
    [Range(0f, 1f)] [SerializeField] float screenFlashSpeed;
    public void UpdateColor(ref PrimaryColor playerColor)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && playerColor != PrimaryColor.RED)
        {
            playerColor = PrimaryColor.RED;
            red_m2.SetActive(true);
            yellow_m2.SetActive(false);
            blue_m2.SetActive(false);
            screenFlash.color = Color.red;
            StartCoroutine(FlashScreenColour());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && playerColor != PrimaryColor.YELLOW)
        {
            playerColor = PrimaryColor.YELLOW;
            red_m2.SetActive(false);
            yellow_m2.SetActive(true);
            blue_m2.SetActive(false);
            screenFlash.color = Color.yellow;
            StartCoroutine(FlashScreenColour());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && playerColor != PrimaryColor.BLUE)
        {
            playerColor = PrimaryColor.BLUE;
            red_m2.SetActive(false);
            yellow_m2.SetActive(false);
            blue_m2.SetActive(true);
            screenFlash.color = Color.blue;
            StartCoroutine(FlashScreenColour());
        }
        
    }


    IEnumerator FlashScreenColour()
    {
        screenFlash.gameObject.SetActive(true);
        Color fadingColour = screenFlash.color;
        float changingAlpha = 0f;
        float timer = 0;
        while(timer < screenFlashDuration / 4f)
        {
            changingAlpha = EasingLibrary.EaseInExpo(changingAlpha, 1f, screenFlashSpeed);
            fadingColour = new Color(fadingColour.r, fadingColour.g, fadingColour.b, changingAlpha);
            screenFlash.color = fadingColour;
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0;
        while (timer < screenFlashDuration * 3f / 4f)
        {
            changingAlpha = EasingLibrary.EaseInCubic(changingAlpha, 0f, screenFlashSpeed / 2f);
            fadingColour = new Color(fadingColour.r, fadingColour.g, fadingColour.b, changingAlpha);
            screenFlash.color = fadingColour;
            timer += Time.deltaTime;
            yield return null;
        }
        screenFlash.gameObject.SetActive(false);
        yield return null;
    }
}
