using UnityEngine;
using UnityEngine.UI;

public class TypeOfEndScreen : MonoBehaviour
{
    [SerializeField] GameObject quitButton;
    [SerializeField] GameObject tryAgainButton;
    [SerializeField] GameObject nextStageButton;
    [SerializeField] GameObject highScoreTable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinEndScreen()
    {
        quitButton.SetActive(true);
        tryAgainButton.SetActive(true);
        nextStageButton.SetActive(false);
        highScoreTable.SetActive(true);
    }

    public void LoseEndScreen()
    {
        quitButton.SetActive(true);
        tryAgainButton.SetActive(true);
        nextStageButton.SetActive(false);
        highScoreTable.SetActive(true);
    }

    public void NextStageEndScreen()
    {
        quitButton.SetActive(false);
        tryAgainButton.SetActive(false);
        nextStageButton.SetActive(true);
        highScoreTable.SetActive(false);
    }
}
