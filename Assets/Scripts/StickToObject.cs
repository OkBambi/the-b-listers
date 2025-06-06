using UnityEngine;
using UnityEngine.InputSystem.HID;

public class StickToObject : MonoBehaviour
{
    public bool stuck;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stuck = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger || stuck)
        {
            return;
        }
        transform.SetParent(other.transform);
        stuck = true;
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
    }
}
