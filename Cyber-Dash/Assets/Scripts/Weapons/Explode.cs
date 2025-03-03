using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    public float ExplosionDuration = .4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ExplosionDuration -= Time.deltaTime;
        if (ExplosionDuration <= 0)
        {
            Destroy(gameObject);
        }
    }

    
}
