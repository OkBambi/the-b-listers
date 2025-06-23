using UnityEngine;

public class SnakeHead : EnemyBase
{
    [SerializeField] Snake snakeBody;
    [SerializeField] float headAttackRange = 1f; // How close the head needs to be to hit
    [SerializeField] LayerMask playerLayer; // Assign the player's layer in the Inspector


    public override void DeathCheck()
    {
        if (hp <= 0)
        {
            isAlive = false;
            ComboManager.instance.AddScore(score);
            ComboFeed.theInstance.AddNewComboFeed("+ " + score.ToString() + " " + transform.name);
            snakeBody.takeDamage(PrimaryColor.OMNI, 1);
            Destroy(gameObject);
            return;
        }
    }

    public void TryAttackPlayer(int damage)
    {
        // Check if the player is within the head's individual attack range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, headAttackRange, playerLayer);
        foreach (var hitCollider in hitColliders)
        {
            // Try to get the IDamage interface from the hit object
            IDamage damageableObject = hitCollider.GetComponent<IDamage>();

            if (damageableObject != null)
            {
                // Call the takeDamage method on the object that implements IDamage
                // Assuming player attacks are "OMNI" from the snake's perspective for simplicity
                damageableObject.takeDamage(PrimaryColor.OMNI, damage);
                Debug.Log(transform.name + " hit " + hitCollider.name + " for " + damage + " damage!");
                // Optionally, add visual/audio feedback for the hit
                // You might want to break here if each head can only hit one target per attack cycle
                // break;
            }
        }
    }

    // Optional: Draw the attack range in the editor for visualization
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, headAttackRange);
    }
}
