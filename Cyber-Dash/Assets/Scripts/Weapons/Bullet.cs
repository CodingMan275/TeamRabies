using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    public SaveData bulletEffects;
    public GameObject Explosion;
    public int Damage;
    public int HowManyPierce;

    public int BulletSpeed;
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * BulletSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (bulletEffects.explodingBullets && HowManyPierce == 0)
            {
                print("Create Explo-sion");
                Instantiate(Explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else if (HowManyPierce == 0)
            {
                Destroy(gameObject);
            }
            else if (HowManyPierce != 0)
            {
                HowManyPierce--;
            }
        }
        else if (collision.gameObject.tag == "Wall")
        {
            print("Create Explo-sion");
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
