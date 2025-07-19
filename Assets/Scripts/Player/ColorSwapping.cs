using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EasingLibrary;

public class ColorSwapping : MonoBehaviour
{

    public TextMeshProUGUI m2;
    //[SerializeField] GameObject red_m2;
    //[SerializeField] GameObject yellow_m2;
    //[SerializeField] GameObject blue_m2;
    [SerializeField] RectTransform currentBar;

    [Header("CoolDownBars")]
    [SerializeField] Image RedCD_UI;
    public Image YellowCD_UI;
    [SerializeField] Image BlueCD_UI;

    [SerializeField] Image colourTint;
    //[SerializeField] RectTransform redTint;
    //[SerializeField] RectTransform yellowTint;
    //[SerializeField] RectTransform blueTint;
    //[SerializeField] RectTransform currentTint;

    [SerializeField] int currentAnimationFrames;
    [SerializeField] int framesPerAnimation;
    [SerializeField] Color currentColour;
    [SerializeField] Color endcolour;

    [SerializeField] float screenFlashDuration;
    [Range(0f, 1f)] [SerializeField] float screenFlashSpeed;
    public void UpdateColor(ref PrimaryColor playerColor)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && playerColor != PrimaryColor.RED)
            SwapToColour(PrimaryColor.RED, ref playerColor);

        else if (Input.GetKeyDown(KeyCode.Alpha2) && playerColor != PrimaryColor.YELLOW)
            SwapToColour(PrimaryColor.YELLOW, ref playerColor);

        else if (Input.GetKeyDown(KeyCode.Alpha3) && playerColor != PrimaryColor.BLUE)
            SwapToColour(PrimaryColor.BLUE, ref playerColor);

        else if (Input.GetKeyDown(KeyCode.E))
        {
            PrimaryColor finalColour = playerColor;
            ++finalColour;
            if (finalColour == PrimaryColor.OMNI)
            {
                finalColour = PrimaryColor.RED;
            }
            SwapToColour(finalColour, ref playerColor);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            PrimaryColor finalColour = playerColor;
            if (finalColour == PrimaryColor.RED)
                finalColour = PrimaryColor.BLUE;
            else
                --finalColour;
            SwapToColour(finalColour, ref playerColor);
        }
    }

    //public void SwapToColour(PrimaryColor currentColour, PrimaryColor finalColour, ref PrimaryColor playerColor)
    //{
    //    switch (currentColour)
    //    {
    //        case PrimaryColor.RED:
    //            switch (finalColour)
    //            {
    //                case PrimaryColor.BLUE:
    //                    StartCoroutine(MoveTint(currentTint, true));
    //                    ProcessCurrentColour(ref finalColour);
    //                    SetUpCurrentTint(true);
    //                    StartCoroutine(MoveTint(currentTint, true));
    //                    playerColor = PrimaryColor.BLUE;
    //                    break;
    //                case PrimaryColor.YELLOW:
    //                    StartCoroutine(MoveTint(currentTint, false));
    //                    ProcessCurrentColour(ref finalColour);
    //                    SetUpCurrentTint(false);
    //                    StartCoroutine(MoveTint(currentTint, false));
    //                    playerColor = PrimaryColor.YELLOW;
    //                    break;
    //            }
    //            break;

    //        case PrimaryColor.YELLOW:
    //            switch (finalColour)
    //            {
    //                case PrimaryColor.RED:
    //                    StartCoroutine(MoveTint(currentTint, true));
    //                    ProcessCurrentColour(ref finalColour);
    //                    SetUpCurrentTint(true);
    //                    StartCoroutine(MoveTint(currentTint, true));
    //                    playerColor = PrimaryColor.RED;
    //                    break;
    //                case PrimaryColor.BLUE:
    //                    StartCoroutine(MoveTint(currentTint, false));
    //                    ProcessCurrentColour(ref finalColour);
    //                    SetUpCurrentTint(false);
    //                    StartCoroutine(MoveTint(currentTint, false));
    //                    playerColor = PrimaryColor.BLUE;
    //                    break;
    //            }
    //            break;

    //        case PrimaryColor.BLUE:
    //            switch (finalColour)
    //            {
    //                case PrimaryColor.RED:
    //                    StartCoroutine(MoveTint(currentTint, false));
    //                    ProcessCurrentColour(ref finalColour);
    //                    SetUpCurrentTint(false);
    //                    StartCoroutine(MoveTint(currentTint, false));
    //                    playerColor = PrimaryColor.RED;
    //                    break;
    //                case PrimaryColor.YELLOW:
    //                    StartCoroutine(MoveTint(currentTint, true));
    //                    ProcessCurrentColour(ref finalColour);
    //                    SetUpCurrentTint(true);
    //                    StartCoroutine(MoveTint(currentTint, true));
    //                    playerColor = PrimaryColor.YELLOW;
    //                    break;
    //            }
    //            break;
    //    }
    //}

    private void FixedUpdate()
    {
        if (currentAnimationFrames > 0)
        {
            currentColour.r = EaseOutBack(currentColour.r, endcolour.r, 0.05f);
            currentColour.g = EaseOutBack(currentColour.g, endcolour.g, 0.05f);
            currentColour.b = EaseOutBack(currentColour.b, endcolour.b, 0.05f);

            colourTint.color = currentColour;
            --currentAnimationFrames;
        }


        m2.transform.localPosition = new Vector3(EaseOutBack(m2.transform.localPosition.x, currentBar.localPosition.x + currentBar.sizeDelta.x + 10f, 0.2f), EaseOutBack(m2.transform.localPosition.y, currentBar.localPosition.y, 0.2f), m2.transform.localPosition.z);
    }

    public void SwapToColour(PrimaryColor finalColour, ref PrimaryColor playerColor)
    {
        //so what will happen is that the screen lerps to the final colour

        currentColour = colourTint.color;
        endcolour = Color.red;
        switch (finalColour)
        {
            case PrimaryColor.RED:
                endcolour = Color.red;
                playerColor = PrimaryColor.RED;
                MoveM2(RedCD_UI.rectTransform);
                if (GameManager.instance.schmover.RedCD > 50)
                    m2.color = Color.gray;
                else
                    m2.color = Color.white;

                break;
            case PrimaryColor.YELLOW:
                endcolour = Color.yellow;
                playerColor = PrimaryColor.YELLOW;
                MoveM2(YellowCD_UI.rectTransform);
                if (GameManager.instance.schmover.YellowCD > 50)
                    m2.color = Color.gray;
                else
                    m2.color = Color.white;

                break;
            case PrimaryColor.BLUE:
                endcolour = Color.blue;
                playerColor = PrimaryColor.BLUE;
                MoveM2(BlueCD_UI.rectTransform);
                if (GameManager.instance.schmover.BlueCD > 50)
                    m2.color = Color.gray;
                else
                    m2.color = Color.white;

                break;
            
        }
        EnemyManager.instance.TriggerStopwatch(); // Trigger the stopwatch event when color changes
        //ProcessCurrentColour(ref playerColor);
        currentAnimationFrames += framesPerAnimation;
    }


    //private void SetUpCurrentTint(bool isGoingRight)
    //{
    //    if (isGoingRight)
    //        currentTint.localPosition = new Vector3(-1920, 0, 0);
    //    else
    //        currentTint.localPosition = new Vector3(1920, 0, 0);
    //}

    //IEnumerator MoveTint(RectTransform tint, bool isGoingRight)
    //{
    //    float oldX = tint.localPosition.x;
    //    Debug.Log(oldX);
    //    float finalX = oldX + (isGoingRight ? 1920 : -1920);
    //    Debug.Log(finalX);
    //    float currX = oldX;
    //    Debug.Log(currX);
    //    int counter = 0;

    //    while (counter <= 100)
    //    {
    //        if (isGoingRight)
    //            currX = EaseInCubic(currX, finalX, screenFlashSpeed);
    //        else
    //            currX = EaseInCubic(currX, finalX, screenFlashSpeed);
    //        tint.localPosition = new Vector3(currX, tint.localPosition.y, tint.localPosition.z);
    //        ++counter;
    //        yield return null;
    //    }
    //    tint.localPosition = new Vector3(finalX, tint.localPosition.y, tint.localPosition.z);
    //    yield return null;
    //}

    //private void ProcessCurrentColour(ref PrimaryColor playerColour)
    //{
    //    switch (playerColour)
    //    {
    //        case PrimaryColor.RED:
    //            red_m2.SetActive(true);
    //            yellow_m2.SetActive(false);
    //            blue_m2.SetActive(false);
    //            //currentTint = redTint;
    //            break;

    //        case PrimaryColor.YELLOW:
    //            red_m2.SetActive(false);
    //            yellow_m2.SetActive(true);
    //            blue_m2.SetActive(false);
    //            //currentTint = yellowTint;
    //            break;

    //        case PrimaryColor.BLUE:
    //            red_m2.SetActive(false);
    //            yellow_m2.SetActive(false);
    //            blue_m2.SetActive(true);
    //            //currentTint = blueTint;
    //            break;
    //    }
    //    EnemyManager.instance.TriggerStopwatch(); // Trigger the stopwatch event when color changes
    //}

    void MoveM2(RectTransform bar)
    {
        currentBar = bar;
    }
}
