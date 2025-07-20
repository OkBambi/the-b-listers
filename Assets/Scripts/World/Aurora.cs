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
        Debug.Log(auroraMaterial.GetInt("_ColorSwap"));
        sinTime = Mathf.Sin(Time.time);
        Debug.Log(sinTime);
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
