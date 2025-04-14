using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_Logic : MonoBehaviour
{
    public GameObject BulletPrefab;

    public int Damage;

    public float BulletSpeed;
    public double BulletDelay;
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

    void Awake()
    {
        IC = transform.parent.GetComponent<InputController>();
        WPC = GetComponent<Weapon_Controller>();
       // IC.OnShootPressed += Fire;
    }

    private void OnEnable()
    {
        IC.OnShootPressed += Fire;
    }

    // Update is called once per frame
    void Update()
    {
            timeSinceLastFiredBullet = Time.time - LastTimeBulletFired;
    }

    public void Fire()
    { 
        if ((timeSinceLastFiredBullet > BulletDelay * Mathf.Abs( 1 - stats.FireRateMod - stats.PistolFireRateMod)) && WPC.CanFire)
        {
            GameObject Bullet = Instantiate(BulletPrefab, WPC.Spawnloc, transform.rotation);
            Bullet.GetComponent<Bullet>().BulletSpeed = (int)BulletSpeed;
            Bullet.GetComponent<Bullet>().updateDmg(Damage * (stats.DoubleDamage ? 2: 1));
            Bullet.GetComponent<Bullet>().HowManyPierce = stats.HowManyPierce;
            //  Bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * BulletSpeed, ForceMode2D.Impulse);
            LastTimeBulletFired = Time.time;
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
