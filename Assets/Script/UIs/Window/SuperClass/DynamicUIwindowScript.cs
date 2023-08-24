using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicUIwindowScript : UIwindowScript, IDragHandler, IPointerClickHandler //moveable ui window
{
    [SerializeField] private GameObject _selectIndicate; //obj that will show if player select this window
    [SerializeField] protected bool _canDragWindow; //mark in inspector to determine can drag or not such as esc window cant, inventory window can

    [SerializeField] protected Transform _selectedWindowParent; //ref the parent of window if be child of selected will show on top of other unselect window
    [SerializeField] protected Transform _unSelectWindowParent;

    protected bool _selectedWindow; //mark that this window is selected or not

    #region Close Open window
    public override void CloseAndOpenWindow() //public cuz may use in button
    {
        if (_window.activeSelf) //this is close method so check if window active or not if active will close
        {
            _window.SetActive(false); //disable window obj
            if (_inputHandler._currentOpenWindows.Contains(_window.name)) _inputHandler._currentOpenWindows.Remove(_window.name);
            _inputHandler.CloseWindowCoyoteTime(); //delay time before can open esc window
            DeselectThisWindow(); //close window and call method to deselect it
            return;
        }

        _window.SetActive(true);
        _inputHandler._currentOpenWindows.Add(_window.name);
    }
    public override void CloseWindow()
    {
        if (_window.activeSelf)
        {
            _window.SetActive(false);
            if (_inputHandler._currentOpenWindows.Contains(_window.name)) _inputHandler._currentOpenWindows.Remove(_window.name);
            _inputHandler.CloseWindowCoyoteTime(); //delay time before can open esc window
            DeselectThisWindow();
        }
    }

    public override void CloseAndOpenWindowStopTime() //public cuz may use in button
    {
        if (_window.activeSelf)
        {
            Time.timeScale = 1f;
            _window.SetActive(false);
            if (_inputHandler._currentOpenWindows.Contains(_window.name)) _inputHandler._currentOpenWindows.Remove(_window.name);
            _inputHandler.CloseWindowCoyoteTime(); //delay time before can open esc window
            DeselectThisWindow();
            return;
        }

        Time.timeScale = 0f;
        _window.SetActive(true);
        _inputHandler._currentOpenWindows.Add(_window.name);
    }

    public override void CloseWindowStopTime()
    {
        if (_window.activeSelf)
        {
            Time.timeScale = 1f;
            _window.SetActive(false);
            if (_inputHandler._currentOpenWindows.Contains(_window.name)) _inputHandler._currentOpenWindows.Remove(_window.name);
            _inputHandler.CloseWindowCoyoteTime(); //delay time before can open esc window
            DeselectThisWindow();
        }
    }

    #endregion

    public void OnDrag(PointerEventData eventData)
    {
        if (!_canDragWindow) return;

        _window.transform.position = eventData.position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_selectedWindow)
        {
            _inputHandler.SetSelectWindow(_window.name, this);
            _selectedWindow = true;
            _selectIndicate.SetActive(true);
            this.transform.SetParent(_selectedWindowParent); //set it to be child of selected group so it on top of other window
            return;
        }
        DeselectThisWindow();
    }

    public void DeselectThisWindow() //call in inputhandler when close window or deselect it
    {
        _inputHandler.DeSelectOldWindow(); //this method will clear old selected window data in inputhandler
        _selectedWindow = false; //mark that window is not select now
        _selectIndicate.SetActive(false); //disable indicate of selected this window
        this.transform.SetParent(_unSelectWindowParent); //set it back to unselect group so it not show on top of selected window
    }
}
