using UnityEngine;

public class Monk : MonoBehaviour
{
    [SerializeField] float Casttimer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Casttimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {


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
}
