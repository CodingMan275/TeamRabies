using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback_Logic : MonoBehaviour
{

    public float KnockbackDuration = .2f;
    public int KnockbackDist = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KnockbackDuration -= Time.deltaTime;
        if (KnockbackDuration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
