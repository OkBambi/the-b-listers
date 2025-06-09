using System.Security.Cryptography;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float bobSpeed;
    [SerializeField] float amplitude = 0.5f;

    float originalY;

    private void Start()
    {
        originalY = transform.position.y;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));

        float y = Mathf.PingPong(Time.time * bobSpeed, 1) * amplitude + originalY;
        transform.position = new Vector3(0, y, 0);
    }
}
