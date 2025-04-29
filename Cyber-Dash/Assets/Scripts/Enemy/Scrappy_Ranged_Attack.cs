using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrappy_Ranged_Attack : MonoBehaviour
{
    public float duraion = 3.4f;
    public float activeTime = 3f;
    public int dmg;
    float dot = .01f;
    bool dotNOW = false;
    bool flip = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
         duraion -= Time.deltaTime;
        print(duraion);
        if (duraion <= activeTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
            GetComponent<BoxCollider2D>().enabled = true;
        }
        if (duraion <= 0)
        {
            Destroy(gameObject);
        }

        if(flip)
        dot -= Time.deltaTime;

        if (dot <= 0)
        {
            dot = .1f;
            dotNOW = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
           if (collision.gameObject.tag == "Player")
         {
            //     collision.gameObject.GetComponent<Player_Health>().TakeDamage(dmg);
            flip = true;
          }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (dotNOW)
            {
                collision.gameObject.GetComponent<Player_Health>().TakeDamage(.4f);
                print(duraion + "Collision");
                 dotNOW = false;
            }
        }
    }
   
}
