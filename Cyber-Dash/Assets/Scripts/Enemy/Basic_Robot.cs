using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Basic_Robot : MonoBehaviour
{
    // private variable that can be seen and edited in Unity
    private Enemy_Counter EC;
    private SpriteRenderer render;

    public bool KnockBackStun = false;

    public GameObject DropScrap;
    public GameObject DropEnergy;
    public float Health;
    public float MaxHealth;

    private bool WasHurt = false;
    public bool Die = false;

    public SaveData Player;

   // public bool playerInRange;

    Transform target;
    NavMeshAgent agent;

    Transform miniboss;


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
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.Find("Player").GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {

       if(agent.speed != 0)
        agent.SetDestination(target.position);
            
        DelayTimer = StartDelay ? DelayTimer - Time.deltaTime : MoveDelay;
        if(DelayTimer <=0)
        {
            StartDelay = false;
            GetComponent<CapsuleCollider2D>().enabled = true;
            float dist = Mathf.Abs(Vector3.Distance(target.position, transform.position));
            if(dist > 5.5)
            {
                agent.speed = 8f;
            }
           
        }
    }

    private void FixedUpdate()
    {
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
            agent.speed = 0;
            // playerInRange = true;
            //  agent.SetDestination(-target.position);

        }
        else if(collision.gameObject.name == "Doomba")
        {
            // print("Collided");
            if (collision.gameObject.GetComponent<Miniboss>().Charging)
            {
                GetComponent<CapsuleCollider2D>().enabled = false;
                StartDelay = true;
                //float rand = Random.Range(0f, 1f);
                Vector3 Direction = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y);
                if (Random.value < .5f)
                    agent.velocity = -(Quaternion.Euler(0, 90, 0) * Direction * 10);
                else
                    agent.velocity = -(Quaternion.Euler(0, -90, 0) * Direction * 10);
            }
        }
        
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (agent.speed == 0)
        {
            StartDelay = true;
           // playerInRange = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.CompareTag("Bullet"))
        {
            print("Bullet hit");
            if(col.GetComponent<Bullet>() != null)
            StartCoroutine(TakeDamage(col.GetComponent<Bullet>().Damage));
            else if(col.GetComponent<Bolt>() != null)
            StartCoroutine(TakeDamage(col.GetComponent<Bolt>().Damage));
            else
            StartCoroutine(TakeDamage(col.GetComponent<Beam>().Damage));
        }
        if (col.CompareTag("Explode"))
        {
            print("explo hit");
            StartCoroutine(TakeDamage(1));
        }

        if (col.CompareTag("Knockback"))
        {
            agent.avoidancePriority = 40;
          //Knock back
            Vector3 Direction = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y);
            agent.velocity = -(Direction * col.GetComponent<Knockback_Logic>().KnockbackDist);
        }
    }

    public IEnumerator StunDelay()
    {
        yield return new WaitForSeconds(.5f);
       // agent.avoidancePriority = 50;
        agent.speed = 8;
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
