using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    protected int _xInput; //recieve to move in air

    protected bool _isGrounded; //check ground if touch ground will change state to Landing state

    protected MovementCoreComponent _movementCore; //ref core that will use in InAir state
    protected CollisionSenseCoreComponent _collisionSenseCore;

    public PlayerInAirState(PlayerRestaurant player, PlayerFiniteStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        _movementCore = _core.GetCoreComponent<MovementCoreComponent>();
        _collisionSenseCore = _core.GetCoreComponent<CollisionSenseCoreComponent>();
    }

    public override void DoCheck()
    {
        base.DoCheck();

        _isGrounded = _collisionSenseCore._isGrounded; //check ground all time
    }

    public override void Enter()
    {
        base.Enter();

        _soundCoreComponent.InitAudio(_playerData._audioData._inAirStateAudio);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (_isGrounded) //if is touch ground will enter in LandState
        {
            _stateMachine.ChangeState(_player._landState);
            return;
        }

        _xInput = _player._inputHandler._xInput; //get xinput from player

        _movementCore.SetVelocityX(_xInput * _playerData._moveSpeed); //make it move by input of player
        _movementCore.CheckFlip(_xInput); //check if this input of player nned to flip 

        _player._anim.SetFloat("xVelocity", Mathf.Abs(_xInput)); //set animation in blend tree by input from player in X axis
        _player._anim.SetFloat("yVelocity", Mathf.Clamp(_player._rb.velocity.y, -1, 1)); //set animation y axis by y velocity of rigidbody
    }
}
