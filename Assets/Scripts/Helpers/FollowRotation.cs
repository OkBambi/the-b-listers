using UnityEngine;

public class FollowRotation : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followSpeed;

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * followSpeed);
    }
}
