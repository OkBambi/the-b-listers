using UnityEngine;

public class Splats : MonoBehaviour
{

    public float minSize;
    public float maxSize;

    public Sprite[] sprites;

    private SplatLocation splatLocation;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(SplatLocation _splatLocation)
    {
        splatLocation = _splatLocation;
        SetSprite();
        SetSize();
        SetRotation();
        SetProperties();
    }

    private void SetSprite()//need to update later to have it determine what color sprite it needs
    {
        int randIndex = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[randIndex];
    }

    private void SetSize()
    {
        float sizeMult = Random.Range(minSize, maxSize);
        transform.localScale *= sizeMult;
    }

    private void SetRotation()//may need to remove due to problems
    {
        float randRotation = Random.Range(-360f, 360f);
        transform.rotation = Quaternion.Euler(0f,0f, randRotation);
    }

    private void SetProperties()
    {
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;//to make sure the splats do not hang off the edge.
        spriteRenderer.sortingOrder = 3;//makes sure it is on top
    }
}
