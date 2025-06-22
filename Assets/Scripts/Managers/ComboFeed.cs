using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ComboFeed : MonoBehaviour
{
    public static ComboFeed theInstance;
    [SerializeField] GameObject feedListingPrefab;
    [SerializeField] int maxFeedLength;

    private Queue<GameObject> currentFeedList = new Queue<GameObject>();
    private List<String> finalScoreList = new List<String>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theInstance = this;
    }

    public void AddNewComboFeed(string _scoreFeed) //allows you to add to the kill feed (modifier is what is done to the score. Example + or -)
    {
        GameObject newScoreFeed = Instantiate(feedListingPrefab, transform);
        newScoreFeed.transform.SetSiblingIndex(0);
        newScoreFeed.GetComponent<FeedListing>().SetScoreAndHow(_scoreFeed);
        AddToQueue(newScoreFeed);
    }

    private void AddToQueue(GameObject _newScoreFeed)
    {
        currentFeedList.Enqueue(_newScoreFeed);
        finalScoreList.Add(_newScoreFeed.GetComponent<FeedListing>().GetScoreAndHow());
        if (currentFeedList.Count > maxFeedLength)
        {
            Destroy(currentFeedList.Dequeue());
        }
    }

    public List<String> GetFinalScoreList()
    {
        return finalScoreList;
    }
}
