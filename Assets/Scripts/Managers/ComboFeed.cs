using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Xml;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ComboFeed : MonoBehaviour
{
    public static ComboFeed theInstance;
    [SerializeField] GameObject feedListingPrefab;
    [SerializeField] Transform endFeed;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] int maxFeedLength;
    [SerializeField] float endFeedSpeed;


    [SerializeField] TextMeshProUGUI playerKilledText;
    private Queue<GameObject> currentFeedList = new Queue<GameObject>();
    private List<string> finalFeedList = new List<string>();
    private List<float> finalScoreList = new List<float>();
    private float finalScore;

    private void Awake()
    {
        theInstance = this;
    }

    public void AddNewComboFeed(string _scoreFeed, float _score) //allows you to add to the kill feed (modifier is what is done to the score. Example + or -)
    {
        GameObject newScoreFeed = Instantiate(feedListingPrefab, transform);
        newScoreFeed.transform.SetSiblingIndex(0);
        newScoreFeed.GetComponent<FeedListing>().SetScoreAndHow(_scoreFeed);

        AddToQueue(newScoreFeed, _score);
    }

    private void AddToQueue(GameObject _newScoreFeed, float _score)
    {
        
        currentFeedList.Enqueue(_newScoreFeed);
        finalScoreList.Add(_score);
        finalFeedList.Add(_newScoreFeed.GetComponent<FeedListing>().GetScoreAndHow());
        if (currentFeedList.Count > maxFeedLength)
        {
            Destroy(currentFeedList.Dequeue());
        }
    }

    public void PlayerWasKilledBy(string killer)
    {
        playerKilledText.text = "Killed by: " + killer;
    }

    public void FinalScore()
    {
        Debug.Log("Final Print");
        StartCoroutine(waitASec());
    }

    private IEnumerator waitASec()
    {
        for (int i = 0; i < finalFeedList.Count; i++)
        {
            GameObject newScoreFeed = Instantiate(feedListingPrefab, endFeed);
            newScoreFeed.transform.SetSiblingIndex(0);
            newScoreFeed.GetComponent<FeedListing>().GetComponent<TextMeshProUGUI>().fontSize = 55;
            newScoreFeed.GetComponent<FeedListing>().GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            newScoreFeed.GetComponent<FeedListing>().SetScoreAndHow(finalFeedList[i]);
            finalScore += finalScoreList[i];
            finalScoreText.text = finalScore.ToString();


            yield return new WaitForSecondsRealtime(endFeedSpeed);
        }
    }
}
