using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    private bool WasHurt = false;
    public Slider HealthBar;
    public Slider ExtraHealthBar;
    public SaveData stats;
    SpriteRenderer render;
    SceneController SC;


    [Tooltip("Alfe has a 1/x chance to scream")][Range(1,5)]public int ALFEVocal = 1;
    public AudioSource[] Hurt;

    // Start is called before the first frame update
    void Start()
    {
        SC = transform.GetChild(0).GetComponent<SceneController>();
        HealthBar.maxValue = stats.MaxHealth + stats.healthBuff;
        HealthBar.value = stats.Health;
        ExtraHealthBar.value = stats.ExtraHealth;
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (WasHurt && !Hurt[1].isPlaying &&Random.Range(0, ALFEVocal) == 0)  
                Hurt[1].Play();
    }

    public void TakeDamage(float dmg)
    {
        if (stats.adaptiveArmor)
        {
            //Change var with balancing
            dmg -= 3;
        }
        if (ExtraHealthBar.value > 0 && dmg < ExtraHealthBar.value)
            stats.ExtraHealth -= (int)dmg;
        else if(ExtraHealthBar.value == 0) 
        stats.Health -= stats.Health - dmg <= 0 ? Death() : (int)dmg;
        else
        {
            int remainingDmg = (int)(dmg - ExtraHealthBar.value);
            stats.ExtraHealth = 0;
            stats.Health -= stats.Health - remainingDmg <= 0 ? Death() : remainingDmg;
        }


        ExtraHealthBar.value = stats.ExtraHealth;
        HealthBar.value = stats.Health;
        if (!WasHurt)
            StartCoroutine(Flash());
    }

    public void ForceHeal(int HP)
    {
        stats.ExtraHealth += HP; 
        ExtraHealthBar.value = stats.ExtraHealth;
    }

    public void LoseShield(int HP)
    {
        stats.ExtraHealth = stats.ExtraHealth-HP < 0 ? 0 : stats.ExtraHealth-HP;
        ExtraHealthBar.value = stats.ExtraHealth;
    }

    private IEnumerator Flash()
    {
        WasHurt = true;
        Hurt[0].Play();
        render.color = new Color(255f, 0f, 0f, 255f);
        yield return new WaitForSeconds(.15f);
        render.color = new Color(255f, 255f, 255f, 255f);
        yield return new WaitForSeconds(.15f);
        WasHurt = false;
    }


    private int Death()
    {
        if (stats.lastStand)
        {
            stats.lastStand = false;
            stats.Health = stats.MaxHealth + stats.healthBuff;
            HealthBar.value = stats.MaxHealth;
            stats.ExtraHealth = stats.healthBuff;
            if (stats.Round == 2)
            {
                SC.ChangeScene("Arena 1-2");
            }
            else if (stats.Round == 3)
            {
                SC.ChangeScene("Arena 1-3");
            }
            else if (stats.Round == 4)
            {
                SC.ChangeScene("Arena 1-4");
            }
            else if (stats.Round == 5)
            {
                SC.ChangeScene("Arena 1-5");
            }
        }
        else
        {
            SC.ChangeScene("Defeat");
        }
        return 0;
    }
}
