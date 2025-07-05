using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LockColorChange : MonoBehaviour
{
    [SerializeField] Monk MonkPrimary;
    [SerializeField] ChainMarker[] ChainToggleables;
    [SerializeField] RawImage[] ChainImageArray;
    [SerializeField] PrimaryColor PrimaryColor;
    [SerializeField] ColorSwapping ChangeColor;

    void Start()
    {
        PrimaryColor = MonkPrimary.setColor;

        //find ColorSwapping Script in scene
        ChangeColor = FindFirstObjectByType<ColorSwapping>();
        //Find the object with chain marker script on the scene
        ChainToggleables = FindObjectsByType<ChainMarker>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        //add Images to ChainImageArray
        ChainImageArray = new RawImage[2] { ChainToggleables[0].GetComponent<RawImage>(), ChainToggleables[1].GetComponent<RawImage>()};
        
    }


    void Update()
    {
    }

    public void SwapChainColorToMonk()
    {
        switch (PrimaryColor)
        {
            case PrimaryColor.RED:
                ChainImageArray[0].color = Color.red;
                ChainImageArray[1].color = Color.red;
                ChangeColor.SwapToColour(GameManager.instance.playerScript.GetPlayerColor(), PrimaryColor.RED, ref GameManager.instance.playerScript.currentColor);
                break;

            case PrimaryColor.BLUE:
                ChainImageArray[0].color = Color.blue;
                ChainImageArray[1].color = Color.blue;
                ChangeColor.SwapToColour(GameManager.instance.playerScript.GetPlayerColor(), PrimaryColor.BLUE, ref GameManager.instance.playerScript.currentColor);
                break;

            case PrimaryColor.YELLOW:
                ChainImageArray[0].color = Color.yellow;
                ChainImageArray[1].color = Color.yellow;
                ChangeColor.SwapToColour(GameManager.instance.playerScript.GetPlayerColor(), PrimaryColor.YELLOW, ref GameManager.instance.playerScript.currentColor);
                break;
        }
    }
}

