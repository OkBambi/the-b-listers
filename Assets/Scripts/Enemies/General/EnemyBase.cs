using System.Collections;
using Unity.VisualScripting;
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
        //this code is being refactored, when an enemy spawns, it should add its own rigid body to the boidreferences rigidbody list in the enemy manager
        //this rigid body list is what all boids will use

        EnemyManager.instance.boidReferences.Add(GetComponent<Rigidbody>());
        //BoidAI[] activeboids = FindObjectsByType<BoidAI>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        //for (int boidCount = 0; boidCount < activeboids.Length; boidCount++)
        //{
        //    activeboids[boidCount].boids.Add(GetComponent<Rigidbody>());
        //}
    }

    //CALL THIS METHOD IN THE DEATH OF ALL ENEMIES
    protected void RemoveSelfFromTargetList()
    {
        EnemyManager.instance.boidReferences.Remove(GetComponent<Rigidbody>());
        //BoidAI[] activeboids = FindObjectsByType<BoidAI>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        //for (int boidCount = 0; boidCount < activeboids.Length; boidCount++)
        //{
        //    activeboids[boidCount].boids.Remove(GetComponent<Rigidbody>());
        //}
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
        float scoreWithMult = ComboManager.instance.getScoreTimesMult(score);
        ComboFeed.theInstance.AddNewComboFeed("+ " + scoreWithMult.ToString() + " " + gameObject.name, scoreWithMult);
    }

    public virtual void takeDamage(PrimaryColor hitColor, int amount)
    {
        if (hitColor == setColor || hitColor == PrimaryColor.OMNI || setColor == PrimaryColor.OMNI)
        {
            hp -= amount;
            if (isAlive)
                DeathCheck();

            //flash white
            StartCoroutine(Flash());
            StartCoroutine(ShakePos(0.2f, 0.5f));
            StartCoroutine(ShakeSize(0.2f, 0.1f));

            if (hitVfx)
                Instantiate(hitVfx, transform.position, Quaternion.identity);

            spawnColorParticles();

        }
    }

    public virtual void DeathCheck()
    {
        if (hp <= 0)
        {
            isAlive = false;
            OnAECDestroy();
            RemoveSelfFromTargetList();
            AudioManager.instance.Play("Enemy_Death");
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

    public IEnumerator ShakePos(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;
        float _x;
        float _y;

        while (elapsed < duration)
        {
            if (Time.timeScale == 0f) yield break;
            _x = Random.Range(-1f, 1f) * magnitude;
            _y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(_x, _y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }

    public IEnumerator ShakeSize(float duration, float magnitude)
    {
        Vector3 originalSize = transform.localScale;
        float elapsed = 0.0f;
        float _x;
        float _y;

        while (elapsed < duration)
        {
            if (Time.timeScale == 0f) yield break;
            _x = Random.Range(-1f, 1f) * magnitude;
            _y = Random.Range(-1f, 1f) * magnitude;

            transform.localScale = originalSize + new Vector3(_x, _y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localScale = originalSize;
    }

    private void spawnColorParticles()//spawn colors splats from the enemy
    {
        ParticleManager.instance.colorParticles.transform.position = transform.position;

        var colorParticles = ParticleManager.instance.colorParticles.main;
        if (setColor == PrimaryColor.RED)
        {
            colorParticles.startColor = Color.red;
            ParticleManager.instance.colorParticles.GetComponent<ColorParticles>().startColor = Color.red;
        }
        else if (setColor == PrimaryColor.BLUE)
        {
            colorParticles.startColor = Color.blue;
            ParticleManager.instance.colorParticles.GetComponent<ColorParticles>().startColor = Color.blue;
        }
        else if (setColor == PrimaryColor.YELLOW)
        {
            colorParticles.startColor = Color.yellow;
            ParticleManager.instance.colorParticles.GetComponent<ColorParticles>().startColor = Color.yellow;
        }
        else if (setColor == PrimaryColor.OMNI)
        {
            colorParticles.startColor = Color.black;
            ParticleManager.instance.colorParticles.GetComponent<ColorParticles>().startColor = Color.black;
        }
        ParticleManager.instance.colorParticles.Play();
    }
}
