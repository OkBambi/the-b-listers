using UnityEngine;

public class StopWatch : EnemyBase
{
    [SerializeField] GameObject SpitSac;
   
    int counter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            CountDownTimer();
        }
    }

    void CountDownTimer()
    {
        counter++;
        if(counter == 3)
        {
            counter = 0;

            SacSpit();
        }
    }

    void SacSpit()
    {
        SacGroundDetection mySac = Instantiate(SpitSac,transform.position,Quaternion.identity).GetComponent<SacGroundDetection>();
        mySac.setColor = this.setColor;
    }
}
