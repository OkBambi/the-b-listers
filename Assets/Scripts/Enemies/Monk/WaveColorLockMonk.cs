using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveColorLockMonk : MonoBehaviour
{
    [SerializeField] GameObject WaveInstance;
    [SerializeField] public int ColorLockTimer;
    [SerializeField] GameObject ChainUI;

    float ChainTimer;

    public void ChainScreen()
    {
        ChainTimer += Time.deltaTime;
        AudioManager.instance.Play("Monk_Wave_Hit");
        ChainUI.SetActive(true);

        if (ChainTimer <= ColorLockTimer)
        {
            Debug.Log("AFTER");
            AudioManager.instance.Play("Monk_Wave_Hit");
            ChainUI.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        IColorLock colorLock = GameManager.instance.playerScript.GetComponent<IColorLock>();
        colorLock.LockColorSelection(ColorLockTimer);
        ChainScreen();

    }

    private void OnTriggerExit(Collider other)
    {
        IColorLock colorLock = GetComponent<IColorLock>();

    }
}
