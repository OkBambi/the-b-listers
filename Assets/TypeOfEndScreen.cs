using UnityEngine;
using UnityEngine.UI;

public class TypeOfEndScreen : MonoBehaviour
{
    public GameObject quitButton;
    public GameObject tryAgainButton;
    public GameObject nextStageButton;
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
    }

    public void LoseEndScreen()
    {
        quitButton.SetActive(true);
        tryAgainButton.SetActive(true);
        nextStageButton.SetActive(false);
    }

    public void NextStageEndScreen()
    {
        quitButton.SetActive(false);
        tryAgainButton.SetActive(false);
        nextStageButton.SetActive(true);
    }
}
