using UnityEngine;

public class SacGroundDetection : EnemyBase
{
    //[SerializeField] Renderer model;
  
    string groundTag = "groundTag";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("test check");
        if (collision.gameObject.tag == groundTag)
        {
            Debug.Log("Ground detected");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        ComboManager.instance.AddScore(score);
    }
}
