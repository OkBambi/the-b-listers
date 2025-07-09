using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LockColorChange : MonoBehaviour
{
    [SerializeField] Monk MonkPrimary;
    [SerializeField] ChainMarkerOpen ChainOn;
    [SerializeField] ChainMarkerClose ChainOff;
    [SerializeField] PrimaryColor PrimaryColor;
    [SerializeField] ColorSwapping ChangeColor;
    [SerializeField] WaveColorLockMonk ColorLockMonk;

    [SerializeField] RawImage ChainOnImage;
    [SerializeField] RawImage ChainOffImage;


    void Start()
    {
        PrimaryColor = MonkPrimary.setColor;

        //find ColorSwapping Script in scene
        ChangeColor = FindFirstObjectByType<ColorSwapping>();
        //Find the object with chain marker script on the scene
        ChainOn = FindAnyObjectByType<ChainMarkerOpen>();
        ChainOff = FindAnyObjectByType<ChainMarkerClose>();

        ChainOnImage = ChainOn.GetComponent<RawImage>();
        ChainOffImage = ChainOff.GetComponent<RawImage>();
    }


    void Update()
    {
    }

    public void SwapChainColorToMonk()
    {
        switch (PrimaryColor)
        {
            case PrimaryColor.RED:
                ChainOnImage.color = Color.red;
                ChainOffImage.color = Color.red;
                ChangeColor.SwapToColour(GameManager.instance.playerScript.GetPlayerColor(), PrimaryColor.RED, ref GameManager.instance.playerScript.currentColor);
                break;

            case PrimaryColor.BLUE:
                ChainOnImage.color = Color.blue;
                ChainOffImage.color = Color.blue;
                ChangeColor.SwapToColour(GameManager.instance.playerScript.GetPlayerColor(), PrimaryColor.BLUE, ref GameManager.instance.playerScript.currentColor);
                break;

            case PrimaryColor.YELLOW:
                ChainOnImage.color = Color.yellow;
                ChainOffImage.color = Color.yellow;
                ChangeColor.SwapToColour(GameManager.instance.playerScript.GetPlayerColor(), PrimaryColor.YELLOW, ref GameManager.instance.playerScript.currentColor);
                break;
        }
    }
}

