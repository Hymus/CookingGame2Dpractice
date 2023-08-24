using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantNPCBackHomeState : EntityBackHomeState
{
    RestaurantNPC npc;

    public RestaurantNPCBackHomeState(Entity entity, EntityFiniteStateMachine stateMachine, string animBoolName, EntityBackHomeStateData stateData, RestaurantNPC npc) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.npc = npc;
    }

    public override void Enter()
    {
        base.Enter();

        _restaurantCore._foodTable.LeaveSeat();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (_isDetectedStair) //if found stair will jump up
        {
            _moveMentCore.SetVelocityY(_stateData._jumpForce);
        }
    }
}
