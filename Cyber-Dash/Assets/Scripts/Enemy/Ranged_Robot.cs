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
    private float Health;
    public float MaxHealth;
    private bool WasHurt = false;

    public SaveData Player;

    public int ShootRange = 16;

    public int RunRange = 10;

    public bool running_away = false;

    public bool CanShoot = false;

    

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
        agent = GetComponentInParent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.Find("Player").GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!running_away)
        {
            agent.SetDestination(target.position);
        }
           Vector3 Direction = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y);
           Ray ray = new Ray(transform.position, (target.position - transform.position).normalized * 10);
           RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction);
                //transform.up = Direction;
            if ((Mathf.Abs(Vector3.Distance(target.position, transform.position)) <= RunRange) && hit.collider.gameObject.tag != "Wall")
        {
            running_away = true;
            agent.SetDestination(-Direction * 5);
            agent.speed = 4;
            CanShoot = false;
        }
       else if ((Mathf.Abs(Vector3.Distance(target.position, transform.position)) <= ShootRange) && hit.collider.gameObject.tag != "Wall")
        {
            agent.speed = 0; ;
            CanShoot = true;
        }
        else {
            running_away = false;
            agent.speed = 3.5f;
            CanShoot = false;   
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.CompareTag("Bullet"))
        {
            if(!WasHurt)
            StartCoroutine(TakeDamage(1));
        }
        if (col.CompareTag("Explode"))
        {
            StartCoroutine(TakeDamage(1));
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
        Instantiate(DropScrap, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        return 0;
    }
}
