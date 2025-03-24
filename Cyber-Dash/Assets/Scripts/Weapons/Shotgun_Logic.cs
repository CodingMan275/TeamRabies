using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_Logic : MonoBehaviour
{

    public GameObject BulletPrefab;
    public GameObject KnockbackPrefab;

    public int DamagePerBullet;

    public double ReloadTime = 2;

    public float BulletSpeed;
    public double BulletDelay;
    double holdDelay;
    float LastTimeBulletFired;
    float timeSinceLastFiredBullet;
    public Weapon_Controller WPC;
    InputController IC;

    [Range(1, 5)] public int ALFEVocal = 1;
    public AudioSource Shoot;
    public AudioSource Voice;
    public AudioClip[] PistolSound;
    private bool CanScream = true;

    public SaveData stats;

    int shootCount;

    // Start is called before the first frame update
    void Start()
    {
        holdDelay = BulletDelay;
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
        if ((timeSinceLastFiredBullet > BulletDelay * Mathf.Abs(1 - stats.FireRateMod - stats.ShotgunFireRateMod)) && WPC.CanFire)
        {
            GameObject Bullet = Instantiate(BulletPrefab, WPC.Spawnloc, transform.rotation);
            GameObject Bullet1 = Instantiate(BulletPrefab, WPC.Spawnloc, transform.rotation * Quaternion.Euler(new Vector3(0, 0, -15f)));
            GameObject Bullet2 = Instantiate(BulletPrefab, WPC.Spawnloc, transform.rotation * Quaternion.Euler(new Vector3(0, 0, +15f)));
            GameObject Bullet3 = Instantiate(BulletPrefab, WPC.Spawnloc, transform.rotation * Quaternion.Euler(new Vector3(0, 0, -30f)));
            GameObject Bullet4 = Instantiate(BulletPrefab, WPC.Spawnloc, transform.rotation * Quaternion.Euler(new Vector3(0, 0, +30f)));
            Bullet.GetComponent<Bullet>().BulletSpeed = (int)BulletSpeed;
            Bullet1.GetComponent<Bullet>().BulletSpeed = (int)BulletSpeed;
            Bullet2.GetComponent<Bullet>().BulletSpeed = (int)BulletSpeed;
            Bullet3.GetComponent<Bullet>().BulletSpeed = (int)BulletSpeed;
            Bullet4.GetComponent<Bullet>().BulletSpeed = (int)BulletSpeed;
            Bullet.GetComponent<Bullet>().Damage = DamagePerBullet;
            Bullet1.GetComponent<Bullet>().Damage = DamagePerBullet;
            Bullet2.GetComponent<Bullet>().Damage = DamagePerBullet;
            Bullet3.GetComponent<Bullet>().Damage = DamagePerBullet;
            Bullet4.GetComponent<Bullet>().Damage = DamagePerBullet;

            GameObject KnockBack = Instantiate(KnockbackPrefab, WPC.Spawnloc, transform.rotation * Quaternion.Euler(new Vector3(0, 0, 90f)));
            shootCount++;
            if (shootCount == 2)
            {
                shootCount = 0;
                BulletDelay = ReloadTime;
            }
            else
            {
                BulletDelay = holdDelay;
                LastTimeBulletFired = Time.time;
            }
            StartCoroutine(Bang());
        }
    }
    private void OnDisable()
    {
        IC.OnShootPressed -= Fire;
    }


    private IEnumerator Bang()
    {
        Shoot.clip = PistolSound[0];
        Shoot.Play();
        yield return new WaitForSeconds(PistolSound[0].length);
        if (Random.Range(0, ALFEVocal) == 0 && CanScream)
        {
            CanScream = false;
            int voice = Random.Range(1, 3);
            Voice.clip = PistolSound[voice];
            Voice.Play();
            yield return new WaitForSeconds(PistolSound[voice].length);
            CanScream = true;
        }
        yield return new WaitForSeconds(0.5f);

    }
}
