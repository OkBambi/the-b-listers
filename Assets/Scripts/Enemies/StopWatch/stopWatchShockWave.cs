using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.Rendering.DebugUI;

public class stopWatchShockWave : MonoBehaviour
{
    [SerializeField] GameObject shockWave;
    [SerializeField] float speed = 0.1f;
    [SerializeField] float MaxSize;
    [SerializeField] float ShockTime;
    [SerializeField] float LaunchHeight;
    [SerializeField] float KnockbackForce;

    [SerializeField] ParticleSystem particleEffect; // Reference to the Visual Effect component

    [Header("BoogieWoogie")]
    [SerializeField] public int ColorLockTimer;
    [SerializeField]  ChainUIStopWatch ChainUIColor;

    private PrimaryColor ShockColor;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        ChainUIColor = GetComponent<ChainUIStopWatch>();

        //Get the Visual Effect component if it exists
        particleEffect = shockWave.GetComponent<ParticleSystem>();
        if (particleEffect == null)
        {
            Debug.LogWarning("Particle Effect component not found.");
        }
    }

    void Start()
    {
        StartCoroutine(myShock());
        StartCoroutine(Shockwavesound());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator myShock()
    {
        shockWave.SetActive(true);


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

        if (particleEffect != null)
        {
            // Play the particle effect if it exists
            particleEffect.Play();
        }


        yield return new WaitForSeconds(3f);
        if (particleEffect != null)
        {
            particleEffect.Stop();
        }
        Destroy(gameObject); // Destroy the shock wave GameObject after it reaches its maximum size
    }
        IEnumerator Shockwavesound()
        {
            yield return new WaitForSeconds(0.35f);

            AudioManager.instance.Play("Stopwatch_Smash");
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
