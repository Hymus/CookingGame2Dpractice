using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputHandler : MonoBehaviour
{
    public event Action OnPressedInput; //store all open/close method from window script to check when has input

    public bool _pressESC { get; private set; } //store esc input
    public bool _pressI { get; private set; } //store I input

    public List<string> _currentOpenWindows = new List<string>(); //store all window that open
    public string _currentSelectWindow { get; private set; }

    DynamicUIwindowScript selectedWindowScript; //store selected window script only dynamic window can be select
    public bool _canOpenESC { get; private set; } //mark if it can open esc, use to check in esc window script

    private void Awake()
    {
        _canOpenESC = true; //it can open esc at first
    }

    public void SetSelectWindow(string value, DynamicUIwindowScript windowScript)
    {
        _currentSelectWindow = value;
        if(this.selectedWindowScript) selectedWindowScript.DeselectThisWindow(); //if this window script is not null deselect this window first
        this.selectedWindowScript = windowScript; //set new select window script
    }
    public void DeSelectOldWindow() //clear selected window data that store in here
    {
        _currentSelectWindow = null; //clear name of window to null cuz we will check if it null in esc window script
        this.selectedWindowScript = null; //clear window script of dynamic window
    }
    public void CloseWindowCoyoteTime() //delay time before can press esc open esc window
    {
        _canOpenESC = false;
        Invoke(nameof(ResetCanOpenESC), 0.1f);
    }

    void ResetCanOpenESC() => _canOpenESC = true;

    public void OnEscInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _pressESC = true;
            OnPressedInput?.Invoke();
        }

        if(context.canceled) _pressESC = false;
    }

    public void OnInventoryInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _pressI = true;
            OnPressedInput?.Invoke();
        }

        if (context.canceled) _pressI = false;
    }
}
