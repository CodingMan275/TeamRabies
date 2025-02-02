using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Controller : MonoBehaviour
{

    public Rigidbody2D rb;

    public Transform player_transform;

    public Player_Controller player;

    Vector2 Mouse_Pos;

    float aimAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mouse_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 aimDirection = Mouse_Pos - rb.position;
        if (player.isRight)
        {
            aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg + 90f;
        }
        else
        {
            aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        }
        rb.rotation = aimAngle;
        transform.position = player_transform.transform.position;
    }
}
