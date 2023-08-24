using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiniteStateMachine : MonoBehaviour
{
    public PlayerState _currentState { get; private set; } 

    public void InitializedState(PlayerState state) //initialize state for the first time
    {
        _currentState = state;
        _currentState.Enter();
    }

    public void ChangeState(PlayerState state) //change state here
    {
        _currentState.Exit(); //exit old state first before change to next state
        _currentState = state; //set state to state that recieve from parameter
        _currentState.Enter(); //when change state will enter now
    }
}
