using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Dash : MonoBehaviour
{
    //General outline for abilities
    
    //Refrence to the player to change locks or other stats
    public Player_Controller player;

    //getting the rigid body for a dash
    private Rigidbody2D rb;

    //Move these variables to dash function in another script
    public float DashSpeed = 23f;
    public float DashDuration = .5f;
    public float DashCooldown = 2f;

    //While ability is active
    private float ActiveAbilityDuration = 0f;
    private float AbilityCountdownCounter = 0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            player.AbilityMovementLock = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Dash();
            }
        }

        //Ability cooldown decrmenent
        if (AbilityCountdownCounter >= 0)
        {
            AbilityCountdownCounter -= Time.deltaTime;
        }
    }

    private void Dash()
    {
        

        //Dash Code

            if (AbilityCountdownCounter <= 0 && ActiveAbilityDuration <= 0)
            {
                player.AbilityMovementLock = true;
            if (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) == Vector2.zero)
            {
                rb.velocity = Vector2.right * DashSpeed * (player.isRight?1:-1);
            }
            else
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * DashSpeed;
            }
                //To countodwn how long the dash is going
                ActiveAbilityDuration = DashDuration;
            }
        
    }
}
