using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerRestaurant player, PlayerFiniteStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();

        _movementCore?.SetVelocityX(_playerData._moveSpeed * _xInput); //set velocity to start moving
        _soundCoreComponent.InitAudio(_playerData._audioData._moveStateAudio);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(_xInput == 0) // if not input movement will enter idle state
        {
            _stateMachine.ChangeState(_player._idleState);
            return;
        }

        _movementCore?.CheckFlip(_xInput); //check if it should flip if player input certain value

        _movementCore?.SetVelocityX(_playerData._moveSpeed * _xInput); //make player move
    }
}
