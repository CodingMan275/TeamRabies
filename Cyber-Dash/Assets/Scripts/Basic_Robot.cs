using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Basic_Robot : MonoBehaviour
{
    // private variable that can be seen and edited in Unity
    [SerializeField] private Enemy_Counter EC;
    private SpriteRenderer render;

    private float Health;
    public float MaxHealth;
    private bool WasHurt = false;

    public SaveData Player;

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
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.Find("Player").GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.CompareTag("Bullet"))
        {
            if(!WasHurt)
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
        Destroy(this.gameObject);
        return 0;
    }
}
