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
        // Find all Circle Collider 2D components in the scene
        CircleCollider2D[] circleColliders = FindObjectsOfType<CircleCollider2D>();

        // Get this object's Collider 2D
        Collider2D thisCollider = GetComponent<Collider2D>();

        // Iterate through all found Circle Collider 2Ds and ignore collisions
        foreach (CircleCollider2D circleCollider in circleColliders)
        {
            Physics2D.IgnoreCollision(thisCollider, circleCollider, true);
        }
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
            if (bulletEffects.explodingBullets)
            {
                print("Create Explo-sion");
                Instantiate(Explosion, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

    }
}
