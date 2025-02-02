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

    public bool isRight;
    public bool isRunning;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        isRunning = Input.GetButton("Run");
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        HealthBar.value = HP;
    }


    public void Move(float moveX, float moveY)
    {
        // This is an if/else statement
        // var = (condition) ? True : False (else) ;
        moveX = (moveX > .1f || moveX < -.1f) ? moveX : 0f;
        moveY = (moveY > .1f || moveY < -.1f) ? moveY : 0f;


        //move check
        TurnCheck(moveX);

        if(moveX !=0 || moveY !=0)
        {
           Vector2 targetVelocity = new Vector2(moveX, moveY) * (isRunning ? RunSpeed : WalkSpeed);
            moveVelocity = Vector2.Lerp(moveVelocity, targetVelocity, Time.fixedDeltaTime);
            rb.velocity = moveVelocity;
        }
        else
            rb.velocity = Vector2.zero;

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
}
