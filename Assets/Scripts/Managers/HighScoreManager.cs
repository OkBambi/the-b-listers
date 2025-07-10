using NUnit.Framework;
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

    public void SaveHighScore(float newHighScore)
    {
        List<float> highScores = GetHighScores();
        highScores.Add(newHighScore);
        highScores = highScores.OrderByDecending(s => s).Take(MaxHighScores).ToList();

        for (int index = 0; index < highScores.Count; index++)
        {
            PlayerPrefs.SetFloat(prefix + index, highScores[index]);
        }
        PlayerPrefs.Save();
    }
}
