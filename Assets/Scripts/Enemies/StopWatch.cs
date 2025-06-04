using UnityEngine;

public class StopWatch : MonoBehaviour
{
    [SerializeField] Renderer model;
    [SerializeField] PrimaryColor setColor;
    [Space]
    [SerializeField] int hp;

    Color colorOriginal;
    string nameStr;
    int counter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        counter = 0;

        switch (setColor)
        {
            case PrimaryColor.RED:
                model.material.color = Color.red;
                nameStr = "Red";
                break;
            case PrimaryColor.YELLOW:
                model.material.color = Color.yellow;
                nameStr = "Yellow";
                break;
            case PrimaryColor.BLUE:
                model.material.color = Color.blue;
                nameStr = "Blue";
                break;
            case PrimaryColor.OMNI:
            default:
                model.material.color = Color.black;
                nameStr = "Omni";
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            CountDownTimer();
        }
    }

    void CountDownTimer()
    {
        counter++;
        if(counter == 3)
        {
            counter = 0;
            // Destroy object in use for testing
            Destroy(gameObject);

            SacSpit();
        }
    }

    void SacSpit()
    {
        
    }
}
