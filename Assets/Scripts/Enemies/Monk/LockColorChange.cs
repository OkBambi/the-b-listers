using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LockColorChange : MonoBehaviour
{
    [SerializeField] Monk MonkPrimary;
    [SerializeField] ChainMarker[] ChainStates;
    [SerializeField] RawImage[] ChainToggleables;
    [SerializeField] PrimaryColor PrimaryColor;
    [SerializeField] ColorSwapping ChangeColor;
    [SerializeField] WaveColorLockMonk ColorLockMonk;




    void Start()
    {
        PrimaryColor = MonkPrimary.setColor;

        //find ColorSwapping Script in scene
        ChangeColor = FindFirstObjectByType<ColorSwapping>();
        //Find the object with chain marker script on the scene
        ChainStates = FindObjectsByType<ChainMarker>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        ChainToggleables = new RawImage[2];

        foreach (ChainMarker chainMarker in ChainStates)
        {
            if (chainMarker.chainType == ChainType.Lock)
            {
                ChainToggleables[0] = chainMarker.GetComponent<RawImage>();
            }
            if (chainMarker.chainType == ChainType.Unlock)
            {
                ChainToggleables[1] = chainMarker.GetComponent<RawImage>();
            }
        }
    }


    void Update()
    {
    }

    public void SwapChainColorToMonk()
    {
        switch (PrimaryColor)
        {
            case PrimaryColor.RED:
                ChainToggleables[0].color = Color.red;
                ChainToggleables[1].color = Color.red;
                ChangeColor.SwapToColour(GameManager.instance.playerScript.GetPlayerColor(), PrimaryColor.RED, ref GameManager.instance.playerScript.currentColor);
                break;

            case PrimaryColor.BLUE:
                ChainToggleables[0].color = Color.blue;
                ChainToggleables[1].color = Color.blue;
                ChangeColor.SwapToColour(GameManager.instance.playerScript.GetPlayerColor(), PrimaryColor.BLUE, ref GameManager.instance.playerScript.currentColor);
                break;

            case PrimaryColor.YELLOW:
                ChainToggleables[0].color = Color.yellow;
                ChainToggleables[1].color = Color.yellow;
                ChangeColor.SwapToColour(GameManager.instance.playerScript.GetPlayerColor(), PrimaryColor.YELLOW, ref GameManager.instance.playerScript.currentColor);
                break;
        }
    }
}

