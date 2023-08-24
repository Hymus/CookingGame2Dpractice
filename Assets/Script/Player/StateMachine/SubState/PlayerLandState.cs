using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    bool isFinishLand; //check it it done landing

    public PlayerLandState(PlayerRestaurant player, PlayerFiniteStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void AnimationFinishTriger() //when animation end will call this event and set it finishLand
    {
        base.AnimationFinishTriger();
        isFinishLand = true;
    }

    public override void Enter() 
    {
        base.Enter();
        _soundCoreComponent.InitAudio(_playerData._audioData._idleStateAudio);

        if (_xInput != 0) //if player still input x axis will enter moveState immediatly
        {
            _stateMachine.ChangeState(_player._moveState);
            return;
        }
        isFinishLand = false; //first set it not finish land
        _movementCore.SetVelocityX(0f); //if land and not input move will not move at first
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_xInput != 0) //if player input x axis will move immediatly
        {
            _stateMachine.ChangeState(_player._moveState);
            return;
        }

        if (isFinishLand) //if finish play land animation will enter idleState
        {
            _stateMachine.ChangeState(_player._idleState);
            return;
        }
    }
}
