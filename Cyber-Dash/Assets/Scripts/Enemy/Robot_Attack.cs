using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Attack : MonoBehaviour
{
    [SerializeField] private float AttackDmg;
    [SerializeField] private float AttackCoolDown;
    private float CoolDownTimer;
    public bool CanAttack = true;
    private bool Timer = false;

    // Start is called before the first frame update
    void Start()
    {
        CoolDownTimer = AttackCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        CoolDownTimer -= Timer ? Time.deltaTime : 0;
        Timer = CoolDownTimer <= 0 ? false : Timer;
        CanAttack = Timer ? CanAttack : true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if(col.CompareTag("Player"))
        {
            if (CanAttack) 
            {
                CanAttack = false;
                StartCoroutine(col.GetComponent<Player_Health>().TakeDamage(AttackDmg));
                Timer = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
           GameObject col = collision.gameObject;
           if (col.CompareTag("Player"))
           {
            if (CanAttack)
            {
                CanAttack = false;
                Timer = true;
                StartCoroutine(col.GetComponent<Player_Health>().TakeDamage(AttackDmg));
                CoolDownTimer = AttackCoolDown;
            }
           }
       
    }
}
