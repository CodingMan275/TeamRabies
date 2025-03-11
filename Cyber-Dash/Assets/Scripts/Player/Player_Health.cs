using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public float MaxHP = 10;
    public float HP;
    private bool WasHurt = false;
    private bool scream = false;
    public Slider HealthBar;
    SpriteRenderer render;
    SceneController SC;


    [Range(1,5)]public int ALFEVocal = 1;
    public AudioSource Hurt;
    public AudioClip[] HurtSound;

    // Start is called before the first frame update
    void Start()
    {
        SC = transform.GetChild(0).GetComponent<SceneController>();
        HP = MaxHP;
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.value = HP;
    }

    public void TakeDamage(float dmg)
    {
        Hurt.Play();
        HP -= HP - dmg <= 0 ? Death() : dmg;
        if(!WasHurt)
            StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        WasHurt = true;
        if(!scream)
            StartCoroutine(HurtScream());

        render.color = new Color(255f, 0f, 0f, 255f);
        yield return new WaitForSeconds(.15f);
        render.color = new Color(255f, 255f, 255f, 255f);
        yield return new WaitForSeconds(.15f);
        WasHurt = false;
    }

    private IEnumerator HurtScream()
    {
        scream = true;
        Hurt.clip = HurtSound[0];
        Hurt.Play();
        yield return new WaitForSeconds(HurtSound[0].length);
        if(Random.Range(0, ALFEVocal)==0)
        { 
            Hurt.clip = HurtSound[1];
            Hurt.Play();
        }
        yield return new WaitForSeconds(0.5f);
        scream = false;
    }


    private float Death()
    {
        SC.Defeat();
        return 0;
    }
}
