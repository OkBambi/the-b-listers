using UnityEngine;
using UnityEngine.UI;

public class TypeOfEndScreen : MonoBehaviour
{
    [SerializeField] GameObject quitButton;
    [SerializeField] GameObject tryAgainButton;
    [SerializeField] GameObject nextStageButton;
    [SerializeField] GameObject highScoreTable;
    [SerializeField] GameObject nameForNewScore;
    [SerializeField] GameObject winText;

    public void WinOrLoseEndScreen()
    {
        quitButton.SetActive(true);
        tryAgainButton.SetActive(true);
        nextStageButton.SetActive(false);
        highScoreTable.SetActive(true);
        winText.SetActive(false);
    }

    public void NextStageEndScreen()
    {
        quitButton.SetActive(false);
        tryAgainButton.SetActive(false);
        nextStageButton.SetActive(true);
        highScoreTable.SetActive(false);
        winText.SetActive(false);
    }

    public void NewHighScore()
    {
        nameForNewScore.SetActive(true);
    }

    public void WonGame()
    {
        winText.SetActive(true);
    }
}
