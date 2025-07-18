using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static EasingLibrary;

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
    [Space]
    [Header("Boss Stuff")]
    public Image bossHPBar;
    public float bossHPOrig;
    public TextMeshProUGUI bossName;

    private Vector3 currentSize;
    private Vector3 originalSize;



    //string nameStr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        if (LevelModifierManager.instance.lessHealth)
        {
           hp = hp / 2;
        }
        ColorSelection(setColor);
        UpdateBoidAwareness();
    }

    protected void RandomizeColor()
    {
        setColor = (PrimaryColor)Random.Range(0, 3);
    }

    public void ColorSelection(PrimaryColor newColor)
    {
        setColor = newColor;
        switch (setColor)
        {
            case PrimaryColor.RED:
                model.material.color = Color.red;
                //nameStr = "Red";
                break;
            case PrimaryColor.YELLOW:
                model.material.color = Color.yellow;
                //nameStr = "Yellow";
                break;
            case PrimaryColor.BLUE:
                model.material.color = Color.blue;
                //nameStr = "Blue";
                break;
            case PrimaryColor.OMNI:
            default:
                model.material.color = Color.black;
                //nameStr = "Omni";
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
        EnemyManager.instance.boidReferences.Add(GetComponent<Rigidbody>());
    }

    //CALL THIS METHOD IN THE DEATH OF ALL ENEMIES
    protected void RemoveSelfFromTargetList()
    {
        EnemyManager.instance.boidReferences.Remove(GetComponent<Rigidbody>());
    }

    //call this when an AEC enemy spawn
    public void OnAECAwake()
    {
        originalSize = transform.localScale;
        currentSize = Vector3.zero;
        transform.localScale = currentSize;
        StartCoroutine(SpawnJuice());
        StartCoroutine(ShakePos(0.2f, 0.2f));

        EnemyManager.instance.OnAECAwake();
        
    }
    //call this when an AEC enemy dies
    public void OnAECDestroy()
    {
        EnemyManager.instance.OnAECDestroy();
        float scoreWithMult = ComboManager.instance.getScoreTimesMult(score);
        ComboManager.instance.AddScore(scoreWithMult);
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
            if (gameObject.name != "Goliath")
            {
                if (gameObject.name != "Monk_Mini_Boss")
                {
                    StartCoroutine(Flash());
                }
                StartCoroutine(ShakePos(0.2f, 0.5f));
                StartCoroutine(ShakeSize(0.2f, 0.1f));
            }

            if (hitVfx)
                Instantiate(hitVfx, transform.position, Quaternion.identity);

            spawnHitColorParticles();

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

    private void spawnHitColorParticles()//spawn colors splats from the enemy
    {
        ParticleManager.instance.colorParticles.transform.position = transform.position;

        var colorParticles = ParticleManager.instance.colorParticles.main;
        if (setColor == PrimaryColor.RED)
        {
            colorParticles.startColor = Color.red;
            ParticleManager.instance.colorParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial.color = Color.red;
            ParticleManager.instance.colorParticles.GetComponent<ColorParticles>().startColor = Color.red;
        }
        else if (setColor == PrimaryColor.BLUE)
        {
            colorParticles.startColor = Color.blue;
            ParticleManager.instance.colorParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial.color = Color.blue;
            ParticleManager.instance.colorParticles.GetComponent<ColorParticles>().startColor = Color.blue;
        }
        else if (setColor == PrimaryColor.YELLOW)
        {
            colorParticles.startColor = Color.yellow;
            ParticleManager.instance.colorParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial.color = Color.yellow;
            ParticleManager.instance.colorParticles.GetComponent<ColorParticles>().startColor = Color.yellow;
        }
        else if (setColor == PrimaryColor.OMNI)
        {
            colorParticles.startColor = Color.black;
            ParticleManager.instance.colorParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial.color = Color.black;
            ParticleManager.instance.colorParticles.GetComponent<ColorParticles>().startColor = Color.black;
        }

        var emmission = ParticleManager.instance.colorParticles.emission;
        if (hp > 0)//this allows the amount of particles that come out of an enemy to be less if it is not dead. 
        {
            emmission.SetBurst(0, new ParticleSystem.Burst(0f, ParticleManager.instance.hitParticleAmt));
        }
        else
        {
            emmission.SetBurst(0, new ParticleSystem.Burst(0f, ParticleManager.instance.deathParticleAmt));
        }

        ParticleManager.instance.colorParticles.Play();
    }

    public void updateBossHPBar()
    {
        bossHPBar.fillAmount = hp / bossHPOrig;
    }

    IEnumerator SpawnJuice()
    {
        float timer = 0f;
        while (timer <= 0.1f)
        {
            currentSize.x = EasingLibrary.EaseInBounce(currentSize.x, originalSize.x * 2f, 0.4f);
            currentSize.y = EasingLibrary.EaseInBounce(currentSize.y, originalSize.y * 2f, 0.4f);
            currentSize.z = EasingLibrary.EaseInBounce(currentSize.z, originalSize.z * 2f, 0.4f);

            transform.localScale = currentSize;
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        timer = 0f;
        while (timer <= 0.5f)   
        {
            currentSize.x = EasingLibrary.EaseOutBounce(currentSize.x, originalSize.x, 0.2f);
            currentSize.y = EasingLibrary.EaseOutBounce(currentSize.y, originalSize.y, 0.2f);
            currentSize.z = EasingLibrary.EaseOutBounce(currentSize.z, originalSize.z, 0.2f);

            transform.localScale = currentSize;
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return null;
        transform.localScale = originalSize;
    }
}
