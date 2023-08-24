using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIwindowScript : MonoBehaviour
{
    [SerializeField] protected GameObject _window; //ref window 
    [SerializeField] protected UIInputHandler _inputHandler; //ref input from player

    protected bool _escInput;
    protected bool _iInput;

    protected virtual void Awake()
    {
        _inputHandler.OnPressedInput += UpdateInput;
    }

    protected virtual void UpdateInput()
    {
        _escInput = _inputHandler._pressESC;
        _iInput = _inputHandler._pressI;
    }

    #region Close Open window
    public virtual void CloseAndOpenWindow() //public cuz may use in button
    {
        if(_window.activeSelf)
        {
            _window.SetActive(false);
            if (_inputHandler._currentOpenWindows.Contains(_window.name)) _inputHandler._currentOpenWindows.Remove(_window.name);
            _inputHandler.CloseWindowCoyoteTime(); //delay time before can open esc window
            return;
        }

        _window.SetActive(true);
        _inputHandler._currentOpenWindows.Add(_window.name);
    }
    public virtual void CloseWindow()
    {
        if (_window.activeSelf)
        {
            _window.SetActive(false);
            if (_inputHandler._currentOpenWindows.Contains(_window.name)) _inputHandler._currentOpenWindows.Remove(_window.name);
            _inputHandler.CloseWindowCoyoteTime(); //delay time before can open esc window
        }
    }

    public virtual void CloseAndOpenWindowStopTime() //public cuz may use in button
    {
        if (_window.activeSelf)
        {
            Time.timeScale = 1f;
            _window.SetActive(false);
            if (_inputHandler._currentOpenWindows.Contains(_window.name)) _inputHandler._currentOpenWindows.Remove(_window.name);
            _inputHandler.CloseWindowCoyoteTime(); //delay time before can open esc window
            return;
        }

        Time.timeScale = 0f;
        _window.SetActive(true);
        _inputHandler._currentOpenWindows.Add(_window.name);
    }

    public virtual void CloseWindowStopTime()
    {
        if (_window.activeSelf)
        {
            Time.timeScale = 1f;
            _window.SetActive(false);
            if(_inputHandler._currentOpenWindows.Contains(_window.name)) _inputHandler._currentOpenWindows.Remove(_window.name);
            _inputHandler.CloseWindowCoyoteTime(); //delay time before can open esc window
        }
    }


    #endregion
}
