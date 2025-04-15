using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    [SerializeField] private float AttackDmg;
    private GameObject Player;
    public SaveData sd;
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
                //Won't run damage coroutine if 10% chance hit with ability
                float temp = Random.Range(1f, 10f);
                if (sd.energyDeflector && (temp <= 1f))
                {

                }
                else
                {
                    Player = col;
                    col.GetComponent<Player_Health>().TakeDamage(AttackDmg);
                }
            }
            Destroy(gameObject);
        }
    }
}
