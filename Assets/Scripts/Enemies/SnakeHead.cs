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
                // Call the takeDamage method on the object that implements IDamage.
                // This will trigger the player's Die() method directly as per your IDamage implementation.
                // The 'damage' parameter might not be directly used by the player's takeDamage if it just calls Die(),
                // but it's passed for consistency with the IDamage interface.
                damageableObject.takeDamage(PrimaryColor.OMNI, damage);
                Debug.Log(transform.name + " hit " + hitCollider.name + " causing them to 'Fall'!");

                // You might want to break here if each head can only hit one target per attack cycle
                // or if hitting one part of the player triggers the whole player's death logic.
                // If the player truly 'dies' on any hit, breaking here prevents redundant calls.
                // break;
            }
        }
    }
}
