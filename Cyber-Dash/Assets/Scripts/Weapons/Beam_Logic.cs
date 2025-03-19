using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam_Logic : MonoBehaviour
{

    public int Damage;

    public GameObject BeamPrefab;

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
    void Start()
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
            Transform playerTM = GetComponentInParent<Transform>();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
            Debug.DrawRay(transform.position, transform.right, Color.magenta);
            print("Ray Drawn!");
            if (hit.collider.gameObject.tag == "Wall" && hit.collider.gameObject != null)
            {
                GameObject Bullet = Instantiate(BeamPrefab, WPC.Spawnloc, transform.rotation);
                Bullet.GetComponent<Beam>().Damage = 5;
                float dist = Mathf.Abs(Vector3.Distance(hit.collider.transform.position, transform.position));
                Bullet.transform.localScale = new Vector3(dist,1,1);
            }
            else {
                print(hit.collider.gameObject.tag);
            }
            LastTimeBulletFired = Time.time;
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
