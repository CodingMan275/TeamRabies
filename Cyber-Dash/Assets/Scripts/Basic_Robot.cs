using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Robot : MonoBehaviour
{
    // private variable that can be seen and edited in Unity
    [SerializeField] private Enemy_Counter EC;
    private SpriteRenderer render;

    public float Health;
    public float MaxHealth;
    private bool WasHurt = false;

    // Called First frame when object spawns
    void Awake()
    {
        Health = MaxHealth;
        render = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.CompareTag("Bullet"))
        {
            if(!WasHurt)
            StartCoroutine(FlashRed());
            Health -= Health - 1 <= 0 ? Death() : 1;    //Replace 1 with bull damage
        }
    }

    public IEnumerator FlashRed()
    {
        WasHurt = true;
        render.color = new Color(255f, 0f, 0f, 255f);
        yield return new WaitForSeconds(.25f);
        render.color = new Color(255f, 255f, 255f, 255f);
        yield return new WaitForSeconds(.25f);
        WasHurt = false;
    }

    public float Death()
    {
        EC.UpdateCounter();
        Destroy(this.gameObject);
        return 0;
    }
}
