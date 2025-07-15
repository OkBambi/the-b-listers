using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class stopWatchShockWave : MonoBehaviour
{
    [SerializeField] GameObject shockWave;
    [SerializeField] float speed = 0.1f;
    [SerializeField] float MaxSize;
    [SerializeField] float ShockTime;
    [SerializeField] float LaunchHeight;
    [SerializeField] float KnockbackForce;

    [Header("BoogieWoogie")]
    [SerializeField] public int ColorLockTimer;
    [SerializeField]  ChainUIStopWatch ChainUIColor;

    private PrimaryColor ShockColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        ChainUIColor = GetComponent<ChainUIStopWatch>();
    }

    void Start()
    {
        StartCoroutine(myShock());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator myShock()
    {
        shockWave.SetActive(true);
        //yield return new WaitForSeconds(0.35f);
        AudioManager.instance.Play("Stopwatch_Smash");
        while (true)
        {
            yield return null;
            shockWave.transform.localScale += new Vector3(speed * Time.deltaTime, 0f, speed * Time.deltaTime);

            if (shockWave.transform.localScale.x >= MaxSize)
            {
                shockWave.transform.localScale = new Vector3(0f, 0f, 0f);
                break; // Exit the coroutine when the shock wave reaches its maximum size
            }
        }

        yield return new WaitForSeconds(3f);
        shockWave.SetActive(false);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject); // Destroy the shock wave GameObject after it reaches its maximum size
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (LevelModifierManager.instance.boogieWoogie)
            {
                IColorLock colorLock = GameManager.instance.playerScript.GetComponent<IColorLock>();
                if (colorLock != null)
                {
                    colorLock.LockColorSelection(ColorLockTimer);
                }
                ChainUIColor.SwapChainColor();

                GameManager.instance.ChainScreen(ColorLockTimer);
            }
            else
            {
                // Handle player damage or effects here
                Debug.Log("Player hit by shock wave!");
                GameManager.instance.playerScript.canAction = false; // Disable player actions
                GameManager.instance.playerScript.canSchmove = false;
                // You can add player damage logic here
                var playerRigidbody = GameManager.instance.player.GetComponent<Rigidbody>();
                if (playerRigidbody != null)
                {
                    // Apply knockback force to the player
                    Vector3 hitDirection = (other.transform.position - transform.position);
                    hitDirection.y = 0f; // Keep the knockback force horizontal for consist arches
                    hitDirection = hitDirection.normalized;

                    //apply parabolic arc: horizontal force + upward force
                    Vector3 force = hitDirection * KnockbackForce + Vector3.up * LaunchHeight;
                    playerRigidbody.AddForce(force, ForceMode.Impulse);
                }
                Invoke ("resetPlayer", ShockTime); // Reset player actions after a delay
            }


        }
    }

    private void resetPlayer()
    {
        Debug.Log("reset");
        GameManager.instance.playerScript.canAction = true; // Re-enable player actions
        GameManager.instance.playerScript.canSchmove = true;
    }

    public void LockColorSelection(float duration)
    {
        throw new System.NotImplementedException();
    }
}
