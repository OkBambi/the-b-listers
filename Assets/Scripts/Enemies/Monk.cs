using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Monk : MonoBehaviour, IDamage
{

    [SerializeField] float Casttimer;

    [SerializeField] NavMeshAgent agent;

    [SerializeField] Transform Casting;
    [SerializeField] GameObject wave;
    [SerializeField] float gongBonkRate;

    //noise 
    Color colorOriginal;

    float GongBonkTimer;

    bool PlayerInRange;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Casttimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerInRange)
        {

        }

    }
    void MonkCastTimer()
    {
        Casttimer += Time.deltaTime;

        if (Casttimer >= 5.5)
        {
            Casttimer = 0f;
            Cast();

        }


        void Cast()
        {
            //projectile
            Instantiate(gameObject, transform.position, Quaternion.identity);
            //

            //if player is in the sphere cast
            // lock the player color choice for 3 seconds


            //wait a moment

            //swap through the colors


        }

    }

    public void takeDamage(PrimaryColor hitColor, int amount)
    {
        throw new System.NotImplementedException();
    }
}
