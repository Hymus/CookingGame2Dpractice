using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerRestaurant _player;
    protected PlayerFiniteStateMachine _stateMachine;
    protected PlayerData _playerData;
    protected Core _core;

    protected string _animBoolName; //name of anim to player when enter this state

    protected float _startTime; //store time when enter state

    protected SoundCoreComponent _soundCoreComponent;

    public PlayerState(PlayerRestaurant player, PlayerFiniteStateMachine stateMachine, string animBoolName)
    {
        _player = player;
        _stateMachine = stateMachine;
        _animBoolName = animBoolName;
        _core = _player._core;
        _playerData = _player._playerData; //core and data can fetch from player script

        _soundCoreComponent = _core.GetCoreComponent<SoundCoreComponent>();
    }

    public virtual void Enter() 
    {
        //Debug.Log("player enter " + this);
        _player._anim.SetBool(_animBoolName, true); //play animation of this state
        _startTime = Time.time; //store time when enter state
        DoCheck(); //check in enter
    }

    public virtual void Exit() 
    {
        _player._anim.SetBool(_animBoolName, false); //exit state and unplay animation
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate()
    {
        DoCheck(); //check in after update
    }

    public virtual void DoCheck() { }
    public virtual void AnimationEnterTrigger() { }
    public virtual void AnimationFinishTriger() { }
}
