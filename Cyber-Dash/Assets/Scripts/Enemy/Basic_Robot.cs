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
    private float Health;
    public float MaxHealth;
    private bool WasHurt = false;

    public SaveData Player;

    Transform target;
    NavMeshAgent agent;

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
        if(agent.isActiveAndEnabled)
            agent.SetDestination(target.position);
        DelayTimer = StartDelay ? DelayTimer - Time.deltaTime : MoveDelay;
        if(DelayTimer <=0)
        {
            StartDelay = false;
            agent.speed = 8f;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            agent.speed = 0;
        
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (agent.speed == 0)
            StartDelay = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.CompareTag("Bullet"))
        {
            print("Bullet hit");
            StartCoroutine(TakeDamage(1));
        }
        if (col.CompareTag("Explode"))
        {
            print("explo hit");
            StartCoroutine(TakeDamage(1));
        }
    }

    public IEnumerator TakeDamage(float dmg)
    {
        Health -= Health - dmg <= 0 ? Death() : dmg;    //Replace 1 with bull damage
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
        int temp = Random.Range(0,1);
        if(temp == 0)
        Instantiate(DropScrap, transform.position, Quaternion.identity);
        else
        Instantiate(DropEnergy, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        return 0;
    }
}
