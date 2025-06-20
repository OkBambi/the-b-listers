using UnityEngine;

public class Snake : MonoBehaviour
{
    //HEAD
    //adjust this to control the spinning speed
    public float rotationSpeed = 50f;
    //public float otherrotationspeed = 50f;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        //HEAD
        //modify the Vector3.forward in the transform.Rotate() method to rotate around different axes (e.g., Vector3.up for y-axis, Vector3.right for x-axis).
        //rotates the parent/snake object around the z-axis
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        //we can also add individual rotation to the children here if needed:
        //foreach (Transform child in transform)
        //{
        //    child.Rotate(Vector3.forward * otherrotationspeed * Time.deltaTime);
        //}
    }

    //moves around the map


    //follows the player when in view


    //attacking player


    //health

}
