using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBoidRework : BoidAI
{
    [SerializeField] List<float> chargeCooldown;
    [SerializeField] bool isNormalAI = true;
    [SerializeField] GameObject dashTelegraph;

    private int chargePhase = 0;

    [Header("Charge Stats")]
    [SerializeField] float slowDownSpeed = 0.01f;
    [SerializeField] float slowDownDuration = 3f;
    [SerializeField] float chargeDuration = 3f;
    [SerializeField] float lookSpeed = 0.05f;
    [SerializeField] float dashSpeed = 10f;

    protected override void Start()
    {
        ColorSelection(setColor);
        base.UpdateBoidAwareness();
        StartCoroutine(NoiseWeights());
        StartCoroutine(SwitchAIMode());
    }

    protected override void FixedUpdate()
    {
        //so whats gonna happen is that it will act like it was before, being larger and faster than a normal boid,
        //but sometimes on a coroutine call, it will switch to charge up a dash at the player

        if (isNormalAI)
            base.FixedUpdate();
    }

    IEnumerator SwitchAIMode()
    {
        Debug.Log("switching");
        yield return new WaitForSeconds(Random.Range(chargeCooldown[0], chargeCooldown[1]));
        isNormalAI = !isNormalAI;
        StartCoroutine(FacePlayer());
        StartCoroutine(SlowDown());
    }

    IEnumerator SlowDown()
    {
        float currentTime = 0f;
        while (currentTime < slowDownDuration)
        {
            Debug.Log("slowing");
            currentTime += Time.deltaTime;
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, slowDownSpeed);
            yield return null;
        }
        ++chargePhase;
        StartCoroutine(Charge());
        yield return null;
        
        
    }

    IEnumerator FacePlayer()
    {
        while (chargePhase < 2)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), lookSpeed);
            yield return null;
        }
        yield return null;
        
    }

    IEnumerator Charge()
    {
        Debug.Log("charging");
        dashTelegraph.SetActive(true);
        yield return new WaitForSeconds(chargeDuration);
        
        ++chargePhase;
        StartCoroutine(Dash());

    }

    IEnumerator Dash()
    {
        Debug.Log("dashing");
        yield return new WaitForSeconds(0.5f);
        rb.AddForce(transform.forward * dashSpeed, ForceMode.Acceleration);
        chargePhase = 0;
        yield return new WaitForSeconds(1);
        isNormalAI = !isNormalAI;
        dashTelegraph.SetActive(false);
        StartCoroutine(SwitchAIMode());

    }

}
