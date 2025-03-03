using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_Logic : MonoBehaviour
{

    public GameObject BulletPrefab;

    public float BulletSpeed;
    public double BulletDelay;
    float LastTimeBulletFired;
    float timeSinceLastFiredBullet;
    public Weapon_Controller WPC;
    InputController IC;

    public SaveData stats;


    // Start is called before the first frame update
    void Awake()
    {
        IC = transform.parent.GetComponent<InputController>();
        WPC = GetComponent<Weapon_Controller>();
        IC.OnShootPressed += Fire;
    }

    // Update is called once per frame
    void Update()
    {
            timeSinceLastFiredBullet = Time.time - LastTimeBulletFired;
    }

    public void Fire()
    { 
        if ((timeSinceLastFiredBullet > BulletDelay * stats.FireRateMod) && WPC.CanFire)
        {
            GameObject Bullet = Instantiate(BulletPrefab, WPC.Spawnloc, transform.rotation);
            Bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * BulletSpeed, ForceMode2D.Impulse);
            LastTimeBulletFired = Time.time;
        }
    }
    private void OnDisable()
    {
        IC.OnShootPressed -= Fire;
    }
}
