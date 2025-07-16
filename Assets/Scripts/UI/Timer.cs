using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textForTimer;
    [SerializeField] TextMeshProUGUI fadeInText;
    [SerializeField] TextMeshProUGUI fadeOutText;

    [SerializeField] float timeRemainingInSeconds;

    public bool isCounting;
    int minutes;
    int seconds;
    Vector3 fadeInTextOrigPos;
    Vector3 fadeOutTextOrigPos;
    bool isFading;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fadeInTextOrigPos = fadeInText.transform.position;
        fadeOutTextOrigPos = fadeOutText.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCounting) return;

        if (timeRemainingInSeconds > 0)
        {
            timeRemainingInSeconds -= Time.deltaTime;
        }
        else if (timeRemainingInSeconds <= 0)
        {
            timeRemainingInSeconds = 0;
            GameManager.instance.OnEndCondition();
        }

        minutes = Mathf.FloorToInt(timeRemainingInSeconds / 60);
        seconds = Mathf.FloorToInt(timeRemainingInSeconds % 60);


        if (seconds == 10)
        {
            textForTimer.color = Color.red;
        }

        if (seconds <= 10)
        {
            ShakeText();
        }

        if (!isFading)
        {
            StartCoroutine(FadeInEffect());
            StartCoroutine(FadeOutEffect());
        }
        else
        {
            fadeInText.transform.position -= new Vector3(0, 0.2f, 0);
            fadeOutText.transform.position -= new Vector3(0, 0.2f, 0);
        }

            textForTimer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        


    }

    IEnumerator FadeInEffect()//look to see if you make fade in and fade out work in the same fuction
    {
        isFading = true;
        fadeInText.text = string.Format("{0:0}:{1:00}", minutes, seconds - 1);
        fadeInText.CrossFadeAlpha(255f, 1f, false);
        yield return new WaitForSeconds(1);
        fadeInText.CrossFadeAlpha(1f, 0f, false);
        fadeInText.transform.position = fadeInTextOrigPos;
        isFading = false;
    }
    IEnumerator FadeOutEffect()
    {
        fadeOutText.text = string.Format("{0:0}:{1:00}", minutes, seconds + 1);
        fadeOutText.CrossFadeAlpha(1f, 0f, false);
        yield return new WaitForSeconds(1);
        fadeOutText.CrossFadeAlpha(255f, 1f, false);
        fadeOutText.transform.position = fadeOutTextOrigPos;
    }


    private void ShakeText()
    {

    }
}
