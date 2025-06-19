using UnityEngine;
using UnityEngine.UI;

public class FeedListing : MonoBehaviour
{
    [SerializeField] Text modifier;
    [SerializeField] Text scoreAmount;
    [SerializeField] Text killedOrUsed;
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
        if (timer >= timeTillKilled - 2)
        {
            //MoveText(); Fix this bud
        }
    }

    public void SetScoreAndHow(string _modifier, string _scoreAmount, string _killedOrUsed)
    {
        modifier.text = _modifier;
        scoreAmount.text = _scoreAmount;
        killedOrUsed.text = _killedOrUsed;
    }

    public string GetScoreAndHow()
    {
        return modifier.text + " " + scoreAmount.text + " " + killedOrUsed.text;
    }

    private void MoveText()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition3D += Time.deltaTime;
    }
}
