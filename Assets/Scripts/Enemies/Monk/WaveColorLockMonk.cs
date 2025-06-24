using UnityEngine;

public class WaveColorLockMonk : MonoBehaviour
{
    [SerializeField] GameObject WaveInstance;
    [SerializeField] float ColorLockTimer;



    private void OnTriggerEnter(Collider other)
    {
        IColorLock colorLock = GameManager.instance.playerScript.GetComponent<IColorLock>();
            colorLock.LockColorSelection(ColorLockTimer);

    }

    private void OnTriggerExit(Collider other)
    {
        IColorLock colorLock = GetComponent<IColorLock>();

    }
}
