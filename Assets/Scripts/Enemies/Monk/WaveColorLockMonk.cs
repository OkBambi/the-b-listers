using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveColorLockMonk : MonoBehaviour
{
    [SerializeField] GameObject WaveInstance;
    [SerializeField] float ColorLockTimer;

    //animation
    [SerializeField] GameObject ChainUI;

    IEnumerator ChainScreen()
    {
        Debug.Log("Before UI");
        yield return new WaitForSecondsRealtime(ColorLockTimer);
        Debug.Log("UI OFF");
        ChainUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        IColorLock colorLock = GameManager.instance.playerScript.GetComponent<IColorLock>();
        colorLock.LockColorSelection(ColorLockTimer);
        Debug.Log("UI ON");
        ChainUI.SetActive(true);
        StartCoroutine(ChainScreen());
    }

    private void OnTriggerExit(Collider other)
    {
        IColorLock colorLock = GetComponent<IColorLock>();

    }
}
