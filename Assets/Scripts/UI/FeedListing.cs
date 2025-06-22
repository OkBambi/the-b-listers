using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedListing : MonoBehaviour
{
    //[SerializeField] Text modifier;
    //[SerializeField] Text scoreAmount;
    //[SerializeField] Text killedOrUsed;
    //[SerializeField] Text scoreFeed;
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
        Debug.Log("Die");
        gameObject.GetComponent<TextMeshProUGUI>().text = _scoreFeed;
    }

    //public void SetScoreAndHow(string _modifier, string _scoreAmount, string _killedOrUsed)
    //{
    //    modifier.text = _modifier;
    //    scoreAmount.text = _scoreAmount;
    //    killedOrUsed.text = _killedOrUsed;
    //}

    //public string GetScoreAndHow()
    //{
    //    return modifier.text + " " + scoreAmount.text + " " + killedOrUsed.text;
    //}

    public string GetScoreAndHow()
    {
        return gameObject.GetComponent<TextMeshProUGUI>().text;
    }

    //private void ChangeAlpha()
    //{
    //    modifier.CrossFadeAlpha(0, 3, true);
    //    scoreAmount.CrossFadeAlpha(0, 3, true);
    //    killedOrUsed.CrossFadeAlpha(0, 3, true);
    //}
    private void ChangeAlpha()
    {
        gameObject.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0, 3, true);
    }
}
