using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon_Controller : MonoBehaviour
{

    Rigidbody2D rb;
    CapsuleCollider2D col;
    InputController IC;
    SpriteRenderer sr;

    Vector3 player;
    public Vector2 Mouse_Pos;
    [HideInInspector]
    public Vector3 Spawnloc;

    public float Gun_Radius;

    public bool CanFire = true;


    float aimAngle;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        IC = transform.parent.GetComponent<InputController>();
      //  IC.OnShootPressed += Fire;
    }

    // Update is called once per frame
    void Update()
    {
        sr.flipY = (-90 < aimAngle && aimAngle < 90) ? false : true;

      
       player = transform.parent.position;
       transform.position = player + new Vector3(Mathf.Cos(aimAngle * Mathf.Deg2Rad) * Gun_Radius, Mathf.Sin(aimAngle * Mathf.Deg2Rad) * Gun_Radius, 0f);
       Spawnloc = player + new Vector3(Mathf.Cos(aimAngle * Mathf.Deg2Rad) * (Gun_Radius + col.size.x/2), Mathf.Sin(aimAngle * Mathf.Deg2Rad) * (Gun_Radius + col.size.y), 0f);

    }

    private void FixedUpdate()
    {
       aimAngle = IC.AimAngle;
       rb.rotation = aimAngle;
 
    }

    public void AssignWeapon(string Weapon)
    {
        switch (Weapon)
        {
            case "Bolt Launcher": OnDisable();
                GetComponent<Bolt_Launcher_Logic>().enabled = true;
                break;
            case "Pistol":
                OnDisable();
                GetComponent<Pistol_Logic>().enabled = true;
                break;
            default:
                
                break;
        }
    }
    private void OnDisable()
    {
        GetComponent<Pistol_Logic>().enabled = false;
        GetComponent<Bolt_Launcher_Logic>().enabled = false;
    }
}
