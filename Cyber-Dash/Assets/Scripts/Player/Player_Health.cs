using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    private bool WasHurt = false;
    public Slider HealthBar;
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
        stats.Health -= stats.Health - dmg <= 0 ? Death() : (int)dmg;
        HealthBar.value = stats.Health;
        if (!WasHurt)
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


    private int Death()
    {
        SC.ChangeScene("Defeat");
        return 0;
    }
}
