using UnityEngine;
using UnityEngine.UI;

public class EnemyInfoManager : MonoBehaviour
{
    //this will hold the information stuff for the enemies. gets asssigned in the Insoector
    public GameObject defaultEnemyInfoPanel;
    public GameObject monolithInfoPanel;
    public GameObject boidsInfoPanel;
    public GameObject angryBoidsInfoPanel;
    public GameObject stopwatchInfoPanel;
    public GameObject monkInfoPanel;
    public GameObject snakeInfoPanel;
    public GameObject goliathInfoPanel;

    //to keep track of the currently displayed panel
    private GameObject currentActivePanel;

    private void Start()
    {
        //ensures only the default panel is visible at the start. which will be the main enemies panel
        ShowPanel(defaultEnemyInfoPanel);
    }

    //this is a helper method to hide all panels and then show the desired one
    void ShowPanel(GameObject panelToShow)
    {
        //deactivate the currently active panel, if any
        if (currentActivePanel != null)
        {
            currentActivePanel.SetActive(false);
        }

        //activates the new panel
        if (panelToShow != null)
        {
            panelToShow.SetActive(true);
            currentActivePanel = panelToShow;   //updates the active panel tracker
        }
    }

    //public methods that will because by button clicks
    public void OnMonolithButtonClicked()
    {
        ShowPanel(monolithInfoPanel);
    }
    public void OnBoidsButtonClicked()
    {
        ShowPanel(boidsInfoPanel);
    }
    public void OnAngryBoidsButtonClicked()
    {
        ShowPanel(angryBoidsInfoPanel);
    }
    public void OnStopwatchButtonClicked()
    {
        ShowPanel(stopwatchInfoPanel);
    }
    public void OnMonkButtonClicked()
    {
        ShowPanel(monkInfoPanel);
    }
    public void OnSnakeButtonClicked()
    {
        ShowPanel(snakeInfoPanel);
    }
    public void OnGoliathButtonClicked()
    {
        ShowPanel(goliathInfoPanel);
    }
}