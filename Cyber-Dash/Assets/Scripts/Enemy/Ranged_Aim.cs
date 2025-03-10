using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Aim : MonoBehaviour
{
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Direction = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y);
        transform.up = Direction;
    }
}
