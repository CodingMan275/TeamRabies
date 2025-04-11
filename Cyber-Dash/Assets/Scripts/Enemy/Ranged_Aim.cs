using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Aim : MonoBehaviour
{
    [SerializeField] Transform target;
    Ranged_Robot parent;
    public GameObject enemy_bullet;
    public float BulletSpeed;
    public double BulletDelay;
    float LastTimeBulletFired;
    double timeSinceLastFiredBullet;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Ranged_Robot>();
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.CanShoot)
        {
            timeSinceLastFiredBullet = Time.time - LastTimeBulletFired;
        }
        else
        {
            timeSinceLastFiredBullet = BulletDelay;
        }
        Vector3 Direction = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y);
        transform.up = Direction;
        if (parent.CanShoot && (timeSinceLastFiredBullet > BulletDelay))
        {
            anim.Play("Attack");
            Fire();
        }
    }


    void Fire()
    {
        GameObject Bullet = Instantiate(enemy_bullet, transform.position, transform.rotation);
        Bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * BulletSpeed, ForceMode2D.Impulse);
        LastTimeBulletFired = Time.time;
    }
}
