using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRestaurantIdleState : EntityRestaurantState
{
    protected EntityIdleStateData _stateData;

    protected bool _flipAfterIdle; //check if should flip after idle like if front is wall will flip before enter move state
    protected float _idleTime; //time that will stay in idle state
    public EntityRestaurantIdleState(Entity entity, EntityFiniteStateMachine stateMachine, string animBoolName, EntityIdleStateData stateData) : base(entity, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        _soundCoreComponent.InitAudio(_stateData._stateAudio); //play sound from entity of this state
        _moveMentCore.SetVelocityX(0f); //make it stop in Idle
        RandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if(_flipAfterIdle) //if it has set to flip after idle will flip such as when face wall 
        {
            _moveMentCore.Flip();
        }
    }

    public void SetFlipIdle(bool value) //set when move state when detect wall
    {
        _flipAfterIdle = value;
    }

    public void RandomIdleTime() //random time that will stay in Idle
    {
        _idleTime = Random.Range(_stateData._minMaxIdleTime.x, _stateData._minMaxIdleTime.y);
    }

}
