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
        if (Input.GetKeyDown(KeyCode.Alpha1) && playerColor != PrimaryColor.RED)
            SwapToColour(playerColor, PrimaryColor.RED, ref playerColor);

        else if (Input.GetKeyDown(KeyCode.Alpha2) && playerColor != PrimaryColor.YELLOW)
            SwapToColour(playerColor, PrimaryColor.YELLOW, ref playerColor);

        else if (Input.GetKeyDown(KeyCode.Alpha3) && playerColor != PrimaryColor.BLUE)
            SwapToColour(playerColor, PrimaryColor.BLUE, ref playerColor);

        else if (Input.GetKeyDown(KeyCode.E))
        {
            PrimaryColor finalColour = playerColor;
            ++finalColour;
            if (finalColour == PrimaryColor.OMNI)
            {
                finalColour = PrimaryColor.RED;
            }
            SwapToColour(playerColor, finalColour, ref playerColor);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            PrimaryColor finalColour = playerColor;
            if (finalColour == PrimaryColor.RED)
                finalColour = PrimaryColor.BLUE;
            else
                --finalColour;
            SwapToColour(playerColor, finalColour, ref playerColor);
        }
    }

    public void SwapToColour(PrimaryColor currentColour, PrimaryColor finalColour, ref PrimaryColor playerColor)
    {
        switch (currentColour)
        {
            case PrimaryColor.RED:
                switch (finalColour)
                {
                    case PrimaryColor.BLUE:
                        StartCoroutine(MoveTint(currentTint, true));
                        ProcessCurrentColour(ref finalColour);
                        SetUpCurrentTint(true);
                        StartCoroutine(MoveTint(currentTint, true));
                        playerColor = PrimaryColor.BLUE;
                        break;
                    case PrimaryColor.YELLOW:
                        StartCoroutine(MoveTint(currentTint, false));
                        ProcessCurrentColour(ref finalColour);
                        SetUpCurrentTint(false);
                        StartCoroutine(MoveTint(currentTint, false));
                        playerColor = PrimaryColor.YELLOW;
                        break;
                }
                break;

            case PrimaryColor.YELLOW:
                switch (finalColour)
                {
                    case PrimaryColor.RED:
                        StartCoroutine(MoveTint(currentTint, true));
                        ProcessCurrentColour(ref finalColour);
                        SetUpCurrentTint(true);
                        StartCoroutine(MoveTint(currentTint, true));
                        playerColor = PrimaryColor.RED;
                        break;
                    case PrimaryColor.BLUE:
                        StartCoroutine(MoveTint(currentTint, false));
                        ProcessCurrentColour(ref finalColour);
                        SetUpCurrentTint(false);
                        StartCoroutine(MoveTint(currentTint, false));
                        playerColor = PrimaryColor.BLUE;
                        break;
                }
                break;

            case PrimaryColor.BLUE:
                switch (finalColour)
                {
                    case PrimaryColor.RED:
                        StartCoroutine(MoveTint(currentTint, false));
                        ProcessCurrentColour(ref finalColour);
                        SetUpCurrentTint(false);
                        StartCoroutine(MoveTint(currentTint, false));
                        playerColor = PrimaryColor.RED;
                        break;
                    case PrimaryColor.YELLOW:
                        StartCoroutine(MoveTint(currentTint, true));
                        ProcessCurrentColour(ref finalColour);
                        SetUpCurrentTint(true);
                        StartCoroutine(MoveTint(currentTint, true));
                        playerColor = PrimaryColor.YELLOW;
                        break;
                }
                break;
        }
    }
    

    private void SetUpCurrentTint(bool isGoingRight)
    {
        if (isGoingRight)
            currentTint.localPosition = new Vector3(-1920, 0, 0);
        else
            currentTint.localPosition = new Vector3(1920, 0, 0);
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
                break;

            case PrimaryColor.YELLOW:
                red_m2.SetActive(false);
                yellow_m2.SetActive(true);
                blue_m2.SetActive(false);
                currentTint = yellowTint;
                break;

            case PrimaryColor.BLUE:
                red_m2.SetActive(false);
                yellow_m2.SetActive(false);
                blue_m2.SetActive(true);
                currentTint = blueTint;
                break;
        }
        EnemyManager.instance.TriggerStopwatch(); // Trigger the stopwatch event when color changes
    }
}
