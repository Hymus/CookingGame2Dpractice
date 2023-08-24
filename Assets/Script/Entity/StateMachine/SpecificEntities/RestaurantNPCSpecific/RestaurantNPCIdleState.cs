using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantNPCIdleState : EntityRestaurantIdleState
{
    RestaurantNPC npc;

    public RestaurantNPCIdleState(Entity entity, EntityFiniteStateMachine stateMachine, string animBoolName, EntityIdleStateData stateData, RestaurantNPC npc) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.npc = npc;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time > (_startTime + _idleTime)) //check time that will stay in idle state
        {
            _stateMachine.ChangeState(npc._findTableState); 
            return;
        }
    }
}
