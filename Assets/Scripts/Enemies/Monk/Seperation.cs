using UnityEngine;

public class Seperation : MonoBehaviour
{
    [SerializeField] Monk parent;

    //triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("monkEnemy"))
        {
            parent.monkInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("monkEnemy"))
        {
            parent.monkInRange = false;
        }
    }
}
