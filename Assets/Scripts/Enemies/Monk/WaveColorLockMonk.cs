using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveColorLockMonk : MonoBehaviour
{
    [SerializeField] public int ColorLockTimer;
    [SerializeField] GameObject ChainLock;
    [SerializeField] GameObject ChainUnlock;
    [SerializeField] GameObject ChainUI;
    [SerializeField] LockColorChange LockColorChange;

    float ChainTimer;

    public void ChainScreen()
    {
        ChainLock.SetActive(true);
        ChainUnlock.SetActive(false);
        AudioManager.instance.Play("Monk_Wave_Hit");
        StartCoroutine(ExitChainScreen(ColorLockTimer));
    }


    IEnumerator ExitChainScreen(int timer)
    {
        Debug.Log("BEFORE EXIT CHAIN SCREEN TRIGGER");
        yield return new WaitForSeconds(timer);
        AudioManager.instance.Play("Monk_Wave_End");
        ChainUnlock.SetActive(true);
        ChainLock.SetActive(false);


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
