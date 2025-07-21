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
    [SerializeField] float timeToWaitBeforeHighScores;


    [SerializeField] TextMeshProUGUI playerKilledText;
    private Queue<GameObject> currentFeedList = new Queue<GameObject>();
    private Queue<GameObject> currentFinalFeedList = new Queue<GameObject>();
    private List<string> finalFeedList = new List<string>();
    private List<float> finalScoreList = new List<float>();
    private List<Color> finalColorList = new List<Color>();
    private static float finalScore;
    public bool isHighScoreObtained;

    private void Awake()
    {
        theInstance = this;
    }

    public void AddNewComboFeed(string _scoreFeed, float _score) //allows you to add to the kill feed (modifier is what is done to the score. Example + or -)
    {
        GameObject newScoreFeed = Instantiate(feedListingPrefab, transform);
        newScoreFeed.transform.SetSiblingIndex(0);
        newScoreFeed.GetComponent<FeedListing>().SetScoreAndHow(_scoreFeed);
        newScoreFeed.GetComponent<FeedListing>().SetColor(FindPlayerColor());

        AddToQueue(newScoreFeed, _score);
    }

    private void AddToQueue(GameObject _newScoreFeed, float _score)
    {
        currentFeedList.Enqueue(_newScoreFeed);
        finalScoreList.Add(_score);
        finalFeedList.Add(_newScoreFeed.GetComponent<FeedListing>().GetScoreAndHow());
        finalColorList.Add(FindPlayerColor());

        if (currentFeedList.Count > maxFeedLength)
        {
            Destroy(currentFeedList.Dequeue());
        }
    }

    private Color FindPlayerColor()
    {
        var playerColor = GameManager.instance.playerScript.currentColor;
        Color textColor;
        if (playerColor == PrimaryColor.RED)
        {
            textColor = Color.red;
        }
        else if (playerColor == PrimaryColor.BLUE)
        {
            textColor = Color.blue;
        }
        else
        {
            textColor = Color.yellow;
        }
        return textColor;
    }

    public void FinalScore()
    {
        if (GameManager.instance.isWon)
        {
            GameManager.instance.GetActiveMenu().GetComponent<TypeOfEndScreen>().WonGame();
        }
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
            newScoreFeed.GetComponent<FeedListing>().SetColor(finalColorList[i]);
            finalScore += finalScoreList[i];
            finalScoreText.text = finalScore.ToString();
            finalScoreText.color = finalColorList[i];

            currentFinalFeedList.Enqueue(newScoreFeed);
            if (currentFinalFeedList.Count > maxFeedLength - 6)
            {
                Destroy(currentFinalFeedList.Dequeue());
            }

            yield return new WaitForSecondsRealtime(endFeedSpeed);

            if (i == finalFeedList.Count - 1)
            {
                GameObject finalMultiplier = Instantiate(feedListingPrefab, endFeed);
                finalMultiplier.transform.SetSiblingIndex(0);
                finalMultiplier.GetComponent<FeedListing>().GetComponent<TextMeshProUGUI>().fontSize = 55;
                finalMultiplier.GetComponent<FeedListing>().GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                finalMultiplier.GetComponent<FeedListing>().SetScoreAndHow("* " + ComboManager.instance.difficultyMult + " Difficulty Multiplier");
                finalMultiplier.GetComponent<FeedListing>().SetColor(Color.white);

                finalScore *= ComboManager.instance.difficultyMult;
                finalScoreText.text = finalScore.ToString();
                finalScoreText.color = Color.white;
            }
        }
        currentFinalFeedList.Clear();

        if ((GameManager.instance.player.GetComponentInParent<Player>().isDead || GameManager.instance.isWon) && HighScoreManager.theInstance.IsHighScore((int)finalScore))
        {
            isHighScoreObtained = true;
            GameManager.instance.GetActiveMenu().GetComponent<TypeOfEndScreen>().NewHighScore();
        }

        //clear screen for highscore
        yield return new WaitForSecondsRealtime(timeToWaitBeforeHighScores);
        //check for highscore then saves if found
        if (GameManager.instance.player.GetComponentInParent<Player>().isDead || GameManager.instance.isWon)
        {

            GameManager.instance.GetActiveMenu().GetComponent<TypeOfEndScreen>().WinOrLoseEndScreen();
            finalScoreText.text = "";
            playerKilledText.text = "";
            StartCoroutine(HighScoreManager.theInstance.DisplayHighScoreTable());
        }
        else
        {
            GameManager.instance.GetActiveMenu().GetComponent<TypeOfEndScreen>().NextStageEndScreen();
        }
    }

    public void PlayerWasKilledBy(string killer)
    {
        playerKilledText.text = "Killed by: " + killer;
    }

    public void clearFinalScore()
    {
        finalScore = 0;
    }
}
