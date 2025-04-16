using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam_Logic : MonoBehaviour
{

   // public int Damage;

    public GameObject BeamPrefab;

    public double BulletDelay = 3;
    bool Charged = false;
    public Weapon_Controller WPC;
    InputController IC;

    [Range(1, 5)] public int ALFEVocal = 1;
    public AudioSource Shoot;
    public AudioSource Voice;
    public AudioClip[] PistolSound;
    private bool CanScream = true;

    public SaveData stats;

    public GameObject visibleCharge;
    float maxChrage = 2f;
    float minCharge = 0f;
    float curCharge = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        IC = transform.parent.GetComponent<InputController>();
        WPC = GetComponent<Weapon_Controller>();
        IC.OnShootPressed += Charge;
        IC.onShootReleased += Fire;
        visibleCharge.SetActive(true);
    }
    private void OnEnable()
    {
        IC.OnShootPressed +=Charge;
        IC.onShootReleased += Fire;
        visibleCharge.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Charge()
    {
        //set charge time
        curCharge += .01f;
        if (curCharge >= 2f)
        {
            curCharge = 2f;
            visibleCharge.GetComponent<SpriteRenderer>().color = new Color(254, 250, 0, 255);
            Charged = true;
        }
        visibleCharge.GetComponent<SpriteRenderer>().size = new Vector2(minCharge + curCharge,.36f);
    }

    public void Fire()
    {
        print("Released");
        curCharge = 0;
        visibleCharge.GetComponent<SpriteRenderer>().size = new Vector2(minCharge, .36f);
        visibleCharge.GetComponent<SpriteRenderer>().color = new Color(0, 0, 104, 255);

        if (WPC.CanFire && Charged)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
            Debug.DrawRay(transform.position, transform.right, Color.magenta);
            print("Ray Drawn!");
            if (hit.collider.gameObject.tag == "Wall" && hit.collider.gameObject != null)
            {
                GameObject Bullet = Instantiate(BeamPrefab, WPC.Spawnloc, transform.rotation);
                Bullet.GetComponent<Beam>().updateDmg(stats.beamDamage * (stats.UD.DoubleDamage ? 2 : 1));
                float dist = Mathf.Abs(Vector3.Distance(hit.collider.transform.position, transform.position));
                Bullet.transform.localScale = new Vector3(dist, 1, 1);
            }
            else
            {
                print(hit.collider.gameObject.tag);
            }
        }
        Charged = false;
    
    }
    private void OnDisable()
    {
        IC.OnShootPressed -= Charge;
        IC.onShootReleased -= Fire;
        visibleCharge.SetActive(false);
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
