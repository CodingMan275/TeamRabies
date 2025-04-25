using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrappy_Phase1 : MonoBehaviour
{
    public float speed = 8f;
    Animator anim;
    Transform target;
    Rigidbody2D rb;
    Vector3 dir;

    public int meleeDmg = 4;
    public float meleeDelay;
    public int rangedDmg = 5;

    bool playerInRange = false;

    public GameObject MeleeAttack;
    public GameObject RangedAttack;

    float AttackDelay = 5.0f;
    float AttackTimer = 5.0f;

    bool isRight = true;
    SpriteRenderer sr;

    public GameObject Spawner;

    public GameObject player = null;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = (target.position - transform.position).normalized;
        AttackTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = null;
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            playerInRange = false;
        }
    }

    private void FixedUpdate()
    {
        anim.SetBool("Walk", (rb.velocity.magnitude > .1f || rb.velocity.magnitude < -.1f));
        TurnCheck(rb.velocity.x);
        if (!playerInRange)
            rb.velocity = new Vector2(dir.x, dir.y) * speed;
        if (AttackTimer <= 0)
        {
            StartCoroutine(Attack());
            AttackTimer = AttackDelay;
        }
    }

     IEnumerator Attack()
    {
        if (player != null)
        {
            //melee attack
            //Play attack animation
            for (int i = 0; i < 3; i++)
            {
                //Attackanimation
                //Becuase the player can move out range and i want to limit errors
                anim.Play("Scrappy Melee");
                if (player != null)
                {
                    GameObject attack = Instantiate(MeleeAttack, player.transform.position, transform.rotation);
                    attack.GetComponent<Scrappy_Melee_Logic>().dmg = meleeDmg;
                }
                yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
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
            anim.Play("Scrappy Range");
            yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    //FOr michael
    public void Spawn()
    {
        Spawner.SetActive(true);
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
}
