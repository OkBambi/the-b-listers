using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveColorLockMonk : MonoBehaviour
{
    [SerializeField] GameObject WaveInstance;
    [SerializeField] float ColorLockTimer;


    IEnumerator ChainScreen()
    {
        AudioManager.instance.Play("Monk_Wave_Hit");
        GameManager.instance.ChainUI.SetActive(true);
       yield return new WaitForSecondsRealtime(ColorLockTimer);
        Debug.Log("AFTER");
        AudioManager.instance.Play("Monk_Wave_Hit");
        GameManager.instance.ChainUI.SetActive(false);
        
    }


    private void OnTriggerEnter(Collider other)
    {
        IColorLock colorLock = GameManager.instance.playerScript.GetComponent<IColorLock>();
        colorLock.LockColorSelection(ColorLockTimer);
        StartCoroutine(ChainScreen());

    }

    private void OnTriggerExit(Collider other)
    {
        IColorLock colorLock = GetComponent<IColorLock>();

    }
}
