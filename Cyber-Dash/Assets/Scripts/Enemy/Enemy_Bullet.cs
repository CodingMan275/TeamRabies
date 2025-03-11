using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    [SerializeField] private float AttackDmg;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.tag == "Player")
            {
                Player = col;
               col.GetComponent<Player_Health>().TakeDamage(AttackDmg);
            }
            Destroy(gameObject);
        }
    }
}
