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

    private string scoreFileName = "ScoreFile.txt";
    private Queue<GameObject> currentFeedList = new Queue<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theInstance = this;
        File.WriteAllText(scoreFileName, "");
    }

    public void AddNewComboFeed(string _modifier, string _score, string _killedOrUsed) //allows you to add to the kill feed (modifier is what is done to the score. Example + or -)
    {
        GameObject newScoreFeed = Instantiate(feedListingPrefab, transform);
        newScoreFeed.transform.SetSiblingIndex(0);
        newScoreFeed.GetComponent<FeedListing>().SetScoreAndHow(_modifier, _score, _killedOrUsed);
        AddToQueue(newScoreFeed);
    }

    private void AddToQueue(GameObject _newScoreFeed)
    {
        currentFeedList.Enqueue(_newScoreFeed);
        AddScoreToFile(_newScoreFeed);
        if (currentFeedList.Count > maxFeedLength)
        {
            Destroy(currentFeedList.Dequeue());
        }
    }

    private void AddScoreToFile(GameObject _newScoreFeed)
    {
        StreamWriter writer = new StreamWriter(scoreFileName, true);
        writer.Write(_newScoreFeed.GetComponent<FeedListing>().GetScoreAndHow());
    }
}
