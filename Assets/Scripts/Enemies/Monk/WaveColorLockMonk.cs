using System.Collections;
using UnityEngine;

public class WaveColorLockMonk : MonoBehaviour
{
    [SerializeField] GameObject WaveInstance;
    [SerializeField] float ColorLockTimer;

    //animation
    [SerializeField] GameObject ChainUI;

    IEnumerator ChainScreen()
    {
        yield return new WaitForSeconds(ColorLockTimer);
        ChainUI.SetActive(false);
        Debug.Log("ChainUI Disabled");
    }

    private void OnTriggerEnter(Collider other)
    {
        IColorLock colorLock = GameManager.instance.playerScript.GetComponent<IColorLock>();
        colorLock.LockColorSelection(ColorLockTimer);
        ChainUI.SetActive(true);
        StartCoroutine(ChainScreen());
    }

    private void OnTriggerExit(Collider other)
    {
        IColorLock colorLock = GetComponent<IColorLock>();

    }
}
