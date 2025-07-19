using System.Collections;
using UnityEngine;
using static EasingLibrary;

public class SnakeOrb : EnemyBase
{
    public SnakeHead_MiniBoss parentHead;
    [SerializeField] Rigidbody rb;

    [SerializeField] float popMagnitude;
    [SerializeField] float timeToReturn = 5f;
    [SerializeField] float magnetStrength = 0.2f;
    [SerializeField] int magnetFrames = 100;

    [SerializeField] Vector3 magnetizedSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        if (LevelModifierManager.instance.lessHealth)
        {
            hp = hp / 2;
        }
        ColorSelection(parentHead.setColor);
        UpdateBoidAwareness();

        rb.AddForce(new Vector3(Random.Range(-1f, 1f), 3f, Random.Range(-1f, 1f)) * popMagnitude, ForceMode.Impulse);

        Invoke("ReturnToParentHead", timeToReturn);
        name = "Snake Orb";
    }

    private void ReturnToParentHead()
    {
        rb.Sleep();
        StartCoroutine(MagnetizeToHead());
    }

    IEnumerator MagnetizeToHead()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        float xSize = transform.localScale.x;
        float ySize = transform.localScale.y;
        float zSize = transform.localScale.z;
        int count = 0;
        while (count < magnetFrames)
        {
            x = EasingLibrary.EaseInExpo(x, parentHead.transform.position.x, magnetStrength);
            y = EasingLibrary.EaseInExpo(y, parentHead.transform.position.y, magnetStrength);
            z = EasingLibrary.EaseInExpo(z, parentHead.transform.position.z, magnetStrength);

            xSize = EasingLibrary.EaseInExpo(xSize, magnetizedSize.x, magnetStrength);
            ySize = EasingLibrary.EaseInExpo(ySize, magnetizedSize.y, magnetStrength);
            zSize = EasingLibrary.EaseInExpo(zSize, magnetizedSize.z, magnetStrength);
            transform.position = new Vector3(x, y, z);  
            transform.localScale = new Vector3(xSize, ySize, zSize);
            ++count;
            yield return new WaitForFixedUpdate();
        }

        parentHead.col.enabled = true;
        parentHead.killBox.SetActive(true);
        parentHead.model.gameObject.SetActive(true);
        parentHead.trail.enabled = true;
        parentHead.hp += Mathf.CeilToInt((float)parentHead.maxHp / 3f);

        isAlive = false;
        parentHead.orbs.Clear();
        Destroy(gameObject);
        RemoveSelfFromTargetList();

        yield return null;
    }

    public override void DeathCheck()
    {
        if (hp <= 0)
        {
            isAlive = false;
            RemoveSelfFromTargetList();
            AudioManager.instance.Play("Enemy_Death");
            parentHead.orbs.Remove(gameObject);
            Destroy(gameObject);
            if (parentHead.orbs.Count <= 0)
            {
                parentHead.Death();
            }
            return;
        }
    }
}
