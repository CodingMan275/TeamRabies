using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ranged_Robot : MonoBehaviour
{
    // private variable that can be seen and edited in Unity
    private Enemy_Counter EC;
    private SpriteRenderer sr;

    public GameObject DropScrap;
    public GameObject DropEnergy;
    private float Health;
    public float MaxHealth;
    private bool WasHurt = false;

    public SaveData Player;

    public float speed;
    public int ShootRange = 16;

    Rigidbody2D rb;
    Vector3 dir;

    public int RunRange = 10;

    public bool CanShoot = false;

   [SerializeField ]private Transform gunTransform;
    [SerializeField] private float MoveDelay;
    private float DelayTimer;
    private bool StartDelay = false;

    [SerializeField] Transform target;

    Animator anim;
    bool isRight = true;
    bool Die = false;


    // Called First frame when object spawns
    void Awake()
    {
        Health = MaxHealth;
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        EC = GameObject.Find("Enemy Count").GetComponent<Enemy_Counter>(); 
        anim = GetComponent<Animator>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        //get direction
        dir = (target.position - transform.position).normalized;
        DelayTimer = StartDelay ? DelayTimer - Time.deltaTime : MoveDelay;
        if (DelayTimer <= 0)
        {
            GetComponent<CapsuleCollider2D>().isTrigger = false;
            StartDelay = false;
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
        }

    private void FixedUpdate()
    {
        TurnCheck(rb.velocity.x);
        anim.SetBool("Move", (rb.velocity.magnitude > .1f || rb.velocity.magnitude < -.1f) && !Die);
        anim.SetBool("Death", Die);

        if (Health <= 0 && !Die)
        {
            Die = true;
        }



        Vector3 Direction = new Vector3(target.position.x - gunTransform.position.x, target.position.y - gunTransform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(gunTransform.position, Direction);
        Debug.DrawRay(gunTransform.position, Direction, Color.green);
        //transform.up = Direction;
        if ((Mathf.Abs(Vector3.Distance(target.position, transform.position)) <= ShootRange) && hit.collider.gameObject.tag != "Wall")
        {        
            if (!StartDelay)
            {
                 rb.velocity = Vector2.zero;
                rb.angularVelocity = 0;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            CanShoot = true;
        }
        else 
        {
            CanShoot = false;
            rb.velocity = new Vector2(dir.x, dir.y) * speed;
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
            GetComponent<CapsuleCollider2D>().isTrigger = true;
            StartDelay = true;
            rb.AddForce(-dir * col.GetComponent<Knockback_Logic>().KnockbackDist * 10f, ForceMode2D.Impulse);
            rb.velocity = -dir * speed * 8f;
        }
    }

    public IEnumerator TakeDamage(float dmg)
    {
        Health -= dmg;    //Replace 1 with bull damage
        if (!WasHurt)
        {
            WasHurt = true;
            sr.color = new Color(255f, 0f, 0f, 255f);
            yield return new WaitForSeconds(.15f);
            sr.color = new Color(255f, 255f, 255f, 255f);
            yield return new WaitForSeconds(.15f);
        }
        if (Health > 0)
            WasHurt = false;
    }

    public float Death()
    {
        int temp = Random.Range(0, 2);
        if (temp == 0)
            Instantiate(DropScrap, transform.position, Quaternion.identity);
        else
            Instantiate(DropEnergy, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        return 0;
    }

    private void OnDestroy()
    {
        Player.killCount++;
        EC.UpdateCounter();
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
