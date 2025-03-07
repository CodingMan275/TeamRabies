using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt_Launcher_Logic : MonoBehaviour
{
    public GameObject BoltPrefab;

    public float BulletSpeed = 20;
    public double BulletDelay = 1;
    float LastTimeBulletFired;
    float timeSinceLastFiredBullet;
    public Weapon_Controller WPC;
    InputController IC;
    public AudioSource Shoot;

    public SaveData stats;
    // Start is called before the first frame update
    void Awake()
    {
        IC = transform.parent.GetComponent<InputController>();
        WPC = GetComponent<Weapon_Controller>();
        IC.OnShootPressed += Fire;
        //stats = 
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
            Shoot.Play();
            GameObject Bullet = Instantiate(BoltPrefab, WPC.Spawnloc, transform.rotation);
            Bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * BulletSpeed, ForceMode2D.Impulse);
            LastTimeBulletFired = Time.time;
        }
    }

    private void OnDisable()
    {
        IC.OnShootPressed -= Fire;
    }
}
