using System.Collections;
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
        StartCoroutine(KillYourSelf());
    }

    private void Update()
    {

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

    private IEnumerator KillYourSelf()//kills feed if it lasts longer then 5 seconds.
    {
        yield return new WaitForSecondsRealtime(timeTillKilled - 3);
        ChangeAlpha();
        yield return new WaitForSecondsRealtime(3);
        Destroy(gameObject);
    }
}
