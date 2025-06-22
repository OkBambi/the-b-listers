using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textForTimer;
    [SerializeField] float timeRemainingInSeconds;

    public bool isCounting;
    int minutes;
    int seconds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        textForTimer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
