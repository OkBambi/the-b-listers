using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveColorLockMonk : MonoBehaviour
{
    [SerializeField] public int ColorLockTimer;
    [SerializeField] GameObject ChainUI;

    float ChainTimer;

    public void ChainScreen()
    {
        ChainUI.SetActive(true);
        AudioManager.instance.Play("Monk_Wave_Hit");
        StartCoroutine(ExitChainScreen(ColorLockTimer));
    }

    IEnumerator ExitChainScreen(int timer)
    {
        Debug.Log("BEFORE EXIT CHAIN SCREEN TRIGGER");
        yield return new WaitForSeconds(timer);
        ChainUI.SetActive(false);
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

        ChainScreen();

    }
}
