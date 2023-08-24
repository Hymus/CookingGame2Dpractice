using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideWindowScript : DynamicUIwindowScript
{
    protected override void Awake()
    {
        base.Awake();

        _inputHandler.OnPressedInput += CloseAndOpenWindow; //subscript close and open fuction to event that will call when any input recieve
        _inputHandler._currentOpenWindows.Add(_window.name); //cuz guide is open at first so add it to list in here awake
    }

    public override void CloseAndOpenWindow()
    {
        if (_escInput && _window.activeSelf && _inputHandler._currentSelectWindow == _window.name) //if it active and press esc will check if this window is selected or not 
        {
            base.CloseAndOpenWindow(); //if selected will close this guide window
        }
    }
}
