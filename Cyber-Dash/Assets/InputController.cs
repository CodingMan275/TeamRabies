using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputController : MonoBehaviour
{

    public Vector2 LInput { get; private set; }
    public float AimAngle { get; private set; }

    public bool isRunning { get; private set; }

    public event Action OnDashPressed;
    public event Action OnShootPressed;

    public PlayerControl pc;
    private InputAction move;
    private InputAction Maim;
    private InputAction Gaim;
    private InputAction run;
    private InputAction Dash;
    private InputAction Shoot;



    // This is to rotate our gun to where we want to aim
    public float RotateSpeed = 200; // Speed of roataion
    public float AngleSnap = 3f;
    public float TAngle; // Where we want to aim
    public float CAngle = 0; // where we currently are aiming


    private void OnEnable()
    {
        move = pc.PlayerInput.Movement;
        Maim = pc.PlayerInput.MouseAim;
        Gaim = pc.PlayerInput.GamepadAim;
        run = pc.PlayerInput.Running;
        Dash = pc.PlayerInput.Dash;
        Shoot = pc.PlayerInput.Shoot;

        move.Enable();
        Maim.Enable();
        Gaim.Enable();
        run.Enable();
        Dash.Enable();
        Shoot.Enable();


    }

    private void OnDisable()
    {
        move.Disable();
        Maim.Disable();
        Gaim.Disable();
        run.Disable();
        Dash.Disable();
        Shoot.Disable();

    }

    private void Awake()
    {
        pc = new PlayerControl();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       LInput = move.ReadValue<Vector2>();
       isRunning = run.IsPressed();


        if (Dash.triggered)
        {
            OnDashPressed?.Invoke();
        }

        if (Shoot.IsInProgress()) //Previously .triggered
        {
            OnShootPressed?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        // Do nothing if TA is within AngleSnap of CAngle
        // Thats prbably why jtters



        Vector2 G = Gaim.ReadValue<Vector2>();
        Vector2 Mouse_Pos = (Vector2)Camera.main.ScreenToWorldPoint(Maim.ReadValue<Vector2>()) - new Vector2(transform.position.x, transform.position.y);
        
        float angleDifference = Mathf.DeltaAngle(CAngle, TAngle);
        CAngle = Mathf.Abs(angleDifference) <= AngleSnap ? TAngle : (CAngle += Mathf.Sign(angleDifference) * RotateSpeed * Time.deltaTime);
        
        //if a controller is connected look at its input, if it gives an input read it, else dont
        // if a controller is not connected then we look at mouse position
        TAngle = Gamepad.all.Count > 0 ? (  G != Vector2.zero ? Mathf.Atan2(G.y, G.x) * Mathf.Rad2Deg : CAngle) : Mathf.Atan2(Mouse_Pos.y, Mouse_Pos.x) * Mathf.Rad2Deg;

        
        // limit Current Angle to be within -180 t0 180
        if (CAngle > 180.0f)
        {
            CAngle -= 360.0f;
        }
        else if (CAngle < -180.0f)
        {
            CAngle += 360.0f;
        }

        AimAngle = CAngle;
    }
}
