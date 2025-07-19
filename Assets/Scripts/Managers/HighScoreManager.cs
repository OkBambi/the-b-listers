using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static TextAnimations;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager theInstance;
    private const string highscorePrefix = "HighScore";
    private const string playerNamePrefix = "PlayerName"; 
    [SerializeField] const int maxHighScores = 10;
    public TextMeshProUGUI highScoresTableText;
    public TextMeshProUGUI highScoresNamesTableText;
    public TMP_InputField userName;
    private int indexForNewName;
    private bool isHighScoreDisplayed;

    TMP_Text highScoresTableTextMesh;
    TMP_Text highScoresNamesTableTextMesh;

    void Start()
    {
        theInstance = this;
        if (!PlayerPrefs.HasKey(highscorePrefix + 0))
        {
            ClearHighScores();
            PlayerPrefs.Save();
        }
        highScoresTableTextMesh = highScoresTableText.GetComponent<TMP_Text>();
        highScoresNamesTableTextMesh = highScoresNamesTableText.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (isHighScoreDisplayed)
        {
            AnimateText(highScoresTableTextMesh, 0.1f, PerChar, Shake);
            AnimateText(highScoresNamesTableTextMesh, 0.2f, PerChar, Shake);
        }
    }

    public void SaveHighScore(int newHighScore, int location, string userName)
    {
        //shuffes everything else into order
        for (int index = location ; index < maxHighScores; index++)
        {
            string swapedUserName = PlayerPrefs.GetString(playerNamePrefix + index);
            int swapedScore = PlayerPrefs.GetInt(highscorePrefix + index);
            PlayerPrefs.SetString(playerNamePrefix + index, userName);
            PlayerPrefs.SetInt(highscorePrefix + index, newHighScore);
            userName = swapedUserName;
            newHighScore = swapedScore;
        }
        PlayerPrefs.Save();
    }

    private List<int> GetScores()
    {
        List<int> highScores = new List<int>();
        for (int index = 0; index < maxHighScores; index++)
        {
            if(PlayerPrefs.HasKey(highscorePrefix + index))
            {
                highScores.Add(PlayerPrefs.GetInt(highscorePrefix + index));
            }
        }
        return highScores;
    }

    private List<string> GetNames()
    {
        List<string> highScoreNames = new List<string>();
        for (int index = 0; index < maxHighScores; index++)
        {
            if (PlayerPrefs.HasKey(playerNamePrefix + index))
            {
                highScoreNames.Add(PlayerPrefs.GetString(playerNamePrefix + index));
            }
        }
        return highScoreNames;
    }

    public IEnumerator DisplayHighScoreTable()
    {
        isHighScoreDisplayed = true;
        string highScoreTable = "High Scores:\n";
        string highScoreNameTable = "\n";
        highScoresNamesTableText.text += highScoreNameTable;
        highScoresTableText.text += highScoreTable;
        List<int> highScores = GetScores();
        List<string> highScoresNames = GetNames();
        for (int index = 0; index < maxHighScores; ++index)
        {
            //highScoreNameTable += highScoresNames[index] + "\n";
            //highScoreTable += (index + 1) + ". " + highScores[index] + "\n";
            highScoresNamesTableText.text += highScoresNames[index] + "\n";
            highScoresTableText.text += (index + 1) + ". " + highScores[index] + "\n";
            yield return new WaitForSecondsRealtime(0.2f);
        }

        if (ComboFeed.theInstance.isHighScoreObtained)
        {
            GameManager.instance.GetActiveMenu().GetComponent<TypeOfEndScreen>().EnterHighScoreName();
        }
    }

    public void UpdateHighScoreTable()
    {
        string highScoreTable = "High Scores:\n";
        string highScoreNameTable = "\n";
        List<int> highScores = GetScores();
        List<string> highScoresNames = GetNames();
        for (int index = 0; index < maxHighScores; ++index)
        {
            highScoreNameTable += highScoresNames[index] + "\n";
            highScoreTable += (index + 1) + ". " + highScores[index] + "\n";
        }
        highScoresNamesTableText.text = highScoreNameTable;
        highScoresTableText.text = highScoreTable;
    }

    public void ClearHighScores()
    {
        for (int index = 0; index < maxHighScores; ++index)
        {
            PlayerPrefs.SetInt(highscorePrefix + index, 0);
            PlayerPrefs.SetString(playerNamePrefix + index, "Alphonsine");
        }
    }

    public bool IsHighScore()
    {
        bool isHighscore = false;
        List<int> highscores = GetScores();
        int totalScore = ComboManager.instance.GetScore();
        for (int index = 0; index < highscores.Count; index++)
        {
            if (totalScore > highscores[index])
            {
                SaveHighScore(totalScore, index, "");
                indexForNewName = index;
                isHighscore = true;
                break;
            }
        }
        return isHighscore;
    }

    public void SaveName()
    {
        AudioManager.instance.Play("Key_Click", UnityEngine.Random.Range(0.9f, 1.1f));
        PlayerPrefs.SetString(playerNamePrefix + indexForNewName, userName.text);
        UpdateHighScoreTable();
    }
}
