using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Miniboss : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 chargePos;

    public float Speed =  1;

    public bool LockedOn = false;
    public bool onWall = false;
    public bool Charging;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
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
        if (rb.velocity != (chargePos * Speed) && Charging)
        { 
         rb.velocity = chargePos * Speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
             Charging = false;
            rb.velocity = chargePos * 0;
            //chargePos = new Vector3(target.position.x * .5f - transform.position.x, target.position.y * .5f - transform.position.y, target.position.z);
           
            // rb.velocity = Vector2.MoveTowards(transform.position, target.position, Speed);
            chargePos = (target.position - transform.position).normalized;
            rb.velocity = chargePos * Speed;
            transform.right = target.position - transform.position;
            Charging = true;
        }
    }
}
