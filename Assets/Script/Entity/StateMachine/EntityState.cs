using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityState
{
    protected Entity _entity;
    protected EntityFiniteStateMachine _stateMachine;
    protected Core _core;

    protected SoundCoreComponent _soundCoreComponent;

    protected string _animBoolName; //name of bool anim to play in this state
    protected float _startTime; //time  that enter state create for use in some state that need to count to stay time in state
    public EntityState(Entity entity, EntityFiniteStateMachine stateMachine, string animBoolName) //set require variable when make new state
    {
        _entity = entity;
        _stateMachine = stateMachine;
        _animBoolName = animBoolName;
        _core = _entity._core;

        _soundCoreComponent = _core.GetCoreComponent<SoundCoreComponent>();
    }

    public virtual void Enter()
    {
        Debug.Log(_entity.transform.name + " enter " + this); 
        _entity._anim.SetBool(_animBoolName, true); //play animation of this state
        _startTime = Time.time; //store time when enter state
        DoCheck();
    }

    public virtual void Exit()
    {
        _entity._anim.SetBool(_animBoolName, false); //when exit will stop play animation of this anim
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate()
    {
        DoCheck();
    }

    public virtual void DoCheck() { } //this will call in enter and physics to check
}
