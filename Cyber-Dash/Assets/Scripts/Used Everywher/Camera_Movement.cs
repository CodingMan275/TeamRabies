using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    Transform player;
    public Vector3 Camera_Offset = new Vector3(0, 1, -5);
    public float minX , maxX , minY , maxY ;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent;
        transform.SetParent(null);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = player.position.x + Camera_Offset.x;
        float y = player.position.y + Camera_Offset.y;
        if (x > maxX) { x = maxX; }
        if (x < minX) { x = minX; }
        if (y > maxY) { y = maxY; }
        if (y < minY) { y = minY; }
        transform.position = new Vector3(x, y, player.position.z + Camera_Offset.z);
    }
}
