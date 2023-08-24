using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFiniteStateMachine : MonoBehaviour
{
    public EntityState _currentState { get; private set; } //ref state in current

    public void InitializedState(EntityState state) //set up start state
    {
        _currentState = state;
        _currentState.Enter();
    }

    public void ChangeState(EntityState state) //change state
    {
        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }
}
