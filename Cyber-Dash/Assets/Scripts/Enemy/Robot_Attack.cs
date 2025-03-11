using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Attack : MonoBehaviour
{
    [SerializeField] private float AttackDmg;
    [SerializeField] private float AttackCoolDown;
    [SerializeField] private float AttackRange;
    public float CoolDownTimer;
    private bool CanAttack = true;
    private bool Timer = false;

    public bool inRange;
    public float dist;

    private GameObject Player;
    private Transform pt;
    // Start is called before the first frame update
    void Start()
    {
        pt = GameObject.Find("Player").GetComponent<Transform>();
        CoolDownTimer = AttackCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Mathf.Abs(Vector3.Distance(pt.position, transform.position));
        if(dist <= AttackRange)
         {
            inRange = true;
            CoolDownTimer = CoolDownTimer - Time.deltaTime;
         }
        else 
        { inRange = false;
            CoolDownTimer = AttackCoolDown;
        }
        // CoolDownTimer = Timer ? CoolDownTimer - Time.deltaTime : AttackCoolDown;
       
        if(CoolDownTimer <= 0 && inRange)
        {
            Timer = false;
            StartCoroutine(Attack());
            CoolDownTimer = AttackCoolDown;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if(col.CompareTag("Player"))
        {
              //  Player = col;
              //  col.GetComponent<Player_Health>().TakeDamage(AttackDmg);
                CanAttack = Timer = true;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
       // Player = null;
        Timer = CanAttack = false;
    }

    IEnumerator Attack()
    {
       pt.GetComponent<Player_Health>().TakeDamage(AttackDmg);
       yield return new WaitForSeconds(.15f);
        Timer = true;

    }
}
