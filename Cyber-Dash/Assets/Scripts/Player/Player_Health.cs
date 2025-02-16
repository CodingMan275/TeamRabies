using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public SceneController SC;

    public float MaxHP = 10;
    public float HP;
    private bool WasHurt = false;
    public Slider HealthBar;
    SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.value = HP;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.CompareTag("Enemy"))
        {
           // if (!WasHurt)
            //    StartCoroutine(TakeDamage(col.GetComponent<Basic_Robot>().AttackDmg));
        }
    }

    public IEnumerator TakeDamage(float dmg)
    {
        HP -= HP - dmg <= 0 ? Death() : dmg;    //Replace 1 with bull damage
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
