using UnityEngine;

public class SacGroundDetection : MonoBehaviour
{
    string groundTag = "Ground";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == groundTag)
        {
            Debug.Log("Ground detected");
        }
    }
}
