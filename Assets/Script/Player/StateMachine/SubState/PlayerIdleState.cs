using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerRestaurant player, PlayerFiniteStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _movementCore?.SetVelocityX(0f); //not move in idle state
        _soundCoreComponent.InitAudio(_playerData._audioData._idleStateAudio);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(_xInput != 0) //if recieve input in x axis will enter move state
        {
            _stateMachine.ChangeState(_player._moveState);
            return;
        }
    }
}
