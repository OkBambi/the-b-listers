using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Monolith : EnemyBase
{
    [SerializeField] CharacterController controller;
    [SerializeField] GameObject myBoid;
    [SerializeField] int speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ColorSelection(setColor);

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

        moveDir = (Input.GetAxis("Horizontal") * transform.right)
                + (Input.GetAxis("Vertical") * transform.forward);

        controller.Move(moveDir * speed * Time.deltaTime);

        //jump

        controller.Move(playerVel * Time.deltaTime);


        //transform.position += moveDir * speed * Time.deltaTime;
    }
}
