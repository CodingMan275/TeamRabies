using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    // Start is called before the first frame update

    public SaveData bulletEffects;
    public GameObject Explosion;
    public int Damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateDmg(int dmg)
    {
        if (bulletEffects.criticalStrike)
        {
            //if 10% chance
            float temp = Random.Range(1f, 10f);
            if (temp <= 1f)
            {
                Damage = dmg * 2;
            }

        }
        else
            Damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (bulletEffects.explodingBullets)
            {
                Instantiate(Explosion, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
