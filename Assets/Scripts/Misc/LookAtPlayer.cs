using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.LookAt(GameManager.instance.player.transform);
    }
}
