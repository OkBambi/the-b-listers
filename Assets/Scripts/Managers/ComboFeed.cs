using UnityEngine;

public class ComboFeed : MonoBehaviour
{
    public static ComboFeed theInstance;
    [SerializeField] GameObject feedListingPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theInstance = this;
    }

    public void AddNewComboFeed(string _modifier, string _score, string _killedOrUsed) //allows you to add to the kill feed (modifier is what is done to the score. Example + or -)
    {
        GameObject newScoreFeed = Instantiate(feedListingPrefab, transform);
        newScoreFeed.transform.SetSiblingIndex(0);
        FeedListing feedListing = newScoreFeed.GetComponent<FeedListing>();
        feedListing.SetScoreAndHow(_modifier, _score, _killedOrUsed);
    }
}
