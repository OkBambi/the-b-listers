using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaveColorLockMonk : MonoBehaviour
{
    [SerializeField] public int ColorLockTimer;
    [SerializeField] LockColorChange LockColorChange;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER ENTERED");
        IColorLock colorLock = GameManager.instance.playerScript.GetComponent<IColorLock>();
        if (colorLock != null)
        {
            colorLock.LockColorSelection(ColorLockTimer);
        }
        LockColorChange.SwapChainColorToMonk();

        GameManager.instance.ChainScreen(ColorLockTimer);
    }
}
