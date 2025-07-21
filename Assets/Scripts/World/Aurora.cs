using UnityEngine;

public class Aurora : MonoBehaviour
{
    float sinTime;
    Material auroraMaterial;
    // Update is called once per frame

    private void Start()
    {
        auroraMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }
    void Update()
    {
        sinTime = Mathf.Sin(Time.time);
        if (sinTime >= 0)
        {
            auroraMaterial.SetInt("_ColorSwap", 1);
        }
        else
        {
            auroraMaterial.SetInt("_ColorSwap", 0);
        }
    }
}
