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
        //activates the new panel
        if (panelToShow != null)
        {
            //currentActivePanel.SetActive(false);
            HideAllPanels();
            panelToShow.SetActive(true);
            currentActivePanel = panelToShow;   //updates the active panel tracker
        }
    }
    public void HideAllPanels()
    {
        monolithInfoPanel.SetActive(false);
        boidsInfoPanel.SetActive(false);
        angryBoidsInfoPanel.SetActive(false);
        stopwatchInfoPanel.SetActive(false);
        monkInfoPanel.SetActive(false);
        snakeInfoPanel.SetActive(false);
        goliathInfoPanel.SetActive(false);
    }

    //public methods that will because by button clicks
    public void OnMonolithButtonClicked()
    {
        AudioManager.instance.Play("Monolith_Spawn");
        ShowPanel(monolithInfoPanel);
    }
    public void OnBoidsButtonClicked()
    {
        AudioManager.instance.Play("Boid_Spawn");
        ShowPanel(boidsInfoPanel);
    }
    public void OnAngryBoidsButtonClicked()
    {
        AudioManager.instance.Play("A_Boid_Dash");
        ShowPanel(angryBoidsInfoPanel);
    }
    public void OnStopwatchButtonClicked()
    {
        AudioManager.instance.Play("Stopwatch_Indicator");
        ShowPanel(stopwatchInfoPanel);
    }
    public void OnMonkButtonClicked()
    {
        AudioManager.instance.Play("Monk_Cast");
        ShowPanel(monkInfoPanel);
    }
    public void OnSnakeButtonClicked()
    {
        AudioManager.instance.Play("Snake");
        ShowPanel(snakeInfoPanel);
    }
    public void OnGoliathButtonClicked()
    {
        AudioManager.instance.Play("Goliath_Dive");
        ShowPanel(goliathInfoPanel);
    }
}