using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamage
{
    public Renderer model;
    public PrimaryColor setColor;

    [SerializeField] GameObject hitVfx;

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
    protected bool isAlive = true;

    string nameStr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        ColorSelection(setColor);
        UpdateBoidAwareness();
    }

    protected void RandomizeColor()
    {
        setColor = (PrimaryColor)Random.Range(0, 3);
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

    //CALL THIS METHOD IN THE DEATH OF ALL ENEMIES
    protected void RemoveSelfFromTargetList()
    {
        BoidAI[] activeboids = FindObjectsByType<BoidAI>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        for (int boidCount = 0; boidCount < activeboids.Length; boidCount++)
        {
            activeboids[boidCount].boids.Remove(GetComponent<Rigidbody>());
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
            if (isAlive)
                DeathCheck();

            //flash white
            StartCoroutine(Flash());
            StartCoroutine(Shake(0.2f, 0.1f));
            StartCoroutine(GrowAndShrink(0.2f, 0.05f));

            if (hitVfx)
                Instantiate(hitVfx, transform.position, Quaternion.identity);
        }
    }

    public virtual void DeathCheck()
    {
        if (hp <= 0)
        {
            isAlive = false;
            OnAECDestroy();
            RemoveSelfFromTargetList();
            Destroy(gameObject);
            return;
        }
    }

    public IEnumerator Flash()
    {
        model.materials = flashMats;
        yield return new WaitForSeconds(0.05f);
        model.materials = matList;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float _x = Random.Range(-1f, 1f) * magnitude;
            float _y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition += new Vector3(_x, _y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }

    public IEnumerator GrowAndShrink(float duration, float magnitude)
    {
        Vector3 originalSize = transform.localScale;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float _x = Random.Range(-1f, 1f) * magnitude;
            float _y = Random.Range(-1f, 1f) * magnitude;

            transform.localScale += new Vector3(_x, _y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localScale = originalSize;
    }
}
