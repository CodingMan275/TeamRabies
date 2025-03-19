using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public float BeamDuration = .2f;
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        
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
