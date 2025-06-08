using System.Collections.Generic;
using UnityEngine;

public class YellowSchmove : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Player player;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform shootingPoint;
    [Space]
    [Header("SchmoveObjects")]
    [SerializeField] GameObject YELLOWBEAAAM;
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

                //if (ComboManager.instance.currentScore >= chargeLevel * 100)
                chargeLevel = Mathf.Clamp(++chargeLevel, 0, chargeLevelDuration.Count);
            }

            //IMMA FIRRIN MY LAZER (Railgun)
            if (Input.GetMouseButtonUp(1))
            {
                if (chargeLevel != 0) //you need to charge the railgun bro
                {
                    YellowRailgunHitbox beam = Instantiate(YELLOWBEAAAM, shootingPoint.position + 2 * shootingPoint.forward, shootingPoint.rotation).GetComponentInChildren<YellowRailgunHitbox>();

                    beam.transform.localScale = new Vector3(Mathf.Clamp(chargeLevel * 2, 1f, 50f), 100, Mathf.Clamp(chargeLevel * 5, 1f, 50f));
                    beam.railgunDmg = chargeLevel * railgunDmg;
                    rb.AddForce(shootingPoint.forward * chargeLevel * railgunKnockback, ForceMode.Impulse);

                    ComboManager.instance.AddScore(-100 * chargeLevel); //may need to change this later
                    StartCoroutine(player.gameObject.GetComponent<Schmoves>().UpdateCoolDownUI());
                }

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
        chargeTime = 0;
        activated = true;
    }
}
