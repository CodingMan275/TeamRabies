using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
public class ControllerCurosr : MonoBehaviour
{
   // [SerializeField] private PlayerInput playerInput;
    [SerializeField] private RectTransform CursorTransform;
    [SerializeField] private float mouseSpeed = 1000;
    [SerializeField] private RectTransform canvasTransform;
    [SerializeField]private Camera cam;
    private Mouse virtualMouse;
    private bool prevMouseState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (virtualMouse == null)
        {
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else if (virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }

     //   InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);
        

        if (CursorTransform != null)
        {
            Vector2 position = CursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, position);
        }

        InputSystem.onAfterUpdate += UpdateMotion;
    }

    private void OnDisable()
    {
        if(virtualMouse != null && virtualMouse.added)
        InputSystem.RemoveDevice(virtualMouse);
        InputSystem.onAfterUpdate -= UpdateMotion;
    }

    private void UpdateMotion() {
        if (virtualMouse == null || Gamepad.current == null)
        { return; }

        Vector2 StickValue = Gamepad.current.rightStick.ReadValue();
        StickValue *= mouseSpeed * Time.deltaTime;

        Vector2 currentpos = virtualMouse.position.ReadValue();

        Vector2 newpos = currentpos + StickValue;

      //  newpos.x = Mathf.Clamp(newpos.x, 0, Screen.width);
       // newpos.y = Mathf.Clamp(newpos.y, 0, Screen.height);

        InputState.Change(virtualMouse.position, newpos);
        InputState.Change(virtualMouse.delta, StickValue);

        //Michael change later with your input system
        bool aButtonIspRessed = Gamepad.current.rightTrigger.isPressed;
        if (prevMouseState != aButtonIspRessed)
        {
            virtualMouse.CopyState<MouseState>(out var mouseState);
            mouseState.WithButton(MouseButton.Left, aButtonIspRessed);
            InputState.Change(virtualMouse, mouseState);
            prevMouseState = aButtonIspRessed;
        }
        moveCursor(newpos);
    }

    void moveCursor(Vector2 newPos)
    {
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, newPos, cam, out anchoredPos);
        CursorTransform.anchoredPosition = anchoredPos;
    }
}
