using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LockColorChangeMonk_boss : MonoBehaviour
{
    [SerializeField] Monk_MiniBoss monkBoss;
    [SerializeField] ChainMarker[] ChainToggleables;
    //[SerializeField] RawImage[] ChainImageArray;
    [SerializeField] PrimaryColor primaryColor;
    [SerializeField] ColorSwapping ChangeColor;
    [SerializeField] WaveColorLockMonk ColorLockMonk;

    //[SerializeField] RawImage locks;
    //[SerializeField] RawImage unlocks;

    //[SerializeField] ChainMarker lockMarker;
    //[SerializeField] ChainMarker unlockedMarker;




    void Start()
    {
        primaryColor = monkBoss.setColor;
        //find ColorSwapping Script in scene
        ChangeColor = FindFirstObjectByType<ColorSwapping>();
        //Find the object with chain marker script on the scene
        ChainToggleables = FindObjectsByType<ChainMarker>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        //add Images to ChainImageArray
        //ChainImageArray = new RawImage[2] { ChainToggleables[0].GetComponent<RawImage>(), ChainToggleables[1].GetComponent<RawImage>() };

        //lockMarker = GameObject.Find("ChainLock").GetComponent<ChainMarker>();
        //unlockedMarker = GameObject.Find("ChainUnlock").GetComponent<ChainMarker>();

        //locks = lockMarker.gameObject.GetComponent<RawImage>();
        //unlocks = unlockedMarker.gameObject.GetComponent<RawImage>();

        //lockMarker.gameObject.SetActive(false);
        //unlockedMarker.gameObject.SetActive(false);

        //locks = GameObject.Find("ChainLock").GetComponent<RawImage>();
        //unlockedMarker = GameObject.Find("ChainUnlock").GetComponent<ChainMarker>();

        //locks.gameObject.SetActive(false);
        //unlockedMarker.gameObject.SetActive(false);

    }

    public void SwapChainColor(PrimaryColor color)
    {
        primaryColor = color;
        SwapChainColor();
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

