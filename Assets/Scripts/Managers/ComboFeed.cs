using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboFeed : MonoBehaviour
{
    public static ComboFeed theInstance;
    [SerializeField] GameObject feedListingPrefab;
    [SerializeField] int maxFeedLength;

    private Queue<GameObject> currentFeedList = new Queue<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theInstance = this;
    }

    public void AddNewComboFeed(string _modifier, string _score, string _killedOrUsed) //allows you to add to the kill feed (modifier is what is done to the score. Example + or -)
    {
        GameObject newScoreFeed = Instantiate(feedListingPrefab, transform);
        AddToQueue(newScoreFeed);
        newScoreFeed.transform.SetSiblingIndex(0);
        newScoreFeed.GetComponent<FeedListing>().SetScoreAndHow(_modifier, _score, _killedOrUsed);
    }

    private void AddToQueue(GameObject _newScoreFeed)
    {
        currentFeedList.Enqueue(_newScoreFeed);
        if (currentFeedList.Count > maxFeedLength)
        {
            Destroy(currentFeedList.Dequeue());
        }
    }
}
