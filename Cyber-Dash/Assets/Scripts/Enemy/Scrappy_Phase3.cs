using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrappy_Phase3 : MonoBehaviour
{
    public float speed = 7f;
    Transform target;
    Rigidbody2D rb;
    Vector3 dir;

    public int meleeDmg = 4;
    public float meleeDelay;
    public int rangedDmg = 5;
    public int missileDmg = 4;
    public int explosionDmg = 8;

    public GameObject MeleeAttack;
    public GameObject RangedAttack;
    public GameObject MissileAttack;
    public GameObject ExplosionAttack;

    float AttackDelay = 3.0f;
    float AttackTimer = 3.0f;

    int rotateAttacks = 0;

    public GameObject player;

    bool locked = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = (target.position - transform.position).normalized;
        if(!locked)
        AttackTimer = AttackTimer - Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = null;
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dir.x, dir.y) * speed;
        if (AttackTimer <= 0)
        {
            StartCoroutine(Attack());
            AttackTimer = AttackDelay;
        }
    }

    IEnumerator Attack()
    {
        rotateAttacks++;
        if (rotateAttacks == 4)
        {
            //Misslles
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            GameObject attack = Instantiate(MissileAttack, target.transform.position, transform.rotation);
            attack.GetComponent<Scrappy_MIssile_Logic>().dmg = missileDmg;

            yield return new WaitForSeconds(1f);
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

            rotateAttacks = 0;
        }
        else if (player != null)
        {
            //melee attack
            //Play attack animation
            for (int i = 0; i < 3; i++)
            {
                //Attackanimation
                //Becuase the player can move out range and i want to limit errors
                if (player != null)
                {
                    GameObject attack = Instantiate(MeleeAttack, player.transform.position, transform.rotation);
                    attack.GetComponent<Scrappy_Melee_Logic>().dmg = meleeDmg;
                }
                yield return new WaitForSeconds(meleeDelay);
            }
        }
        else
        {
            //Ranged attack
            Vector3 dir = GetComponentInChildren<Scrappy_Aim>().Direction;
            GameObject attack = Instantiate(RangedAttack, transform.position, transform.rotation);
            attack.transform.right = dir;
            attack.GetComponent<Scrappy_Ranged_Attack>().dmg = rangedDmg;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            yield return new WaitForSeconds(.4f);
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
        AttackTimer = AttackDelay;
    }

    //FOr michael
    public void Spawn()
    {

    }

    public IEnumerator Explode()
    {
        locked = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GameObject attack = Instantiate(ExplosionAttack, transform.position, transform.rotation);
        attack.GetComponent<Scrappy_Explosion>().dmg = explosionDmg;
        yield return new WaitForSeconds(4f);
        locked = false;
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

    }
}
