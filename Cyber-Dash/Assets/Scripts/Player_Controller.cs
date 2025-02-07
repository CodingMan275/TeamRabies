using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{

    public int HP = 10;

    public Slider HealthBar;

    public float WalkSpeed = 5f;
    public float RunSpeed = 8f;

    //Move these variables to dash function in another script
    public float DashSpeed = 10f;
    public float DashDuration = .5f;
    public float DashCooldown = 2f;

    private float ActiveAbilityDuration = 0f;
    private float AbilityCountdownCounter = 0f;

    //Bool for if ability locks out movement
    private bool AbilityMovementLock = false;

    public bool isRight;
    public bool isRunning;

    private Rigidbody2D rb;
    public Vector2 moveVelocity;
    private Vector2 targetVelocity;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isRight = true;
    }

    // Update is called once per frame
    void Update()
    {
       
        //Ability that locks out movement duration
        if (ActiveAbilityDuration > 0)
        {
            ActiveAbilityDuration -= Time.deltaTime;
            if (ActiveAbilityDuration <= 0)
            { 
             //Countdown = Script.Ability.Cooldown;
            AbilityCountdownCounter = DashCooldown;
            }
        }
        else 
        {
            //Either nothing changes or movement gets unlocked
            AbilityMovementLock = false;

            //Ability code
            //This will be chaked every frame so there is a very small window to miss the input
              if (Input.GetKeyDown(KeyCode.Space))
                 {
                Dash();
                 }
          
        }

        //Ability cooldown decrmenent
        if (AbilityCountdownCounter > 0)
        {
            AbilityCountdownCounter -= Time.deltaTime;
        }

       

            //If not locked
         if (!AbilityMovementLock)
        { 
         isRunning = Input.GetButton("Run");
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        HealthBar.value = HP;
        }
    }


    public void Move(float moveX, float moveY)
    {
        // This is an if/else statement
        // var = (condition) ? True : False (else) ;
        moveX = (moveX > .1f || moveX < -.1f) ? moveX : 0f;
        moveY = (moveY > .1f || moveY < -.1f) ? moveY : 0f;


        //move check
        TurnCheck(moveX);

        if (moveX == 0 && moveY == 0)
        {
            moveVelocity = Vector2.zero;
            rb.velocity = Vector2.zero;
        }
        else
        {
            targetVelocity = Vector2.zero;
            targetVelocity = new Vector2(moveX, moveY) * (isRunning ? RunSpeed : WalkSpeed);
            moveVelocity = Vector2.Lerp(moveVelocity, targetVelocity, Time.fixedDeltaTime);
            rb.velocity = moveVelocity;
        }


    }



    private void TurnCheck(float moveX)
    {
        if (isRight && moveX < 0)
            Turn(false);
        if (!isRight && moveX > 0)
            Turn(true);

    }

    public void Turn(bool turn)
    {
        isRight = turn;
        transform.Rotate(0f, 180f * (turn ? 1f : -1f), 0f);
    }

    private void Dash()
    {
        AbilityMovementLock = true;

        //Dash Code
        if (Input.GetKey(KeyCode.Space))
        {
            if (AbilityCountdownCounter <= 0 && ActiveAbilityDuration <= 0)
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * DashSpeed;
                //To countodwn how long the dash is going
                ActiveAbilityDuration = DashDuration;
            }
        }
    }
}
