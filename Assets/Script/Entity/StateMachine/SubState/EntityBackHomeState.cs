using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBackHomeState : EntityRestaurantState
{
    protected EntityBackHomeStateData _stateData;

    public EntityBackHomeState(Entity entity, EntityFiniteStateMachine stateMachine, string animBoolName, EntityBackHomeStateData stateData) : base(entity, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _soundCoreComponent.InitAudio(_stateData._stateAudio);

        if (_restaurantCore._doorOut.position.x - _entity.transform.position.x < 0 && _moveMentCore._facingDirection == 1) //check if door out is on the left and is npc facing right or not
        {
            _moveMentCore.Flip(); //if it facing right but door out is on left will flip turn around
        }

        _moveMentCore.SetVelocityX(_stateData._moveSpeed * _moveMentCore._facingDirection); //make it move
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(_isDetectedWall) //if detect wall will stop and turn back immediately
        {
            _moveMentCore.SetVelocityX(0f);
            _moveMentCore.Flip();
            return;
        }

        _moveMentCore.SetVelocityX(_stateData._moveSpeed * _moveMentCore._facingDirection); //make it move
    }
}
