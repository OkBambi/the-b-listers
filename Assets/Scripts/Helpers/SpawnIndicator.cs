using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIndicator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //Enemy model is spawned, and they are visible. But they are pure WHITE, with a glossy material (make a temp one, ill shader the hell out of it later)
    //Enemy model currently lacks any AI, is static, and you can move through it(no collider)
    //Other enemies will still try to avoid it though, so boids, monks, etc
    //Enemy model will begin with blink, flashing 3 times before

    //How i think I'm going to do this is that this prefab will bethe one to actually spawn the enemies,
    //and the enemy manager will instantiate and control these.
    public Renderer indicatorRenderer;
    [SerializeField] MeshFilter meshFilter;
    public GameObject modelFrame;
    public Mesh enemyMesh; //this will be set when this is instantiated

    [SerializeField] List<Mesh> enemyMeshList;
    public GameObject enemyToSpawn;

    [SerializeField] Color baseColour;
    [SerializeField] Color flashColour;
    private Material[] flashMats;

    [SerializeField] float flashSpeed = 0.01f;
    private float red;
    private float green;
    private float blue;

    private int flashIndex = 0;

    public void SetMesh(Mesh newMesh)
    {
        meshFilter.sharedMesh = enemyMesh;
    }

    private void Awake()
    {
        red = baseColour.r;
        green = baseColour.g;
        blue = baseColour.b;

        flashMats = new Material[indicatorRenderer.materials.Length];
        for (int materialIndex = 0; materialIndex < indicatorRenderer.materials.Length; ++materialIndex)
        {
            flashMats[materialIndex] = new Material(EnemyManager.instance.spawnMat);
        }
        indicatorRenderer.materials = flashMats; 
        StartCoroutine(Flash());
    }


    IEnumerator Flash()
    {
        while (isActiveAndEnabled)
        {
            if (red == flashColour.r && green == flashColour.g && blue == flashColour.b)
            {
                red = baseColour.r;
                green = baseColour.g;
                blue = baseColour.b;
                ++flashIndex;
                if(flashIndex >= 3)
                {
                    Instantiate(enemyToSpawn, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            }

            red = Mathf.Lerp(red, flashColour.r, flashSpeed);
            if (Mathf.Abs(red - flashColour.r) <= 0.05f)
            {
                red = flashColour.r;
            }

            green = Mathf.Lerp(green, flashColour.g, flashSpeed);
            if (Mathf.Abs(green - flashColour.g) <= 0.05f)
            {
                green = flashColour.g;
            }

            blue = Mathf.Lerp(blue, flashColour.b, flashSpeed);
            if (Mathf.Abs(blue - flashColour.b) <= 0.05f)
            {
                blue = flashColour.b;
            }

            foreach (Material material in indicatorRenderer.materials)
            {
                material.color = new Color(red, green, blue);
            }
            
            yield return null;
        }
        AudioManager.instance.Play("Enemy_Spawn");
    }
     
}
