using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Splats : MonoBehaviour
{

    public float minSize;
    public float maxSize;

    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(Color splatColor)
    {
        SetSprite(splatColor);
        SetSize();
        SetRotation();
        SetProperties();
    }

    private void SetSprite(Color splatColor)//need to update later to have it determine what color sprite it needs
    {
        int randIndex = 0;
        if (splatColor == Color.red)
        {
            randIndex = Random.Range(0, 3);
        }
        else if (splatColor == Color.blue)
        {
            randIndex = Random.Range(4, 7);
        }
        else if (splatColor == Color.yellow)
        {
            randIndex = Random.Range(8, 11);
        }
        else
        {
            randIndex = Random.Range(12, 15);
        }
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
        transform.rotation = Quaternion.Euler(90f, randRotation, 0f);
    }

    private void SetProperties()
    {
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;//to make sure the splats do not hang off the edge.
        spriteRenderer.sortingOrder = 3;//makes sure it is on top
    }
}
