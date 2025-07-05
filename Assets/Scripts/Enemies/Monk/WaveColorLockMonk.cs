using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaveColorLockMonk : MonoBehaviour
{
    [SerializeField] public int ColorLockTimer;
    [SerializeField] ChainMarker[] ChainStates;
    [SerializeField] RawImage[] ChainToggleables;
    [SerializeField] LockColorChange LockColorChange;

    private void Start()
    {
        //Find the object with chain marker script on the scene
        ChainStates = FindObjectsByType<ChainMarker>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        //add Images to ChainImageArray
        ChainToggleables = new RawImage[2] { ChainStates[0].GetComponent<RawImage>(), ChainStates[1].GetComponent<RawImage>() };

    }


    public void ChainScreen()
    {
        ChainStates[1].gameObject.SetActive(true);
        ChainStates[0].gameObject.SetActive(false);
        AudioManager.instance.Play("Monk_Wave_Hit");
        StartCoroutine(ExitChainScreen(ColorLockTimer));
    }


    IEnumerator ExitChainScreen(int timer)
    {
        Debug.Log("BEFORE EXIT CHAIN SCREEN TRIGGER");
        yield return new WaitForSeconds(timer);
        AudioManager.instance.Play("Monk_Wave_End");
        ChainStates[0].gameObject.SetActive(true);
        ChainStates[1].gameObject.SetActive(false);


        Debug.Log("IVE GONE THROUGH IT, IT SHOULD WORK");
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER ENTERED");
        IColorLock colorLock = GameManager.instance.playerScript.GetComponent<IColorLock>();
        if (colorLock != null)
        {
            colorLock.LockColorSelection(ColorLockTimer);
        }
        LockColorChange.SwapChainColorToMonk();

        ChainScreen();

    }
}
