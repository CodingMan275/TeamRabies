using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrappy_Health : MonoBehaviour
{
    public float Health;
    public float MaxHealth;
    public Slider HealthBar;

    private SpriteRenderer render;
    private Enemy_Counter EC;

    public SaveData Player;

    private bool WasHurt = false;
    public bool Die = false;

    bool phase1 = true;
    bool phase2 = false;
    bool phase3 = false;

    bool phase1spawn = false;
    bool explosion = false;

    public int phase1hp;
    public int phase2hp;
    public int FirstWaveHP;
    public int FinalExplosion;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        EC = GameObject.Find("Enemy Count").GetComponent<Enemy_Counter>();
        render = GetComponent<SpriteRenderer>();

        phase1hp = (int)(MaxHealth * .70f);
        phase2hp = (int)(MaxHealth * .40f);
        FirstWaveHP = (int)(MaxHealth * .76f);
        FinalExplosion = (int)(MaxHealth * .1f);
         phase1 = true;
         phase2 = false;
         phase3 = false;
        HealthBar.maxValue = MaxHealth;
        HealthBar.value = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (Health <= 0 && !Die)
        {
            Die = true;
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.CompareTag("Bullet"))
        {
            print("Bullet hit");
            if (col.GetComponent<Bullet>() != null)
                StartCoroutine(TakeDamage(col.GetComponent<Bullet>().Damage));
            else if (col.GetComponent<Bolt>() != null)
                StartCoroutine(TakeDamage(col.GetComponent<Bolt>().Damage));
            else
                StartCoroutine(TakeDamage(col.GetComponent<Beam>().Damage));
        }
    }
    public IEnumerator TakeDamage(float dmg)
    {
        Health -= dmg;    //Replace 1 with bull damage
        HealthBar.value = Health;

        if ((Health <= FirstWaveHP) && !phase2 && !phase1spawn)
        {
            //For michael, not sure how you want to do this
            GetComponent<Scrappy_Phase1>().Spawn();
            print("Phase 1 spawn");
            phase1spawn = true;
        }
        else if ((Health <= phase1hp) && !phase2)
        {
            print("phase 2 should be here");
            phase2 = true;
            GetComponent<Scrappy_Phase1>().enabled = false;
            GetComponent<Scrappy_Phase2>().enabled = true;
        }
        else if ((Health <= phase2hp) && !phase3)
        {
            phase3 = true;
            GetComponent<Scrappy_Phase2>().enabled = false;
            GetComponent<Scrappy_Phase3>().enabled = true;
        }
        else if ((Health <= FinalExplosion) && !explosion)
        {
            explosion = true;
            print("EXPLODE");
           StartCoroutine(GetComponent<Scrappy_Phase3>().Explode());
        }

        if (!WasHurt)
        {
            WasHurt = true;
            render.color = new Color(255f, 0f, 0f, 255f);
            yield return new WaitForSeconds(.05f);
            render.color = new Color(255f, 255f, 255f, 255f);
            yield return new WaitForSeconds(.05f);
        }
        if (Health > 0)
            WasHurt = false;
    }

    public float Death()
    {
        Player.killCount++;
        EC.UpdateCounter();
        Destroy(this.gameObject);
        return 0;
    }
}
