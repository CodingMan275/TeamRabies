using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ultamite : MonoBehaviour
{
    public Slider UltSlider;
    public bool CanUlt = false;
    public SaveData player;
    public GameObject EBlast;

    InputController IC;

    public void OnEnable()
    {
        IC = GetComponent<InputController>();
        IC.UltActivate += UltCheck;
    }


    // Start is called before the first frame update
    void Start()
    {
        player.UD.ChangeUltVars(player);
        UltSlider.value = player.UD.UltPerc;
    }

    private void Update()
    {
        if(!player.UD.inShop)
        {
            if(player.UD.DoubleDamage)
                player.UD.UltDur = player.UD.UltDur <= 0 ? UltDone() : player.UD.UltDur - Time.deltaTime;

                player.UD.UltCoolDown = player.UD.UltCoolDown <= 0 ? 0 : player.UD.UltCoolDown - Time.deltaTime;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CanUlt = UltSlider.value == UltSlider.maxValue && player.UD.UltCoolDown == 0 ? true : false;
    }

    void UltCheck()
    {
        if (CanUlt && !player.UD.inShop) 
        {
            player.UD.UltCoolDown = player.UD.UltUseage;
            player.UD.UltDur = player.UD.UltTimer;
            player.UD.DoubleDamage = true;
            player.UD.UltPerc = 0;
            UltSlider.value = player.UD.UltPerc;
            GetComponent<Player_Health>().ForceHeal((int)player.UD.HealBuff);
            StartCoroutine(Electricblast());
        }
    }

    int UltDone()
    {
        player.UD.DoubleDamage = false;
        GetComponent<Player_Health>().LoseShield((int)player.UD.HealBuff);
        return 0;
    }
    IEnumerator Electricblast()
    {
        EBlast.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        EBlast.SetActive(false);
    }
}
