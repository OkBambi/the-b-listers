using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        theInstance = this;
        if (!PlayerPrefs.HasKey("MakeHighScoreTable"))
        {
            PlayerPrefs.SetInt("MakeHighScoreTable", 1);
            ClearHighScores();
            PlayerPrefs.Save();
        }
        //ClearHighScores();
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

    public void DisplayHighScoreTable()
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
        highScoresTableText.text = highScoreTable;
        highScoresNamesTableText.text = highScoreNameTable;
    }

    public void ClearHighScores()
    {
        for (int index = 0; index < maxHighScores; ++index)
        {
            PlayerPrefs.SetInt(highscorePrefix + index, 0);
            PlayerPrefs.SetString(playerNamePrefix + index, "Alphonsine");
        }
    }

    public bool SaveIfHighScore()
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
        PlayerPrefs.SetString(playerNamePrefix + indexForNewName, userName.text);
        DisplayHighScoreTable();
    }
}
