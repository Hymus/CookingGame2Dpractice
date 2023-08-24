using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantNPCFindTableState : EntityFindTableState
{
    RestaurantNPC npc;
    RestaurantCoreComponent restaurantCore;

    public RestaurantNPCFindTableState(Entity entity, EntityFiniteStateMachine stateMachine, string animBoolName, EntityMoveStateData stateData, RestaurantNPC npc) : base(entity, stateMachine, animBoolName, stateData)
    {
        restaurantCore = _core.GetCoreComponent<RestaurantCoreComponent>();
        this.npc = npc;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(_isDetectedWall) //if detect wall will stand still enter idle wait couple minute and turn back and walk and restaurant no have ledge so dont need to check ledge
        {
            npc._idleState.SetFlipIdle(true); //set it to true to tell when finish idle must turn back cuz front is wall
            _stateMachine.ChangeState(npc._idleState); 
            return;
        }

        if(_isDetectedStair) //if detectedstair jump up
        {
            _moveMentCore.SetVelocityY(_stateData._jumpForce);
        }

        if(_foundTable) //if found table will enter wait for food state
        {
            _stateMachine.ChangeState(npc._waitForFoodState);
            return;
        }
    }
}
