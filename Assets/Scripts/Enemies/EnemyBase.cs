using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Renderer model;
    public PrimaryColor setColor;
    [Space]
    public int hp;
    public int score = 50;

    string nameStr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ColorSelection(setColor);
        UpdateBoidAwareness();
    }

    protected void ColorSelection(PrimaryColor newColor)
    {
        setColor = newColor;
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

    //CALL THIS METHOD IN THE START OF ALL ENEMIES
    protected void UpdateBoidAwareness()
    {
        //this will update all other active boids with this current boid
        //for enemies that dont care about boids, they need to update all the boids,
        //but they wont have a boid list themselves, so they dont care to begin with

        //this basically means that all enemies will need to call this function on startup so that boids care about boids and other enemies,
        //but non boids dont care

        BoidAI[] activeboids = FindObjectsByType<BoidAI>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        for (int boidCount = 0; boidCount < activeboids.Length; boidCount++)
        {
            activeboids[boidCount].boids.Add(GetComponent<Rigidbody>());
        }
    }

    //call this when an AEC enemy spawn
    public void OnAECAwake()
    {
        EnemyManager.instance.OnAECAwake();
    }
    //call this when an AEC enemy dies
    public void OnAECDestroy()
    {
        EnemyManager.instance.OnAECDestroy();
        ComboManager.instance.AddScore(score);
    }
}
