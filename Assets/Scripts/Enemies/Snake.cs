using UnityEngine;

//CHANGE COMMENTS WHEN CODE CHANGES PLZ
public class Snake : MonoBehaviour
{
    //HEALTH


    //HEAD
    //adjust this to control the spinning speed
    public float spinningSpeed = 50f;
    //public float otherrotationspeed = 50f;

    //MOVEMENT/ROAM - trying with waypoint (obstacle avoidance + flocking)
    public float movementSpeed = 2f;
    public float rotationSpeed = 5f;
    public float wanderingRadius = 5f;
    public float wanderingTiner = 2f;


    void Start()
    {
        
    }

    
    void Update()
    {
        //HEAD
        //modify the Vector3.forward in the transform.Rotate() method to rotate around different axes (e.g., Vector3.up for y-axis, Vector3.right for x-axis).
        //rotates the parent/snake object around the z-axis
        transform.Rotate(Vector3.right * spinningSpeed * Time.deltaTime);
        //we can also add individual rotation to the children here if needed:
        //foreach (Transform child in transform)
        //{
        //    child.Rotate(Vector3.forward * otherrotationspeed * Time.deltaTime);
        //}

        //MOVEMENT
        
    }

    //moves around the map
    

    //follows the player when in view


    //attacking player


    //health

}
