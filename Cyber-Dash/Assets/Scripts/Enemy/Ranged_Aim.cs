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

    [Tooltip("Alfe has a 1/x chance to scream")][Range(1, 5)] public int ALFEVocal = 1;
    public AudioSource[] Hurt;


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
        if (parent.CanShoot && (timeSinceLastFiredBullet > BulletDelay) && !anim.GetBool("Death"))
        {
            StartCoroutine(Fire());
        }
    }

    int var = 0;
    IEnumerator Fire()
    {
        anim.Play("Attack");
        GameObject Bullet = Instantiate(enemy_bullet, transform.position, transform.rotation);
        Bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * BulletSpeed, ForceMode2D.Impulse);
        LastTimeBulletFired = Time.time;
        if (Random.Range(0, ALFEVocal) == 0 && !Hurt[var].isPlaying)
        {
            var = (int)Random.Range(0, Hurt.Length);
            Hurt[var].Play();
            yield return new WaitForSeconds(Hurt[var].clip.length);
        }
        else
            yield return new WaitForSeconds(.15f);
    }
}
