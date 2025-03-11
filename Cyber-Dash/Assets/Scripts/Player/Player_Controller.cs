using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    public float WalkSpeed = 5f;
    public float RunSpeed = 8f;

    InputController IC;

    public bool MovementLock = false;

    public bool AbilityLock = false;

    public bool isRight = true;
    
    private bool isMoving;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    Animator anim;

    public AudioSource Walk;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        IC = GetComponent<InputController>();
        GetComponent<Ability_Dash>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("RunSpeed" , IC.isRunning?(RunSpeed/WalkSpeed):1f);
        Walk.volume = isMoving ? 1: 0;
    }

    private void FixedUpdate()
    {
        anim.SetBool("Dash", MovementLock);
        //If not locked
         if (!MovementLock)
         { 
          Move(IC.LInput.x , IC.LInput.y);
         }
    }

    #region movement
    public void Move(float moveX, float moveY)
    {
        moveX = (moveX > .1f || moveX < -.1f) ? moveX : 0f;
        moveY = (moveY > .1f || moveY < -.1f) ? moveY : 0f;  
        isMoving = (moveX !=0 || moveY !=0)? true : false;
        anim.SetBool("Run", isMoving);

        TurnCheck(moveX);

        Vector2 movement = new Vector2(moveX, moveY).normalized;
        rb.velocity = movement * (IC.isRunning ? RunSpeed : WalkSpeed);

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
        sr.flipX = !isRight;
    }

    #endregion

}
