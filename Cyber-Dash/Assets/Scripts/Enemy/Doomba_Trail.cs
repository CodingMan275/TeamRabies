using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doomba_Trail : MonoBehaviour
{
    public float Duration = 5f;
    public int dmg = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Duration -= Time.deltaTime;
        if (Duration <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { 
        collision.GetComponent<Player_Health>().TakeDamage(dmg);
        }
    }
}
