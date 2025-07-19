using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedListing : MonoBehaviour
{
    [SerializeField] float timeTillKilled = 5f;

// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(KillYourSelf());
    }

    public void SetScoreAndHow(string _scoreFeed)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = _scoreFeed;
        var playerColor = GameManager.instance.playerScript.currentColor;
        if (playerColor == PrimaryColor.RED)
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else if (playerColor == PrimaryColor.BLUE)
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = Color.blue;
        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = Color.yellow;
        }
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
