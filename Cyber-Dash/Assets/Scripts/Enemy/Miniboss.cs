using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Miniboss : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 chargePos;
    public SaveData Player;
    public GameObject DropScrap;
    public GameObject DropEnergy;
    public GameObject Trail;

    public int AttackDmg;

    public float Health;
    public float MaxHealth;

    public float Speed =  1;

    public bool LockedOn = false;
    public bool onWall = false;
    public bool Charging;
    Transform target;

    private bool WasHurt = false;
    public bool Die = false;
    private Enemy_Counter EC;
    private SpriteRenderer render;

    float delay = .4f;
    float delayTimer;
    float lastAttack;

    float trailDelay;
    float lastTrail;
    public Slider HealthBar;

    public bool isLeft = true;


    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        EC = GameObject.Find("Enemy Count").GetComponent<Enemy_Counter>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        //  chargePos = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, target.position.z);
        // rb.velocity = Vector2.MoveTowards(transform.position,target.position, Speed);
        chargePos = (target.position - transform.position).normalized;
        rb.velocity = chargePos * Speed;
        Charging = true;
       // transform.right = target.position - transform.position; ** This is for rotating **
        HealthBar.maxValue = MaxHealth;
        HealthBar.value = MaxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        lastAttack = Time.time - delayTimer;
        if(Charging)
        {
            lastTrail = Time.time - trailDelay;
            if (lastTrail > .11)
            {
                Instantiate(Trail, transform.position, transform.rotation);
                trailDelay = Time.time;
            }
        }
      
        if (rb.velocity != (chargePos * Speed) && Charging)
        { 
         rb.velocity = chargePos * Speed;
        }
    }

    private void FixedUpdate()
    {
        anim.SetBool("Walking", (rb.velocity.magnitude > .1f || rb.velocity.magnitude < -.1f) && !Die && !anim.GetBool("Stabbing"));
        anim.SetBool("Death", Die);
        if(anim.GetBool("Walking"))
        TurnCheck(rb.velocity.x);

        if (Health <= 0 && !Die)
        {
            Die = true;
        }
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Charging = false;
            rb.velocity = chargePos * 0;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;


            if (collision.contacts[0].normal.x != 0)
            {
                anim.SetBool("Turning", true);
                anim.Play("Doomba Turn");
                yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
                anim.SetBool("Turning", false);
                Turn(!isLeft);
            }
            yield return new WaitForSeconds(1f);



            //chargePos = new Vector3(target.position.x * .5f - transform.position.x, target.position.y * .5f - transform.position.y, target.position.z);

            // rb.velocity = Vector2.MoveTowards(transform.position, target.position, Speed);
            chargePos = (target.position - transform.position).normalized;
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = chargePos * Speed;
            Charging = true;
        }
        else if (collision.gameObject.tag == "Player")
        {
            if ((lastAttack > delay) && Charging)
            {
                anim.SetBool("Stabbing", true);
                target.GetComponent<Player_Health>().TakeDamage(AttackDmg);
                anim.Play("Doomba Slice");
                yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
                delayTimer = Time.time;
                anim.SetBool("Stabbing", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.CompareTag("Bullet"))
        {
            print("Bullet hit");
            if (col.GetComponent<Bullet>() != null)
                StartCoroutine(TakeDamage(col.GetComponent<Bullet>().Damage));
            else if (col.GetComponent<Bolt>() != null)
                StartCoroutine(TakeDamage(col.GetComponent<Bolt>().Damage));
            else
                StartCoroutine(TakeDamage(col.GetComponent<Beam>().Damage));
        }
        else if (col.CompareTag("UltBlast"))
        {
            StartCoroutine(TakeDamage(20));
        }
    }

    public IEnumerator TakeDamage(float dmg)
    {
        Health -= dmg;    //Replace 1 with bull damage
        HealthBar.value = Health;
        if (!WasHurt)
        {
            WasHurt = true;
            render.color = new Color(255f, 0f, 0f, 255f);
            yield return new WaitForSeconds(.15f);
            render.color = new Color(255f, 255f, 255f, 255f);
            yield return new WaitForSeconds(.15f);
        }
        if (Health > 0)
            WasHurt = false;
    }

    public float Death()
    {
        Player.killCount++;
        EC.UpdateCounter();
        int temp = Random.Range(0, 2);
        if (temp == 0)
            Instantiate(DropScrap, transform.position, Quaternion.identity);
        else
            Instantiate(DropEnergy, transform.position, Quaternion.identity);
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Finish");
        foreach (GameObject go in gos)
            Destroy(go);
        Destroy(this.gameObject);
        return 0;
    }

    private void OnDestroy()
    {
        EC.UpdateCounter();
    }

    private void TurnCheck(float moveX)
    {
        if (isLeft && moveX < 0)
            Turn(false);
        if (!isLeft && moveX > 0)
            Turn(true);
    }

    public void Turn(bool turn)
    {
        isLeft = turn;
        render.flipX = isLeft;
    }
}
