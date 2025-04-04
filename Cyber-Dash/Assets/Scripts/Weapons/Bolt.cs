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
