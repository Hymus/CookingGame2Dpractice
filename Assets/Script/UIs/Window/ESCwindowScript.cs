using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCwindowScript : StaticUIwindowScript
{
    protected override void Awake()
    {
        base.Awake();
         
        _inputHandler.OnPressedInput += CloseAndOpenWindowStopTime; //subscript close open control function to event that will call when any input recieve
    }

    public override void CloseAndOpenWindowStopTime()
    {
        if (!_escInput) return; //if not input _esc will return;

        if (!_inputHandler._canOpenESC) return; //if just close other window will return if it get call will call same time and will open esc window when close other window

        if(_inputHandler._currentOpenWindows.Count > 0) //if it has window open will check that there is esc window or not
        {
            if (!_inputHandler._currentOpenWindows.Contains(_window.name) && _inputHandler._currentSelectWindow != null) return; //if there is window open but not esc will not do below must close all window first to eble to open esc window
        }

        base.CloseAndOpenWindowStopTime();
    }
}
