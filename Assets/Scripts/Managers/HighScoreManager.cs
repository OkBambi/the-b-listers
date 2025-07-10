using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager theInstance;
    private const string prefix = "HighScore";
    [SerializeField] const int MaxHighScores = 10;

    void Start()
    {
        theInstance = this;
    }

    public static void SaveHighScore(float newHighScore)
    {
        List<float> highScores = GetHighScores();
        highScores.Add(newHighScore);
        highScores = highScores.OrderByDescending(s => s).Take(MaxHighScores).ToList();

        for (int index = 0; index < highScores.Count; index++)
        {
            PlayerPrefs.SetFloat(prefix + index, highScores[index]);
        }
        PlayerPrefs.Save();
    }

    public static List<float> GetHighScores()
    {
        List<float> highScores = new List<float>();
        for (int index = 0; index < MaxHighScores; index++)
        {
            if(PlayerPrefs.HasKey(prefix + index))
            {
                highScores.Add(PlayerPrefs.GetFloat(prefix + index));
            }
        }
        return highScores;
    }
}
