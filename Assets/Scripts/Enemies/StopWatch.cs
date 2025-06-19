using UnityEngine;

public class StopWatch : EnemyBase, IDamage
{
    //[SerializeField] GameObject SpitSac;
    
    int counter;

    private void Awake()
    {
        OnAECAwake();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        counter = 0;
        ColorSelection(setColor);
        base.UpdateBoidAwareness(); //this will need to be commented out once/if the stopwatch gets a rigidbody
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
            //SacSpit();
        }
    }

    //void SacSpit()
    //{
    //    SacGroundDetection mySac = Instantiate(SpitSac,transform.position,Quaternion.identity).GetComponent<SacGroundDetection>();
    //    mySac.setColor = this.setColor;
    //}

    
}
