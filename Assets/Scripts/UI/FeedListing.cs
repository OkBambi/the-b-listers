using UnityEngine;
using UnityEngine.UI;

public class FeedListing : MonoBehaviour
{
    [SerializeField] Text modifier;
    [SerializeField] Text scoreAmount;
    [SerializeField] Text killedOrUsed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5f);//kills feed if it lasts longer then 5 seconds.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScoreAndHow(string _modifier, string _scoreAmount, string _killedOrUsed)
    {
        modifier.text = _modifier;
        scoreAmount.text = _scoreAmount;
        killedOrUsed.text = _killedOrUsed;
    }
}
