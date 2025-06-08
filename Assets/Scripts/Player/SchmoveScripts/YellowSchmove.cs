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
    [SerializeField] float slowMod;
    [SerializeField] int railgunDmg;
    [SerializeField] int railgunKnockback;
    [Space]
    [Header("Time")]
    [SerializeField] float originalTimeScale;

    bool activated;

    void Start()
    {
        originalTimeScale = Time.timeScale;
        player = GameObject.FindFirstObjectByType<Player>();
        ChargeGaugeUI.fillMethod = Image.FillMethod.Radial360;
    }

    void Update()
    {

        if (activated)
        {
            Time.timeScale = originalTimeScale / slowMod;
            //charging the railgun
            chargeTime += Time.deltaTime * 2;
            player.canAction = false;
            

            if (chargeTime >= chargeLevelDuration[Mathf.Clamp(chargeLevel, 0, chargeLevelDuration.Count - 1)])
            {
                chargeTime = 0f;

                //if (ComboManager.instance.currentScore >= chargeLevel * 100)x
                AudioManager.instance.Play("Yellow_Charge");
                chargeLevel = Mathf.Clamp(++chargeLevel, 0, chargeLevelDuration.Count);
                ChargeCounterUI.text = chargeLevel.ToString();
                ChargeCounterUI.fontSize = 50 + 50 * chargeLevel;
            }

            if (chargeLevel == 3)
            {
                ChargeGaugeUI.fillAmount = 1f;
                ChargeCounterUI.color = Color.yellow;
            }
            else
            {
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

                    ComboManager.instance.AddScore(-100 * chargeLevel); //may need to change this later
                    StartCoroutine(player.gameObject.GetComponent<Schmoves>().UpdateCoolDownUI());
                }
                //ui resetting
                ChargeCounterUI.color = Color.white;
                ChargeCounterUI.text = "0";
                ChargeCounterUI.fontSize = 50;
                ChargeGaugeUI.gameObject.SetActive(false);

                Time.timeScale = originalTimeScale;
                chargeLevel = 0;
                player.canAction = true;
                activated = false;
            }
        }
    }

    public void Activate()
    {
        //score check
        //if (ComboManager.instance.currentScore >= 100)
        ChargeGaugeUI.gameObject.SetActive(true);
        chargeTime = 0;
        activated = true;
        
    }
}
