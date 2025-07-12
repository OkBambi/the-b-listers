using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager theInstance;
    private const string prefix = "HighScore";
    [SerializeField] const int maxHighScores = 10;
    public TextMeshProUGUI highScoreTableText;

    void Start()
    {
        theInstance = this;
    }

    public void SaveHighScore(int newHighScore)
    {
        List<int> highScores = GetHighScores();
        highScores.Add(newHighScore);
        highScores = highScores.OrderByDescending(s => s).Take(maxHighScores).ToList();

        for (int index = 0; index < maxHighScores; index++)
        {
            PlayerPrefs.SetInt(prefix + index, highScores[index]);
        }
        PlayerPrefs.Save();
    }

    public List<int> GetHighScores()
    {
        List<int> highScores = new List<int>();
        for (int index = 0; index < maxHighScores; index++)
        {
            if(PlayerPrefs.HasKey(prefix + index))
            {
                highScores.Add(PlayerPrefs.GetInt(prefix + index));
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
            PlayerPrefs.SetInt(prefix + index, 0);
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
                SaveHighScore(totalScore);
                break;
            }
        }
    }
}
