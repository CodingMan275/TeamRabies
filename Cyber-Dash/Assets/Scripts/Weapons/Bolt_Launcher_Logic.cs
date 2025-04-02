using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt_Launcher_Logic : MonoBehaviour
{
    public GameObject BoltPrefab;

    public int Damage;

    public float BulletSpeed = 20;
    public double BulletDelay = 1;
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
    // Start is called before the first frame update
    void Awake()
    {
        IC = transform.parent.GetComponent<InputController>();
        WPC = GetComponent<Weapon_Controller>();
      //  IC.OnShootPressed += Fire;
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
        if ((timeSinceLastFiredBullet > BulletDelay * Mathf.Abs(1 - stats.FireRateMod - stats.BoltLauncherFireRateMod)) && WPC.CanFire)
        {
            GameObject Bullet = Instantiate(BoltPrefab, WPC.Spawnloc, transform.rotation);
            Bullet.GetComponent<Bolt>().Damage = Damage;
            Bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * BulletSpeed, ForceMode2D.Impulse);
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
