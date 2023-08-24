using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFindTableState : EntityRestaurantState
{
    protected EntityMoveStateData _stateData;

    protected bool _foundTable; //check if found table

    public EntityFindTableState(Entity entity, EntityFiniteStateMachine stateMachine, string animBoolName, EntityMoveStateData stateData) : base(entity, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _soundCoreComponent.InitAudio(_stateData._stateAudio);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _moveMentCore.SetVelocityX(_stateData._moveSpeed * _moveMentCore._facingDirection); //make it move to find table
        _foundTable = _restaurantCore._foundTable; //check if restaurant core has found table 
    }
}
