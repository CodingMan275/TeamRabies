using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrappy_Ranged_Attack : MonoBehaviour
{
    public float duraion = .4f;
    public float activeTime = .1f;
    public int dmg;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
         duraion -= Time.deltaTime;
        if (duraion <= activeTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
            GetComponent<BoxCollider2D>().enabled = true;
        }
        if (duraion <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player_Health>().TakeDamage(dmg);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player_Health>().TakeDamage(dmg / 2);
        }
    }
}
