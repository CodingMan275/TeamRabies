using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Controller : MonoBehaviour
{

    public Rigidbody2D rb;

    public Transform player_transform;

    public GameObject BulletPrefab;

    public Transform FirePoint;

    public float BulletSpeed;

    public float BulletDelay;

    float LastTimeBulletFired;

    Vector2 Mouse_Pos;

    float aimAngle;

    Vector3 Offset;

    // Start is called before the first frame update
    void Start()
    {
        Offset = transform.position - player_transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Mouse_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = player_transform.transform.position + Offset;
        if (Input.GetMouseButton(0))
        {
            float timeSinceLastFiredBullet = Time.time - LastTimeBulletFired;
            if (timeSinceLastFiredBullet >= BulletDelay)
            {
                Fire();

                LastTimeBulletFired = Time.time;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 aimDirection = Mouse_Pos - rb.position;
        aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = aimAngle;
        
    }

    public void Fire()
    {
        GameObject Bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Bullet.GetComponent<Rigidbody2D>().AddForce(FirePoint.up * BulletSpeed, ForceMode2D.Impulse);
    }
}
