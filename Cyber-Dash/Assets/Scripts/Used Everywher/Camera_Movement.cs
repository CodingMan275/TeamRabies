using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    Transform player;
    public Vector3 Camera_Offset = new Vector3(0, 1, -5);

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent;
        transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + Camera_Offset;
    }
}
