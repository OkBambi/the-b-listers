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
    public TextMeshProUGUI highScoreTableText;
    public TMP_InputField userName;

    void Start()
    {
        theInstance = this;
        for (int index = 0; index < maxHighScores; index++)
        {
            PlayerPrefs.SetString(playerNamePrefix + index, "Alphonsine");
        }
    }

    public void SaveHighScore(int newHighScore, int location, string userName)
    {
        //List<int> highScores = GetHighScores();
        //highScores.Add(newHighScore);
        
        //highScores = highScores.OrderByDescending(s => s).Take(maxHighScores).ToList();

        //shuffes everything else into order
        for (int index = location ; index < maxHighScores; index++)
        {
            string swapedUserName = PlayerPrefs.GetString(playerNamePrefix + index);
            int swapedScore = PlayerPrefs.GetInt(highscorePrefix + index);
            PlayerPrefs.SetString(playerNamePrefix + index, userName);
            PlayerPrefs.SetInt(highscorePrefix + index, newHighScore);
            userName = swapedUserName;
            swapedScore = newHighScore;
        }
        PlayerPrefs.Save();
    }

    public List<int> GetHighScores()
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

    public void DisplayHighScoreTable()
    {
        string highScoreTable = "High Scores:\n";
        List<int> highScores = GetHighScores();
        for (int index = 0; index < maxHighScores; ++index)
        {
            highScoreTable += (index + 1) + ". " + highScores[index] + "\n";
        }
        highScoreTableText.text = highScoreTable;
    }

    public void ClearHighScores()
    {
        for (int index = 0; index < maxHighScores; ++index)
        {
            PlayerPrefs.SetInt(highscorePrefix + index, 0);
        }
    }

    public void SaveIfHighScore()
    {
        List<int> highscores = GetHighScores();
        int totalScore = ComboManager.instance.GetScore();
        for (int index = 0; index < highscores.Count; index++)
        {
            if (totalScore > highscores[index])
            {
                //SaveHighScore(totalScore, index);
                break;
            }
        }
    }

    public void SaveName()
    {
        //userName.text;
        //PlayerPrefs.SetString(prefix + , userName.text);
    }
}
