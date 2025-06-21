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
        //SetSize();
        //SetRotation();
        //SetLocation();
    }

    private void SetSprite()
    {
        int randIndex = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[randIndex];
    }
}
