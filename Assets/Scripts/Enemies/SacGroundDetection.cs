using UnityEngine;

public class SacGroundDetection : MonoBehaviour, IColor
{
    [SerializeField] Renderer model;
  
    string groundTag = "groundTag";

    public void SetColor(IColor.primaryColor myColor)
    {
        IColor.primaryColor = myColor;
    }

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
}
