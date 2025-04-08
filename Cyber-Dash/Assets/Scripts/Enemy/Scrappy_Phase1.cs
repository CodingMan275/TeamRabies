using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrappy_Phase1 : MonoBehaviour
{
    public float speed = 8f;
    Transform target;
    Rigidbody2D rb;
    Vector3 dir;
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
    }

    private void FixedUpdate()
    {
            rb.velocity = new Vector2(dir.x, dir.y) * speed;
    }
}
