using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] Transform shootingPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] int damage;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLifeTime;
    [SerializeField] float tapThreshold = 0.13f;

    [Header("Spray")]
    [SerializeField] float fireRate = 0.14f;

    [Header("Shotgun")]
    [SerializeField] float shotCooldown = 0.25f;
    [SerializeField] int bulletAmount;
    [SerializeField] float spreadAngle = 25f;

    float shootTimer;
    bool isHolding;
    float holdTime;

    CameraShake camShaker;

    public void Initialize()
    {
        camShaker = GameObject.FindFirstObjectByType<CameraShake>();
    }

    public void UpdateWeapon(PrimaryColor playerColor)
    {
        shootTimer += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            isHolding = true;
            holdTime += Time.deltaTime;

            if(holdTime > tapThreshold)
            {
                //spray
                if (shootTimer > fireRate)
                {
                    shootTimer = 0;
                    GameObject b = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
                    b.transform.Rotate(0, 0, Random.Range(-360, 360));
                    switch(playerColor)
                    {
                        case PrimaryColor.RED:
                            b.GetComponent<Renderer>().material.color = Color.red;
                            break;
                        case PrimaryColor.BLUE:
                            b.GetComponent<Renderer>().material.color = Color.blue;
                            break;
                        case PrimaryColor.YELLOW:
                            b.GetComponent<Renderer>().material.color = Color.yellow;
                            break;
                        default:
                            b.GetComponent<Renderer>().material.color = Color.black;
                            break;
                    }

                    StartCoroutine(camShaker.Shake(0.05f, 0.006f));
                    b.GetComponent<Dagger>().Initialize(playerColor, bulletSpeed, bulletLifeTime, ignoreLayer);
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            isHolding = false;

            if(holdTime <= tapThreshold && shootTimer > shotCooldown)
            {
                //shotgun
                shootTimer = 0;
                for (int i = 0; i < bulletAmount; i++)
                {
                    GameObject b = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
                    b.transform.Rotate(
                        Random.Range(-spreadAngle, spreadAngle), 
                        Random.Range(-spreadAngle, spreadAngle), 
                        Random.Range(-360, 360));
                    switch (playerColor)
                    {
                        case PrimaryColor.RED:
                            b.GetComponent<Renderer>().material.color = Color.red;
                            break;
                        case PrimaryColor.BLUE:
                            b.GetComponent<Renderer>().material.color = Color.blue;
                            break;
                        case PrimaryColor.YELLOW:
                            b.GetComponent<Renderer>().material.color = Color.yellow;
                            break;
                        default:
                            b.GetComponent<Renderer>().material.color = Color.black;
                            break;
                    }

                    b.GetComponent<Dagger>().Initialize(playerColor, bulletSpeed, bulletLifeTime, ignoreLayer);
                }

                StartCoroutine(camShaker.Shake(0.08f, 0.1f));
            }

            holdTime = 0;
        }
    }
}
