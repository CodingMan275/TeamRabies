using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrappy_Phase2 : MonoBehaviour
{
    public float speed = 14f;
    Transform target;
    Rigidbody2D rb;
    Vector3 dir;

    float AttackDelay = 4.0f;
    float AttackTimer = 4.0f;

    int rotateAttacks = 0;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = (target.position - transform.position).normalized;
        AttackTimer = AttackTimer - Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            player = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            player = null;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dir.x, dir.y) * speed;
    }

    void Attack()
    {
        rotateAttacks++;
        if (rotateAttacks == 2)
        {
            Spawn();
        }
        else if (rotateAttacks == 4)
        {
            //Misslles
            rotateAttacks = 0;
        }
        else if (player != null)
        {
            //melee attack
            //Play attack animation
        }
        else
        {
            //Ranged attack
        }
        AttackTimer = AttackDelay;
    }

    //FOr michael
    public void Spawn()
    {

    }
}
