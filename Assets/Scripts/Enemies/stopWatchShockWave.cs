using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class stopWatchShockWave : MonoBehaviour
{

    [SerializeField] GameObject shockWave;
    [SerializeField] float speed = 0.1f;
    [SerializeField] float maxSize;
    [SerializeField] float ShockTime;
    [SerializeField] float LaunchHight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

        while (true)
        {
            yield return null;
            shockWave.transform.localScale += new Vector3(speed * Time.deltaTime, 0f, speed * Time.deltaTime);
            if(shockWave.transform.localScale.x >= maxSize)
            {
                shockWave.transform.localScale = new Vector3(0f, 0f, 0f);
                break; // Exit the coroutine when the shock wave reaches its maximum size
            }
        }
        yield return new WaitForSeconds(3f);
        Destroy(gameObject); // Destroy the shock wave GameObject after it reaches its maximum size
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle player damage or effects here
            Debug.Log("Player hit by shock wave!");
            GameManager.instance.playerScript.canAction = false; // Disable player actions
            // You can add player damage logic here
            GameManager.instance.player.GetComponent<Rigidbody>().AddForce(Vector3.up * LaunchHight, ForceMode.Impulse); // Example force to push the player up
            Invoke("resetPlayer", ShockTime);

        }
    }
    
    private void resetPlayer()
    {
        Debug.Log("reset");
        GameManager.instance.playerScript.canAction = true; // Re-enable player actions
    }

    
}
