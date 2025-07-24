using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaveCollider : MonoBehaviour
{
    [SerializeField] public int ColorLockTimer;
    [SerializeField] ChainUIMonk LockColorChange;

    [Header("BoogieWoogie")]
    [SerializeField] float ShockTime;
    [SerializeField] float LaunchHight;
    private PrimaryColor waveColor;
    [SerializeField] private ChainUIMonkBoss chainUI;
    [SerializeField] private ChainUIMonk MonkChainUI;
    [SerializeField] private GameStartDagger gameStartDagger;

    [SerializeField] Monk_MiniBoss MMB;
    [SerializeField] Monk monkboy;


    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(CameraShake.instance.Shake(0.25f, 1f));
        if (LevelModifierManager.instance.boogieWoogie)
        {
            GameManager.instance.playerScript.canAction = false; // Disable player actions
            GameManager.instance.playerScript.canSchmove = false;
            // You can add player damage logic here
            GameManager.instance.player.GetComponent<Rigidbody>().AddForce(Vector3.up * LaunchHight, ForceMode.Impulse); // Example force to push the player up
            if (gameStartDagger.DaggerGot)
            {
                Invoke("resetPlayer", ShockTime);
            }
        }
        else
        {
            if (MMB != null || monkboy != null)
            {
                if (MMB)
                {
                    chainUI.SwapChainColor(MMB.setColor);
                }

                if (monkboy)
                {
                    MonkChainUI.SwapChainColor(monkboy.setColor);
                }
            }
            IColorLock colorLock = GameManager.instance.playerScript.GetComponent<IColorLock>();
            if (colorLock != null)
            {
                colorLock.LockColorSelection(ColorLockTimer);
            }
            GameManager.instance.ChainScreen(ColorLockTimer);
            ChainEvents.LockVideoChain();

            StartCoroutine(UnlockAfterDelay(ColorLockTimer));
        }
    }

    private IEnumerator UnlockAfterDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        ChainEvents.UnlockVideoChain();
    }

    private void resetPlayer()
    {
        GameManager.instance.playerScript.canAction = true; // Re-enable player actions
        GameManager.instance.playerScript.canSchmove = true;
    }
}
