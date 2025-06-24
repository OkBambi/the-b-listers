using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedListing : MonoBehaviour
{
    [SerializeField] float timeTillKilled = 5f;

    float timer;

// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, timeTillKilled);//kills feed if it lasts longer then 5 seconds.
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeTillKilled - 3)
        {
            ChangeAlpha();
        }
    }

    public void SetScoreAndHow(string _scoreFeed)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = _scoreFeed;
    }

    public string GetScoreAndHow()
    {
        return gameObject.GetComponent<TextMeshProUGUI>().text;
    }

    private void ChangeAlpha()
    {
        gameObject.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0, 3, true);
    }
}
