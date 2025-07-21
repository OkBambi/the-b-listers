using System.Collections;
using UnityEngine;

public class YellowRailgunHitbox : MonoBehaviour
{
    [SerializeField] YellowRailgunHitbox self;
    [SerializeField] MeshRenderer mesh1;
    [SerializeField] MeshRenderer mesh2;
    [SerializeField] Collider col;
    public int railgunDmg;
    [SerializeField] GameObject emptyParent;
    private void Awake()
    {
        Invoke("DisableSelf", 0.2f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        //check for damage
        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null)
        {
            //HIT EM
            //dmg.takeDamage(PrimaryColor.OMNI, railgunDmg);
            float rand = Random.Range(0.5f, 1f);
            StartCoroutine(LockTarget(other, rand));
            EnemyBase based = other.GetComponent<EnemyBase>();
            StartCoroutine(based.ShakePos(rand, 0.5f));
            StartCoroutine(based.ShakeSize(rand, 0.1f));

            Instantiate(ParticleManager.instance.yellowSchmoveHit, other.transform.position, Quaternion.identity);

            StartCoroutine(DealDamage(dmg, other, rand));
        }
    }

    private void DisableSelf()
    {
        Invoke("DestroySelf", 2f);
        self.enabled = false;
        mesh1.enabled = false;
        mesh2.enabled = false;
        col.enabled = false;
    }

    private void DestroySelf()
    {
        Destroy(emptyParent);
    }

    private IEnumerator DealDamage(IDamage dmg, Collider other, float rand)
    {
        float timer = 0f;
        
        while (timer < rand)
        {
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        dmg.takeDamage(PrimaryColor.OMNI, railgunDmg);
        Instantiate(ParticleManager.instance.yellowSchmoveExplosion, other.transform.position, Quaternion.identity);
    }

    IEnumerator LockTarget(Collider other, float rand)
    {
        Vector3 lockPos = other.transform.position;
        float timer = 0f;
        while (timer < rand)
        {
            other.transform.position = lockPos;
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
