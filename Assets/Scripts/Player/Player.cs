using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IColorLock
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerCamera playerCamera;
    [SerializeField] PlayerShooting weapon;
    [SerializeField] ColorSwapping colorSwapper;
    [SerializeField] Schmoves schmover;
    [SerializeField] PlayerArm arm;

    [Space]
    [SerializeField] CameraSpring cameraSpring;

    [Space]
    [SerializeField] PrimaryColor currentColor;

    [Space]
    [Header("Conditionals")]
    public bool canMove;
    public bool canCam;
    public bool canAction;
    public bool canColor;
    public bool isDead = false;

    void Start()
    {

        colorSwapper = GameManager.instance.colorSwapper;
        schmover = GameManager.instance.schmover;

        playerMovement.Initialize();
        playerCamera.Initialize();
        weapon.Initialize();
    }

    void Update()
    {
        if (!GameManager.instance.isPaused)
        {
            if (canMove)
                playerMovement.UpdateInput();

            if (canCam)
                playerCamera.UpdateCamera(playerMovement.IsGrounded());

            if (canColor)
                colorSwapper.UpdateColor(ref currentColor);

            if (canAction)
            {
                weapon.UpdateWeapon(currentColor, arm);

                schmover.UpdateInput(currentColor);
            }
        }

        arm.UpdateArm(currentColor);
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isPaused)
        {
            float deltaTime = Time.deltaTime;

            cameraSpring.UpdateSpring(deltaTime, Vector3.up);
        }
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isPaused)
            if (canMove)
                playerMovement.UpdateBody();
    }

    public PrimaryColor GetPlayerColor()
    {
        return currentColor;
    }


    public void Die()
    {
        if (isDead)
        {
            return;
        }
        //disable player stuff
        canAction = false;
        canMove = false;
        canCam = false;
        canColor = false;
        isDead = true;



        AudioManager.instance.Play("Game_Over");

        //save score
        //check for highscore
        int highScore = PlayerPrefs.GetInt("Highscore");

        if (ComboManager.instance.GetScore() > highScore)
        {
            GameManager.instance.SaveHighscore(ComboManager.instance.GetScore());
        }

        //lose menu
        GameManager.instance.OnEndCondition();
    }

    //Monk Lock Color interface implentation **NEW**
    public void LockColorSelection(float duration)
    {
        StartCoroutine(LockColor(duration));
    }
    IEnumerator LockColor(float duration)
    {
        canColor = false;
        yield return new WaitForSeconds(duration);
        canColor = true;
    }
}

//enum of primary colors
public enum PrimaryColor
{
    RED,
    YELLOW,
    BLUE,
    OMNI
}