using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ChainUIMonk : MonoBehaviour
{
    [SerializeField] Monk monk;
    [SerializeField] ChainMarker[] ChainToggleables;
    //[SerializeField] RawImage[] ChainImageArray;
    [SerializeField] PrimaryColor primaryColor;
    [SerializeField] ColorSwapping ChangeColor;
    [SerializeField] ChainUIMonk ColorLockMonk;

    //[SerializeField] RawImage locks;
    //[SerializeField] RawImage unlocks;

    //[SerializeField] ChainMarker lockMarker;
    //[SerializeField] ChainMarker unlockedMarker;


    void Start()
    {

        //find ColorSwapping Script in scene
        ChangeColor = FindFirstObjectByType<ColorSwapping>();
        //Find the object with chain marker script on the scene
        ChainToggleables = FindObjectsByType<ChainMarker>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        ////add Images to ChainImageArray
        //ChainImageArray = new RawImage[2] { ChainToggleables[0].GetComponent<RawImage>(), ChainToggleables[1].GetComponent<RawImage>() };

        //lockMarker = GameObject.Find("ChainLock").GetComponent<ChainMarker>();
        //unlockedMarker = GameObject.Find("ChainUnlock").GetComponent<ChainMarker>();

        //locks = lockMarker.gameObject.GetComponent<RawImage>();
        //unlocks = unlockedMarker.gameObject.GetComponent<RawImage>();

        //lockMarker.gameObject.SetActive(false);
        //unlockedMarker.gameObject.SetActive(false);

        if (monk!=null)
        {
            primaryColor = monk.setColor;
        }
    }

    public void SwapChainColor()
    {
        switch (primaryColor)
        {
            case PrimaryColor.RED:
                GameManager.instance.lockMarker.color = Color.red;
                GameManager.instance.unlockedMarker.color = Color.red;
                ChangeColor.SwapToColour(GameManager.instance.playerScript.GetPlayerColor(), PrimaryColor.RED, ref GameManager.instance.playerScript.currentColor);
                break;

            case PrimaryColor.BLUE:
                GameManager.instance.lockMarker.color = Color.blue;
                GameManager.instance.unlockedMarker.color = Color.blue;
                ChangeColor.SwapToColour(GameManager.instance.playerScript.GetPlayerColor(), PrimaryColor.BLUE, ref GameManager.instance.playerScript.currentColor);
                break;

            case PrimaryColor.YELLOW:
                GameManager.instance.lockMarker.color = Color.yellow;
                GameManager.instance.unlockedMarker.color = Color.yellow;
                ChangeColor.SwapToColour(GameManager.instance.playerScript.GetPlayerColor(), PrimaryColor.YELLOW, ref GameManager.instance.playerScript.currentColor);
                break;
        }
    }
}

