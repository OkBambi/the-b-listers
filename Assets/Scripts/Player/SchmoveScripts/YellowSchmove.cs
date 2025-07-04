using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YellowSchmove : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Player player;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform shootingPoint;
    [Space]
    [Header("SchmoveObjects")]
    [SerializeField] GameObject YELLOWBEAAAM;
    [SerializeField] Image ChargeGaugeUI;
    [SerializeField] TextMeshProUGUI ChargeCounterUI;

    [Space]
    [Header("Charge")]
    [SerializeField] float chargeTime;
    [SerializeField] int chargeLevel;
    [SerializeField] List<float> chargeLevelDuration;
    [Space]
    [Header("Stats")]
    [SerializeField] int railgunDmg;
    [SerializeField] int railgunKnockback;
    [SerializeField] Animator mAnimatorLeft;
    [SerializeField] Animator mAnimatorRight;

    CameraShake camShaker;
    PlayerArm arm;

    bool activated;

    void Start()
    {
        player = GameManager.instance.playerScript;
        rb = player.GetComponentInChildren<Rigidbody>();
        shootingPoint = GameManager.instance.shootingPoint;
        camShaker = GameObject.FindFirstObjectByType<CameraShake>();
        arm = GameObject.FindFirstObjectByType<PlayerArm>();

        ChargeGaugeUI.fillMethod = Image.FillMethod.Radial360;
    }

    void Update()
    {

        if (activated)
        {
            //charging the railgun
            chargeTime += Time.deltaTime;
            player.canAction = false;

            //increasing the charge level
            if (chargeTime >= chargeLevelDuration[Mathf.Clamp(chargeLevel, 0, chargeLevelDuration.Count - 1)] && ComboManager.instance.GetScore() >= 100 * (chargeLevel + 1))
            {
                chargeTime = 0f;
                mAnimatorRight.SetTrigger("RailGunUP");

                //if (ComboManager.instance.currentScore >= chargeLevel * 100)x
                AudioManager.instance.Play("Yellow_Charge");
                chargeLevel = Mathf.Clamp(++chargeLevel, 0, chargeLevelDuration.Count);
                ChargeCounterUI.text = chargeLevel.ToString();
                ChargeCounterUI.fontSize = 50 + 50 * chargeLevel;
                if (chargeLevel == 2)
                {
                    AudioManager.instance.Play("Yellow_Chargelvl2");
                    AudioManager.instance.Stop("Yellow_Charge");
                }
            }

            if (chargeLevel == 3)
            {
                AudioManager.instance.Stop("Yellow_Charge");
                ChargeGaugeUI.fillAmount = 1f;
                ChargeCounterUI.color = Color.yellow;
            }
            else
            {
                AudioManager.instance.Play("Yellow_Chargelvl3");
                ChargeGaugeUI.fillAmount = chargeTime / chargeLevelDuration[chargeLevel];
            }


            //IMMA FIRRIN MY LAZER (Railgun)
            if (Input.GetMouseButtonUp(1))
            {
                if (chargeLevel != 0) //you need to charge the railgun bro
                {
                    YellowRailgunHitbox beam = Instantiate(YELLOWBEAAAM, shootingPoint.position + 2 * shootingPoint.forward, shootingPoint.rotation).GetComponentInChildren<YellowRailgunHitbox>();

                    beam.transform.localScale = new Vector3(Mathf.Clamp(chargeLevel * 2, 1f, 50f), 100, Mathf.Clamp(chargeLevel * 2, 1f, 50f));
                    beam.railgunDmg = chargeLevel * railgunDmg;
                    rb.AddForce(shootingPoint.forward * chargeLevel * railgunKnockback, ForceMode.Impulse);
                    AudioManager.instance.Play("Yellow_Fire");
                    ComboManager.instance.RemoveScore(100 * chargeLevel); //may need to change this later
                    ComboFeed.theInstance.AddNewComboFeed("- " + (100 * chargeLevel).ToString() + " yellowSchmove", (100 * chargeLevel));//same here
                    StartCoroutine(GameManager.instance.schmover.UpdateCoolDownUIYellow());
                    StartCoroutine(camShaker.ShakeTween(1f, 0.04f * chargeLevel, 0f, 0.25f));
                    StartCoroutine(arm.RecoilTween(1f, 0.001f * chargeLevel, 0.01f * chargeLevel, 0.25f));
                }
                //ui resetting
                ChargeCounterUI.color = Color.white;
                ChargeCounterUI.text = "0";
                ChargeCounterUI.fontSize = 50;
                ChargeGaugeUI.gameObject.SetActive(false);

                mAnimatorRight.SetTrigger("RailGunDOWN");

                chargeLevel = 0;
                player.canAction = true;
                activated = false;
            }
        }
        mAnimatorRight.SetTrigger("RailGunDOWN");
    }

    public void Activate()
    {
        //score check
        ChargeGaugeUI.gameObject.SetActive(true);
        chargeTime = 0;
        activated = true;
        StartCoroutine(ChargeShake());

    }

    IEnumerator ChargeShake()
    {
        while (activated)
        {
            StartCoroutine(camShaker.Shake(0.1f, 0.03f * chargeLevel));
            StartCoroutine(arm.Recoil(0.1f, 0.005f * chargeLevel, 0.005f * chargeLevel));
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
