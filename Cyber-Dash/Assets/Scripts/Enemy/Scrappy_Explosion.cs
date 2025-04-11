using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrappy_Explosion : MonoBehaviour
{
    float startSize = .1f;
    float maxSize = 26.3f;

    public int dmg;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(startSize,startSize,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        startSize += .1f;
        if (startSize >= maxSize)
        {
            Destroy(gameObject);
        }
        gameObject.transform.localScale = new Vector3(startSize, startSize, 1);

        //Expand thing
        //if thing max size destroy self
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player_Health>().TakeDamage(dmg);
        }
    }
}
