using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamage
{
    public Renderer model;
    public PrimaryColor setColor;

    //FlashWhite
    private Color baseColor;
    private float red;
    private float green;
    private float blue;
    private Material[] matList;
    private Material[] flashMats;
    [Space]
    public int hp;
    public int score = 50;

    string nameStr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
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
        baseColor = model.material.color;

        matList = model.materials;
        flashMats = new Material[matList.Length];

        for (int materialIndex = 0; materialIndex < matList.Length; ++materialIndex)
        {
            flashMats[materialIndex] = new Material(EnemyManager.instance.flashMat);
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

    public void takeDamage(PrimaryColor hitColor, int amount)
    {
        if (hitColor == setColor || hitColor == PrimaryColor.OMNI || setColor == PrimaryColor.OMNI)
        {
            hp -= amount;
            DeathCheck();

            //flash white
            StartCoroutine(Flash());


        }
    }

    public virtual void DeathCheck()
    {
        if (hp <= 0)
        {
            OnAECDestroy();
            Destroy(gameObject);
            return;
        }
    }

    public IEnumerator Flash()
    {
        model.materials = flashMats;
        yield return new WaitForSeconds(0.05f);
        model.materials = matList;

        //honestly simple is better i think

        //    while (red != Color.white.r || green != Color.white.g || blue != Color.white.b)
        //    {
        //        red = Mathf.Lerp(red, Color.white.r, 0.1f);
        //        if (Mathf.Abs(red - Color.white.r) <= 0.05f)
        //        {
        //            red = Color.white.r;
        //        }

        //        green = Mathf.Lerp(green, Color.white.g, 0.1f);
        //        if (Mathf.Abs(green - Color.white.g) <= 0.05f)
        //        {
        //            green = Color.white.g;
        //        }

        //        blue = Mathf.Lerp(blue, Color.white.b, 0.1f);
        //        if (Mathf.Abs(blue - Color.white.b) <= 0.05f)
        //        {
        //            blue = Color.white.b;
        //        }

        //        model.material.color = new Color(red, green, blue);

        //        yield return null;
        //    }

        //    while (red != baseColor.r || green != baseColor.g || blue != baseColor.b)
        //    {
        //        red = Mathf.Lerp(red, baseColor.r, 0.1f);
        //        if (Mathf.Abs(red - baseColor.r) <= 0.05f)
        //        {
        //            red = baseColor.r;
        //        }

        //        green = Mathf.Lerp(green, baseColor.g, 0.1f);
        //        if (Mathf.Abs(green - baseColor.g) <= 0.05f)
        //        {
        //            green = baseColor.g;
        //        }

        //        blue = Mathf.Lerp(blue, baseColor.b, 0.1f);
        //        if (Mathf.Abs(blue - baseColor.b) <= 0.05f)
        //        {
        //            blue = baseColor.b;
        //        }

        //        model.material.color = new Color(red, green, blue);

        //        yield return null;
        //    }
        //    yield return null;
        //}
    }
}
