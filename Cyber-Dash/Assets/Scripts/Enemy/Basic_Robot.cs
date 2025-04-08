using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Basic_Robot : MonoBehaviour
{
    // private variable that can be seen and edited in Unity
    private Enemy_Counter EC;
    private SpriteRenderer render;

    

    public GameObject DropScrap;
    public GameObject DropEnergy;
    public float Health;
    public float MaxHealth;
    public float speed = 8f;

    private bool WasHurt = false;
    public bool Die = false;

    public SaveData Player;

   // public bool playerInRange;

    Transform target;
    Rigidbody2D rb;
    Vector3 dir;

    Transform miniboss;
    public bool KnockBackStun = false;

    [SerializeField] private float MoveDelay;
    private float DelayTimer;
    private bool StartDelay = false;

    // Called First frame when object spawns
    void Awake()
    {
        Health = MaxHealth;
        DelayTimer = MoveDelay;
        render = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        EC = GameObject.Find("Enemy Count").GetComponent<Enemy_Counter>(); 
        target = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        dir = (target.position - transform.position).normalized;
            
        DelayTimer = StartDelay ? DelayTimer - Time.deltaTime : MoveDelay;
        if(DelayTimer <=0)
        {
            GetComponent<CapsuleCollider2D>().isTrigger = false;
            StartDelay = false;
           
        }
    }

    private void FixedUpdate()
    {
        if(!StartDelay)
        rb.velocity = new Vector2(dir.x, dir.y) * speed;
        if(Health <=0 && !Die)
        {
            Die = true;
            Death();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if (collision.gameObject.GetComponent<Ranged_Robot>() == true)
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<CapsuleCollider2D>());
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartDelay = true;
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
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
        else if (col.CompareTag("Explode"))
        {
            print("explo hit");
            StartCoroutine(TakeDamage(1));
        }
        else if (col.CompareTag("Wall"))
        { 
         GetComponent<CapsuleCollider2D>().isTrigger = false;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
        else if (col.CompareTag("Knockback"))
        {
            //Knock back
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            StartDelay = true;
            GetComponent<CapsuleCollider2D>().isTrigger = true;
            //  dir = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y);
            rb.AddForce(-dir * col.GetComponent<Knockback_Logic>().KnockbackDist * 1.4f, ForceMode2D.Impulse);
            rb.velocity = -dir * speed * 2f;
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
        if(Health > 0)
        WasHurt = false;
    }

    public float Death()
    {
        Player.killCount++;
        EC.UpdateCounter();
        int temp = Random.Range(0,2);
        if(temp == 0)
        Instantiate(DropScrap, transform.position, Quaternion.identity);
        else
        Instantiate(DropEnergy, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        return 0;
    }
}
