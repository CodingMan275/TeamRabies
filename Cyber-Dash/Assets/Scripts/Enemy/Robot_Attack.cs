using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Attack : MonoBehaviour
{
    [SerializeField] private float AttackDmg;
    [SerializeField] private float AttackCoolDown;
    public float CoolDownTimer;
    private bool CanAttack = true;
    private bool Timer = false;

    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        CoolDownTimer = AttackCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        CoolDownTimer = Timer ? CoolDownTimer - Time.deltaTime : AttackCoolDown;
        if(CoolDownTimer <= 0 && CanAttack)
        {
            Timer = false;
            StartCoroutine(Attack());
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if(col.CompareTag("Player"))
        {
                Player = col;
                col.GetComponent<Player_Health>().TakeDamage(AttackDmg);
                CanAttack = Timer = true;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Player = null;
        Timer = CanAttack = false;
    }

    IEnumerator Attack()
    {
       Player.GetComponent<Player_Health>().TakeDamage(AttackDmg);
       yield return new WaitForSeconds(.15f);
        Timer = true;

    }
}
