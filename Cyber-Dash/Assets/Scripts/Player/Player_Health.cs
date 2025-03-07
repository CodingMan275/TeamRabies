using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public float MaxHP = 10;
    public float HP;
   // private bool WasHurt = false;
    public Slider HealthBar;
    SpriteRenderer render;
    SceneController SC;
    public AudioSource Hurt;

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

    public IEnumerator TakeDamage(float dmg)
    {
        Hurt.Play();
        HP -= HP - dmg <= 0 ? Death() : dmg;
        render.color = new Color(255f, 0f, 0f, 255f);
        yield return new WaitForSeconds(.15f);
        render.color = new Color(255f, 255f, 255f, 255f);
        yield return new WaitForSeconds(.15f);
    }

    private float Death()
    {
        SC.Defeat();
        return 0;
    }
}
