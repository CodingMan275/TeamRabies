using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Ability_Dash : MonoBehaviour
{
    //General outline for abilities
    
    //Refrence to the player to change locks or other stats
    public Player_Controller player;

    //getting the rigid body for a dash
    private Rigidbody2D rb;

    InputController IC;

    //Move these variables to dash function in another script
    public float DashSpeed = 23f;
    public float DashDuration = .5f;
    public double DashCooldown = 2;

    //While ability is active
    private float ActiveAbilityDuration = 0f;
    private double AbilityCountdownCounter = 0;

    public SaveData playerUpgrades;

    public Slider CooldownBar;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        IC = GetComponent<InputController>();
        IC.OnDashPressed += Dash;
        CooldownBar.maxValue = (float)(DashCooldown * playerUpgrades.DodgeCooldownMod);
    }

    // Update is called once per frame
    void Update()
    {
        //Ability that locks out movement duration
        if (ActiveAbilityDuration > 0)
        {
            CooldownBar.value = CooldownBar.maxValue;
            ActiveAbilityDuration -= Time.deltaTime;
            if (ActiveAbilityDuration <= 0)
            {
                //Countdown = Script.Ability.Cooldown;
                AbilityCountdownCounter = DashCooldown * playerUpgrades.DodgeCooldownMod;
                //CooldownBar.value = (float)AbilityCountdownCounter;
            }
        }
        else
        {
            //Either nothing changes or movement gets unlocked
            player.MovementLock = false;

          //  if (Input.GetKeyDown(KeyCode.Space) && !player.AbilityLock)
        //    {
          //      Dash();  
            //}
        }

        //Ability cooldown decrmenent
        if (AbilityCountdownCounter >= 0)
        {
            CooldownBar.value = (float)AbilityCountdownCounter;
            CooldownBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = ""+ ((int)AbilityCountdownCounter+1);
            if(AbilityCountdownCounter <= 0.02)
            {
                CooldownBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
            }
            AbilityCountdownCounter -= Time.deltaTime;
        }

        

    }

    private void Dash()
    {

        if (!player.AbilityLock)
        {
            //Dash Code

            if (AbilityCountdownCounter <= 0 && ActiveAbilityDuration <= 0)
            {
                player.MovementLock = true;
                if (IC.LInput == Vector2.zero)
                {
                    rb.velocity = Vector2.right * DashSpeed * (player.isRight ? 1 : -1);
                }
                else
                {
                    rb.velocity = IC.LInput * DashSpeed;
                }
                //To countodwn how long the dash is going
                ActiveAbilityDuration = DashDuration;
            }
        }
        
    }
}
