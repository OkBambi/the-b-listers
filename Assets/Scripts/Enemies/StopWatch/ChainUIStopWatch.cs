using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ChainUIStopWatch : MonoBehaviour
{

    public StopWatch Stopwatch;
    [SerializeField] ChainMarker[] ChainToggleables;

    [SerializeField] PrimaryColor primaryColor;
    [SerializeField] ColorSwapping ChangeColor;
    [SerializeField] ChainUIStopWatch ColorLockMonk;



    void Start()
    {
        primaryColor = Stopwatch.setColor;
        //find ColorSwapping Script in scene
        ChangeColor = FindFirstObjectByType<ColorSwapping>();
        //Find the object with chain marker script on the scene
        ChainToggleables = FindObjectsByType<ChainMarker>(FindObjectsInactive.Include, FindObjectsSortMode.None);


    }

    public void SwapChainColor()
    {
        switch (primaryColor)
        {
            case PrimaryColor.RED:
                GameManager.instance.lockMarker.color = Color.red;
                GameManager.instance.unlockedMarker.color = Color.red;
                ChangeColor.SwapToColour(PrimaryColor.RED, ref GameManager.instance.playerScript.currentColor);
                break;

            case PrimaryColor.BLUE:
                GameManager.instance.lockMarker.color = Color.blue;
                GameManager.instance.unlockedMarker.color = Color.blue;
                ChangeColor.SwapToColour(PrimaryColor.BLUE, ref GameManager.instance.playerScript.currentColor);
                break;

            case PrimaryColor.YELLOW:
                GameManager.instance.lockMarker.color = Color.yellow;
                GameManager.instance.unlockedMarker.color = Color.yellow;
                ChangeColor.SwapToColour(PrimaryColor.YELLOW, ref GameManager.instance.playerScript.currentColor);
                break;
        }
    }
}
