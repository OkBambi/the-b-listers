using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaveColorLockMonk : MonoBehaviour
{
    [SerializeField] public int ColorLockTimer;
    [SerializeField] LockColorChange LockColorChange;

    [Header("BoogieWoogie")]
    [SerializeField] float ShockTime;
    [SerializeField] float LaunchHight;
    private PrimaryColor waveColor;

    private void OnTriggerEnter(Collider other)
    {
        if (LevelModifierManager.instance.boogieWoogie)
        {
            Debug.Log("Player hit by shock wave!");
            GameManager.instance.playerScript.canAction = false; // Disable player actions
            GameManager.instance.playerScript.canSchmove = false;
            // You can add player damage logic here
            GameManager.instance.player.GetComponent<Rigidbody>().AddForce(Vector3.up * LaunchHight, ForceMode.Impulse); // Example force to push the player up
            Invoke("resetPlayer", ShockTime);
        }
        else
        {
            Debug.Log("TRIGGER ENTERED");
            IColorLock colorLock = GameManager.instance.playerScript.GetComponent<IColorLock>();
            if (colorLock != null)
            {
                colorLock.LockColorSelection(ColorLockTimer);
            }
            LockColorChange.SwapChainColor(waveColor);

            GameManager.instance.ChainScreen(ColorLockTimer);
        }
    }
}
