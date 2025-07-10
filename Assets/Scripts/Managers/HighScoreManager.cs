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

    public static void SaveHighScore(float newHighScore)
    {
        List<float> highScores = GetHighScores();
        highScores.Add(newHighScore);
        highScores = highScores.OrderByDescending(s => s).Take(maxHighScores).ToList();

        for (int index = 0; index < maxHighScores; index++)
        {
            PlayerPrefs.SetFloat(prefix + index, highScores[index]);
        }
        PlayerPrefs.Save();
    }

    public static List<float> GetHighScores()
    {
        List<float> highScores = new List<float>();
        for (int index = 0; index < maxHighScores; index++)
        {
            if(PlayerPrefs.HasKey(prefix + index))
            {
                highScores.Add(PlayerPrefs.GetFloat(prefix + index));
            }
        }
        return highScores;
    }

    public void DisplayHighScoreTable()
    {
        string highScoreTable = "High Scores:\n";
        List<float> highScores = GetHighScores();
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
            PlayerPrefs.SetFloat(prefix + index, 0);
        }
    }
}
