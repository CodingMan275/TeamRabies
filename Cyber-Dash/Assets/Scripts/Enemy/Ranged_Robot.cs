using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ranged_Robot : MonoBehaviour
{
    // private variable that can be seen and edited in Unity
    [SerializeField] private Enemy_Counter EC;
    private SpriteRenderer render;

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

    bool running_away = false;

    public bool CanShoot = false;

   [SerializeField ]private Transform gunTransform;
    [SerializeField] private float MoveDelay;
    private float DelayTimer;
    private bool StartDelay = false;

    bool flop = false;



    [SerializeField] Transform target;

    NavMeshAgent agent;

    // Called First frame when object spawns
    void Awake()
    {
        Health = MaxHealth;
        render = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        EC = GameObject.Find("Enemy Count").GetComponent<Enemy_Counter>(); 
       // agent = GetComponentInParent<NavMeshAgent>();
      //  agent.updateRotation = false;
      //  agent.updateUpAxis = false;
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
          //  rb.angularVelocity = 3;
        }
    }

    public IEnumerator TakeDamage(float dmg)
    {
        WasHurt = true;
        Health -= Health - dmg <= 0 ? Death() : dmg;    //Replace 1 with bull damage
        render.color = new Color(255f, 0f, 0f, 255f);
        yield return new WaitForSeconds(.15f);
        render.color = new Color(255f, 255f, 255f, 255f);
        yield return new WaitForSeconds(.15f);
        if(Health > 0)
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
