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
    [SerializeField]private ChainUIMonkBoss chainUI;
    [SerializeField] private ChainUIMonk MonkChainUI;

    [SerializeField] Monk_MiniBoss MMB;
    [SerializeField] Monk monkboy;


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
        }

        Debug.Log(other.name);
        IColorLock colorLock = GameManager.instance.playerScript.GetComponent<IColorLock>();
        if (colorLock != null)
        {
            colorLock.LockColorSelection(ColorLockTimer);

        }


        GameManager.instance.ChainScreen(ColorLockTimer);
    }


    private void resetPlayer()
    {
        Debug.Log("reset");
        GameManager.instance.playerScript.canAction = true; // Re-enable player actions
        GameManager.instance.playerScript.canSchmove = true;
    }
}
