using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 _rawMovementInput { get; private set; } //store raw input from player in x and y

    public int _xInput { get; private set; } //store normalize x input
    public int _yInput { get; private set; } //store normalize y input

    public bool _jumpInput { get; private set; } //store jump input 

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        _rawMovementInput = context.ReadValue<Vector2>(); //recieve input from player

        _xInput =  Mathf.Clamp(Mathf.RoundToInt(_rawMovementInput.x), -1, 1); //normalize it in int and has only -1 to 1 in both x and y
        _yInput = Mathf.Clamp(Mathf.RoundToInt(_rawMovementInput.y), -1, 1); //this way will better to use
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.started) //if press jump mark it true
        {
            _jumpInput = true;
        }

        if(context.canceled) //if release mark it false
        {
            _jumpInput = false;
        }
    }
}
