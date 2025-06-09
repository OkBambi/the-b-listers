using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Monolith : EnemyBase
{
    [SerializeField] GameObject myBoid;
    [SerializeField] int speed;
    [SerializeField] float rotationRadius = 2f;
    [SerializeField] float angularSpeed = 2f;
    [SerializeField]  float posX, posZ, angle= 0f;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ColorSelection(setColor);
        rb = GetComponent<Rigidbody>();
    }

    Vector3 moveDir;
    Vector3 playerVel;

    // Update is called once per frame
    void Update()
    {
        movement();

    }

    public void SpawnBoid()
    {
        //Boid myNewBoid = Instantiate(myBoid, transform.position, Quaternion.identity).GetComponent<Boid>();
        //myNewBoid.setColor = this.setColor;
    }

    void movement()
    {
        posX = rb.position.x + Mathf.Cos(angle) * rotationRadius;
        posZ = rb.position.z + Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector3(posX, transform.position.y, posZ);
        angle += angularSpeed * Time.deltaTime;

        if (angle >= 360f)
        {
            angle = 0f;
        }
    }
}
