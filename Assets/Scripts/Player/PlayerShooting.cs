using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] int range;
    [SerializeField] int damage;
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

    public void Initialize()
    {
        
    }

    public void UpdateWeapon()
    {
        if(Input.GetMouseButton(0))
        {
            isHolding = true;
            holdTime += Time.deltaTime;

            if(holdTime > tapThreshold)
            {
                //spray
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            isHolding = false;

            if(holdTime <= tapThreshold)
            {
                //shotgun
            }

            holdTime = 0;
        }
    }
}
