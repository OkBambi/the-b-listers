using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] int range;
    [SerializeField] int damage;

    [Header("Spray")]
    [SerializeField] float fireRate = 0.14f;

    [Header("Shotgun")]
    [SerializeField] float shotCooldown = 0.25f;
    [SerializeField] int bulletAmount;
    [SerializeField] float spreadAngle = 25f;

    float shootTimer;

    public void Initialize()
    {
        
    }

    public void UpdateWeapon()
    {
        
    }
}
