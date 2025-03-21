using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public float MaxHP = 10;
    public float HP;
    private bool WasHurt = false;
    public Slider HealthBar;
    SpriteRenderer render;
    SceneController SC;


    [Tooltip("Alfe has a 1/x chance to scream")][Range(1,5)]public int ALFEVocal = 1;
    public AudioSource[] Hurt;

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

    private void FixedUpdate()
    {
        if (WasHurt && !Hurt[1].isPlaying &&Random.Range(0, ALFEVocal) == 0)  
                Hurt[1].Play();
    }

    public void TakeDamage(float dmg)
    {
        HP -= HP - dmg <= 0 ? Death() : dmg;
        if(!WasHurt)
            StartCoroutine(Flash());
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


    private float Death()
    {
        SC.Defeat();
        return 0;
    }
}
