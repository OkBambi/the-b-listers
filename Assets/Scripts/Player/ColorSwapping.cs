using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static EasingLibrary;

public class ColorSwapping : MonoBehaviour
{

    [SerializeField] GameObject red_m2;
    [SerializeField] GameObject yellow_m2;
    [SerializeField] GameObject blue_m2;
    [SerializeField] RectTransform redTint;
    [SerializeField] RectTransform yellowTint;
    [SerializeField] RectTransform blueTint;
    [SerializeField] RectTransform currentTint;
    //[SerializeField] float screenFlashDuration;
    [Range(0f, 1f)] [SerializeField] float screenFlashSpeed;
    public void UpdateColor(ref PrimaryColor playerColor)
    {
        bool isRight = true;
        if (Input.GetKeyDown(KeyCode.Alpha1) && playerColor != PrimaryColor.RED)
        {
            switch (playerColor)
            {
                case PrimaryColor.YELLOW:
                    StartCoroutine(MoveTint(currentTint, false));
                    isRight = false;
                    break;
                case PrimaryColor.BLUE:
                    StartCoroutine(MoveTint(currentTint, true));
                    isRight = true;
                    break;
            }

            playerColor = PrimaryColor.RED;
            ProcessCurrentColour(ref playerColor);
            SetUpCurrentTint(isRight);
            StartCoroutine(MoveTint(currentTint, isRight));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && playerColor != PrimaryColor.YELLOW)
        {
            switch (playerColor)
            {
                case PrimaryColor.RED:
                    StartCoroutine(MoveTint(currentTint, true));
                    isRight = true;
                    break;
                case PrimaryColor.BLUE:
                    StartCoroutine(MoveTint(currentTint, false));
                    isRight = false;
                    break;
            }

            playerColor = PrimaryColor.YELLOW;
            ProcessCurrentColour(ref playerColor);
            SetUpCurrentTint(isRight);
            StartCoroutine(MoveTint(currentTint, isRight));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && playerColor != PrimaryColor.BLUE)
        {
            switch (playerColor)
            {
                case PrimaryColor.RED:
                    StartCoroutine(MoveTint(currentTint, false));
                    isRight = false;
                    break;
                case PrimaryColor.YELLOW:
                    StartCoroutine(MoveTint(currentTint, true));
                    isRight = true;
                    break;
            }

            playerColor = PrimaryColor.BLUE;
            ProcessCurrentColour(ref playerColor);
            SetUpCurrentTint(isRight);
            StartCoroutine(MoveTint(currentTint, isRight));

        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(MoveTint(currentTint, false));
            ++playerColor;
            Debug.Log(playerColor);
            if (playerColor == PrimaryColor.OMNI)
            {
                playerColor = PrimaryColor.RED;
            }
            ProcessCurrentColour(ref playerColor);
            SetUpCurrentTint(false);
            StartCoroutine(MoveTint(currentTint, false));
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(MoveTint(currentTint, true));
            if (playerColor == PrimaryColor.RED)
            {
                playerColor = PrimaryColor.BLUE;
            }
            else
            {
                --playerColor;
            }
            ProcessCurrentColour(ref playerColor);
            SetUpCurrentTint(true);
            StartCoroutine(MoveTint(currentTint, true));
        }

    }


    //IEnumerator FlashScreenColour()
    //{
    //    screenFlash.gameObject.SetActive(true);
    //    Color fadingColour = screenFlash.color;
    //    float changingAlpha = 0f;
    //    float timer = 0;
    //    while(timer < screenFlashDuration / 4f)
    //    {
    //        changingAlpha = EasingLibrary.EaseInExpo(changingAlpha, 1f, screenFlashSpeed);
    //        fadingColour = new Color(fadingColour.r, fadingColour.g, fadingColour.b, changingAlpha);
    //        screenFlash.color = fadingColour;
    //        timer += Time.deltaTime;
    //        yield return null;
    //    }
    //    timer = 0;
    //    while (timer < screenFlashDuration * 3f / 4f)
    //    {
    //        changingAlpha = EasingLibrary.EaseInCubic(changingAlpha, 0f, screenFlashSpeed / 2f);
    //        fadingColour = new Color(fadingColour.r, fadingColour.g, fadingColour.b, changingAlpha);
    //        screenFlash.color = fadingColour;
    //        timer += Time.deltaTime;
    //        yield return null;
    //    }
    //    screenFlash.gameObject.SetActive(false);
    //    yield return null;
    //}

    //private void FlashSwipeColour(PrimaryColor currentColour, PrimaryColor finalColour)
    //{
    //    RectTransform currentTint;
    //    RectTransform finalTint;
    //    switch (currentColour)
    //    {
    //        case PrimaryColor.RED:
    //            currentTint = redTint;
    //            break;
    //        case PrimaryColor.YELLOW:
    //            currentTint = yellowTint;
    //            break;
    //        case PrimaryColor.BLUE:
    //            currentTint = blueTint;
    //            break;
    //    }
    //}

    private void SetUpCurrentTint(bool isGoingRight)
    {
        if (isGoingRight)
        {
            currentTint.localPosition = new Vector3(-1920, 0, 0);
        }
        else
        {
            currentTint.localPosition = new Vector3(1920, 0, 0);
        }
        
    }

    IEnumerator MoveTint(RectTransform tint, bool isGoingRight)
    {
        float oldX = tint.localPosition.x;
        Debug.Log(oldX);
        float finalX = oldX + (isGoingRight ? 1920 : -1920);
        Debug.Log(finalX);
        float currX = oldX;
        Debug.Log(currX);
        int counter = 0;
        //Mathf.Approximately(currX, finalX)
        while (counter <= 100)
        {
            if (isGoingRight)
                currX = EaseInCubic(currX, finalX, screenFlashSpeed);
            else
                currX = EaseInCubic(currX, finalX, screenFlashSpeed);
            tint.localPosition = new Vector3(currX, tint.localPosition.y, tint.localPosition.z);
            ++counter;
            yield return null;
        }
        tint.localPosition = new Vector3(finalX, tint.localPosition.y, tint.localPosition.z);
        yield return null;
    }

    private void ProcessCurrentColour(ref PrimaryColor playerColour)
    {
        switch (playerColour)
        {
            case PrimaryColor.RED:
                red_m2.SetActive(true);
                yellow_m2.SetActive(false);
                blue_m2.SetActive(false);
                currentTint = redTint;
                //screenFlash.color = Color.red;
                //StartCoroutine(FlashScreenColour());
                break;
            case PrimaryColor.YELLOW:
                red_m2.SetActive(false);
                yellow_m2.SetActive(true);
                blue_m2.SetActive(false);
                currentTint = yellowTint;
                //screenFlash.color = Color.yellow;
                //StartCoroutine(FlashScreenColour());
                break;
            case PrimaryColor.BLUE:
                red_m2.SetActive(false);
                yellow_m2.SetActive(false);
                blue_m2.SetActive(true);
                currentTint = blueTint;
                //screenFlash.color = Color.blue;
                //StartCoroutine(FlashScreenColour());
                break;
        }
        EnemyManager.instance.TriggerStopwatch(); // Trigger the stopwatch event when color changes


    }
}
