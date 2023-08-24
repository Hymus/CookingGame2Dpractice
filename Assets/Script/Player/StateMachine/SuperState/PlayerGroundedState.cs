using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int _xInput; //store x input from player
    protected int _yInput; //store y inpuut from player

    protected bool _isGrounded; //check if it touch ground
    protected bool _jumpInput; //store jump input from player

    protected MovementCoreComponent _movementCore; //store core component that use in substate of this super state
    protected CollisionSenseCoreComponent _collisionSenseCore;
    protected JumpDownCoreComponent _jumpdownCore;

    public PlayerGroundedState(PlayerRestaurant player, PlayerFiniteStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        _movementCore = _core.GetCoreComponent<MovementCoreComponent>(); //ref core component from core
        _collisionSenseCore = _core.GetCoreComponent<CollisionSenseCoreComponent>();
        _jumpdownCore = _core.GetCoreComponent<JumpDownCoreComponent>();
    }

    public override void DoCheck()
    {
        base.DoCheck();

        _isGrounded = _collisionSenseCore._isGrounded; //always check in this state
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
         
        _xInput = _player._inputHandler._xInput; //receive input from player here
        _yInput = _player._inputHandler._yInput;
        _jumpInput = _player._inputHandler._jumpInput;

        if (!_isGrounded) //if not is ground will enter in air state and ignore below
        {
            _stateMachine.ChangeState(_player._inAirState);
            return;
        }

        if(_yInput == -1 && _jumpInput) //if Hold S will get -1 in _yInput and when press space will jumpdown
        {
            Debug.Log("Jumpdown press");
            if(_jumpdownCore._canJumpDown)
            {
                _jumpdownCore.EnableTriggerCollider();
            }
            return;
        }

        if(_jumpInput) //if get jump input will set velocity in y axis to it
        {
            _movementCore.SetVelocityY(_playerData._jumpForce);
            return;
        }
        
    }
}
