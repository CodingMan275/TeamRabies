using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public float BeamDuration = .2f;
    public int Damage;
    public SaveData bulletEffects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void updateDmg(int dmg)
    {
        if (bulletEffects.criticalStrike)
        {
            //if 10% chance
            float temp = Random.Range(1f, 10f);
            if (temp <= 1f)
            {
                Damage = dmg * 2;
            }

        }
        else
            Damage = dmg;
    }


    // Update is called once per frame
    void Update()
    {
        BeamDuration -= Time.deltaTime;
        if (BeamDuration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
