using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EasingLibrary;

public class SpawnIndicator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //Enemy model is spawned, and they are visible. But they are pure WHITE, with a glossy material (make a temp one, ill shader the hell out of it later)
    //Enemy model currently lacks any AI, is static, and you can move through it(no collider)
    //Other enemies will still try to avoid it though, so boids, monks, etc
    //Enemy model will begin with blink, flashing 3 times before

    //How i think I'm going to do this is that this prefab will bethe one to actually spawn the enemies,
    //and the enemy manager will instantiate and control these.
    [Header("Rendering")]
    public Renderer indicatorRenderer;
    [SerializeField] MeshFilter meshFilter;
    public GameObject modelFrame;
    public Mesh enemyMesh; //this will be set when this is instantiated

    [Header("Enemy")]
    [SerializeField] List<Mesh> enemyMeshList;
    public GameObject enemyToSpawn;
    public PrimaryColor colour;

    [Header("Canvas")]
    [SerializeField] GameObject canvas;
    [SerializeField] Image boxImage;
    [SerializeField] bool canvasLookPlayer;


    [Header("Animation")]
    [SerializeField] Vector3 startScale;
    [SerializeField] Vector3 currentScale;
    //[SerializeField] Vector3 endScale;
    [SerializeField] float shrinkSpeed;
    [SerializeField] float shrinkTime;
    [SerializeField] float pauseTime = 1f;

    [SerializeField] float alpha = 0f;
    [SerializeField] Color baseColour;

    [Header("Sound")]
    [SerializeField] AudioSource spawnSfx;

    [Header("Particles")]
    [SerializeField] GameObject absorbObject;
    [SerializeField] ParticleSystem absorb;
    //[SerializeField] Color flashColour;
    //private Material[] flashMats;

    //[SerializeField] float flashSpeed = 0.01f;
    //private float red;
    //private float green;
    //private float blue;

    //private int flashIndex = 0;



    public void SetMesh(Mesh newMesh)
    {
        meshFilter.sharedMesh = enemyMesh;
    }

    private void Awake()
    {
        colour = (PrimaryColor)Random.Range(0, 3);

        switch (colour)
        {
            case PrimaryColor.RED:
                indicatorRenderer.material.color = Color.red;
                //nameStr = "Red";
                break;
            case PrimaryColor.YELLOW:
                indicatorRenderer.material.color = Color.yellow;
                //nameStr = "Yellow";
                break;
            case PrimaryColor.BLUE:
                indicatorRenderer.material.color = Color.blue;
                //nameStr = "Blue";
                break;
            case PrimaryColor.OMNI:
            default:
                indicatorRenderer.material.color = Color.black;
                //nameStr = "Omni";
                break;
        }

        //scale
        shrinkTime *= (1f - shrinkSpeed);
        transform.localScale = startScale;
        currentScale = startScale;

        absorbObject = Instantiate(ParticleManager.instance.absorbEffect, transform.position, transform.rotation, transform);
        absorb = absorbObject.GetComponent<ParticleSystem>();
        absorbObject.transform.localScale = startScale;


        //colour
        baseColour = indicatorRenderer.material.color;
        boxImage.color = baseColour;

        var mainModule = absorb.main;
        mainModule.startColor = baseColour;
        mainModule.duration = 0.5f;
        absorb.Play();

        baseColour.a = alpha;
        indicatorRenderer.material.color = baseColour;

        //animations
        StartCoroutine(Shrink());
        canvasLookPlayer = true;
        StartCoroutine(CanvasLookPlayer());
        AudioManager.instance.Play("Enemy_Spawn", Random.Range(1.25f, 1.75f), spawnSfx);
        //spawnSfx = AudioManager.
        //red = baseColour.r;
        //green = baseColour.g;
        //blue = baseColour.b;

        //flashMats = new Material[indicatorRenderer.materials.Length];
        //for (int materialIndex = 0; materialIndex < indicatorRenderer.materials.Length; ++materialIndex)
        //{
        //    flashMats[materialIndex] = new Material(EnemyManager.instance.spawnMat);
        //}
        //indicatorRenderer.materials = flashMats; 
        //StartCoroutine(Flash());

    }

    IEnumerator Shrink()
    {
        float timer = 0f;
        bool hasSpawned = false;
        while (timer <= shrinkTime)
        {
            currentScale.x = EasingLibrary.EaseOutBounce(currentScale.x, 0f, shrinkSpeed);
            currentScale.y = EasingLibrary.EaseOutBounce(currentScale.y, 0f, shrinkSpeed);
            currentScale.z = EasingLibrary.EaseOutBounce(currentScale.z, 0f, shrinkSpeed);

            alpha = EasingLibrary.EaseOutBounce(alpha, 1f, shrinkSpeed);

            //alpha = timer / shrinkTime;
            //Debug.Log(alpha);

            baseColour.a = alpha;
            indicatorRenderer.material.color = baseColour;

            transform.localScale = currentScale;
            if (absorbObject)
                absorbObject.transform.localScale = currentScale;
            timer += Time.deltaTime;

            if (!hasSpawned && currentScale.x < 0.1f)
            {
                hasSpawned = true;
                EnemyBase enemy = Instantiate(enemyToSpawn, transform.position, transform.rotation).GetComponent<EnemyBase>();
                enemy.setColor = colour;
                if (LevelModifierManager.instance.doubleEnemies)
                    Instantiate(enemyToSpawn, transform.position + new Vector3(Random.Range(-4f, 4f), 0f, Random.Range(-4f, 4f)), transform.rotation);

                AudioManager.instance.Play("Enemy_Spawn", Random.Range(1.4f, 1.6f), spawnSfx);
                StartCoroutine(CameraShake.instance.ShakeWithDistance(0.5f, 0.3f, gameObject));
            }

            yield return new WaitForFixedUpdate();
        }

        transform.localScale = Vector3.zero;
        yield return new WaitForSecondsRealtime(1f);
        if (enemyToSpawn.name == "Goliath")
        {
            EnemyManager.instance.spawnList.RemoveAt(0);
        }

        Destroy(gameObject);

        yield return null;
    }

    IEnumerator CanvasLookPlayer()
    {
        while (canvasLookPlayer)
        {
            canvas.transform.LookAt(GameManager.instance.player.transform.position);
            yield return new WaitForFixedUpdate();
        }
    }

}
