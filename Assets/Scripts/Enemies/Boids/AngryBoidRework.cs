using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBoidRework : BoidAI
{
    [SerializeField] List<float> chargeCooldown;
    [SerializeField] bool isNormalAI = true;
    [SerializeField] GameObject telegraphs;
    [SerializeField] GameObject redTelegraph;
    [SerializeField] GameObject whiteTelegraph;
    [SerializeField] TrailRenderer trail;

    private int chargePhase = 0;

    [Header("Charge Stats")]
    [SerializeField] float slowDownSpeed = 0.01f;
    [SerializeField] float slowDownDuration = 3f;
    [SerializeField] float chargeDuration = 3f;
    [SerializeField] float lookSpeed = 0.05f;
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float pauseDuration = 0.5f;

    float chargingTime = 0f;

    [Header("Animation Stats")]
    [SerializeField] bool isRedTelegraphShowing;
    [SerializeField] float startBlinkTime = 0.5f;

    [SerializeField] GameObject squishModel;

    Vector3 scaleOriginal;
    [SerializeField] Vector3 scaleSquished;
    protected override void Start()
    {
        scaleOriginal = model.transform.localScale;
        ColorSelection(setColor);
        switch(setColor)
        {
            case PrimaryColor.RED:
                trail.material.color = Color.red;
                break;
            case PrimaryColor.YELLOW:
                trail.material.color = Color.yellow;
                break;
            case PrimaryColor.BLUE:
                trail.material.color = Color.blue;
                break;
        }
        base.UpdateBoidAwareness();
        StartCoroutine(NoiseWeights());
        StartCoroutine(SwitchAIMode());
        name = "Angry Boid";
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
        //chargePhase = 0
        float currentTime = 0f;
        while (currentTime < slowDownDuration)
        {
            Debug.Log("slowing");
            currentTime += Time.deltaTime;
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, slowDownSpeed);
            yield return new WaitForFixedUpdate();
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
            yield return new WaitForFixedUpdate();
        }
        yield return null;
        
    }

    IEnumerator Charge()
    {
        chargingTime = 0f;
        //chargePhase = 1
        AudioManager.instance.Play("A_Boid_Charge");
        Debug.Log("charging");
        StartCoroutine(TelegraphBlink());
        while (chargingTime <= chargeDuration)
        {
            chargingTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        
        ++chargePhase;
        StartCoroutine(Dash());
        yield return null;
    }

    IEnumerator Dash()
    {
        //chargePhase = 2
        trail.enabled = true;
        Debug.Log("dashing");
        AudioManager.instance.Play("A_Boid_Dash");
        yield return new WaitForSeconds(pauseDuration);
        //chargePhase = 3
        ++chargePhase;
        //end the blinking
        StartCoroutine(ChargeSquish());
        rb.AddForce(transform.forward * dashSpeed, ForceMode.Acceleration);
        chargePhase = 0;
        yield return new WaitForSeconds(1);
        isNormalAI = !isNormalAI;
        StartCoroutine(SwitchAIMode());
        trail.enabled = false;

    }

    IEnumerator TelegraphBlink()
    {
        telegraphs.SetActive(true);
        isRedTelegraphShowing = true;
        float blinkDelay = startBlinkTime;
        while (chargePhase < 2)
        {
            AudioManager.instance.Play("A_Boid_Blinker");
            if (isRedTelegraphShowing)
            {
                redTelegraph.SetActive(true);
                whiteTelegraph.SetActive(false);
            }
            else
            {
                redTelegraph.SetActive(false);
                whiteTelegraph.SetActive(true);
            }

            yield return new WaitForSeconds(blinkDelay);
            isRedTelegraphShowing = !isRedTelegraphShowing;
            blinkDelay = Mathf.Clamp((chargeDuration - chargingTime) / (chargeDuration * 1.5f), 0f, 0.5f);
        }

        StartCoroutine(TelegraphLock());
        yield return null;

    }

    IEnumerator TelegraphLock()
    {
        redTelegraph.SetActive(true);
        whiteTelegraph.SetActive(false);
        yield return new WaitForSeconds(pauseDuration);
        telegraphs.SetActive(false);
    }

    IEnumerator ChargeSquish()
    {
        Debug.Log("squish");
        int count = 0;
        while (count < 40)
        {
            model.transform.localScale = Vector3.Lerp(model.transform.localScale, scaleSquished, 0.2f);
            ++count;
            yield return new WaitForFixedUpdate();
        }
        count = 0;

        while (count < 80)
        {
            model.transform.localScale = Vector3.Lerp(model.transform.localScale, scaleOriginal, 0.1f);
            ++count;
            yield return new WaitForFixedUpdate();
        }
        model.transform.localScale = scaleOriginal;
        Debug.Log("stop squish");
        yield return null;

    }


}
