using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Miniboss : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 chargePos;
    public SaveData Player;
    public GameObject DropScrap;
    public GameObject DropEnergy;

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
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        EC = GameObject.Find("Enemy Count").GetComponent<Enemy_Counter>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        //  chargePos = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, target.position.z);
        // rb.velocity = Vector2.MoveTowards(transform.position,target.position, Speed);
        chargePos = (target.position - transform.position).normalized;
        rb.velocity = chargePos * Speed;
        Charging = true;
        transform.right = target.position - transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        lastAttack = Time.time - delayTimer;
        if (!Charging)
        { 
            transform.right = target.position - transform.position;
        }
      
        if (rb.velocity != (chargePos * Speed) && Charging)
        { 
         rb.velocity = chargePos * Speed;
        }
    }

    private void FixedUpdate()
    {
        if (Health <= 0 && !Die)
        {
            Die = true;
            Death();
        }
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Charging = false;
            rb.velocity = chargePos * 0;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            //chargePos = new Vector3(target.position.x * .5f - transform.position.x, target.position.y * .5f - transform.position.y, target.position.z);

            // rb.velocity = Vector2.MoveTowards(transform.position, target.position, Speed);

            yield return new WaitForSeconds(2f);
            chargePos = (target.position - transform.position).normalized;
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = chargePos * Speed;
            transform.right = target.position - transform.position;
            Charging = true;
        }
        else if (collision.gameObject.tag == "Player")
        {
            if ((lastAttack > delay) && Charging)
            {
                target.GetComponent<Player_Health>().TakeDamage(AttackDmg);
                delayTimer = Time.time;
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
    }

    public IEnumerator TakeDamage(float dmg)
    {
        Health -= dmg;    //Replace 1 with bull damage
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
        Destroy(this.gameObject);
        return 0;
    }
}
