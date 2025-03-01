using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon_Controller : MonoBehaviour
{

    Rigidbody2D rb;
    CapsuleCollider2D col;
    InputController IC;
    SpriteRenderer sr;

    Vector3 player;
    public Vector2 Mouse_Pos;
    Vector3 Spawnloc;

    public GameObject BulletPrefab;

    public float BulletSpeed;
    public float BulletDelay;
    float LastTimeBulletFired;

    public float Gun_Radius;


    float aimAngle;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        IC = transform.parent.GetComponent<InputController>();
        IC.OnShootPressed += Fire;
    }

    // Update is called once per frame
    void Update()
    {
        sr.flipY = (-90 < aimAngle && aimAngle < 90) ? false : true;

        /*
        if (Input.GetMouseButton(0))
      {
            float timeSinceLastFiredBullet = Time.time - LastTimeBulletFired;
            if (timeSinceLastFiredBullet >= BulletDelay)
            {
                Fire();

                LastTimeBulletFired = Time.time;
            }
      }
        */
      
       player = transform.parent.position;
       transform.position = player + new Vector3(Mathf.Cos(aimAngle * Mathf.Deg2Rad) * Gun_Radius, Mathf.Sin(aimAngle * Mathf.Deg2Rad) * Gun_Radius, 0f);
       Spawnloc = player + new Vector3(Mathf.Cos(aimAngle * Mathf.Deg2Rad) * (Gun_Radius + col.size.x/2), Mathf.Sin(aimAngle * Mathf.Deg2Rad) * (Gun_Radius + col.size.y), 0f);

    }

    private void FixedUpdate()
    {
       aimAngle = IC.AimAngle;
       rb.rotation = aimAngle;
 
    }

    public void Fire()
    {
        GameObject Bullet = Instantiate(BulletPrefab, Spawnloc, transform.rotation);
        Bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * BulletSpeed, ForceMode2D.Impulse);
    }
}
