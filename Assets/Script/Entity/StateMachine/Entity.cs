using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected EntityFiniteStateMachine _stateMachine;

    public Core _core { get; private set; }

    public Animator _anim { get; private set; }
    public Rigidbody2D _rb { get; private set; }
    [field: SerializeField] public EntityData _entityData { get; private set; }

    protected virtual void Awake()
    {
        _core = GetComponentInChildren<Core>(); //core is in children obj
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();

        _stateMachine = GetComponent<EntityFiniteStateMachine>(); //state machine it attach on same obj
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        _stateMachine._currentState.LogicUpdate(); //update logic in state
    }

    protected virtual void FixedUpdate()
    {
        _stateMachine._currentState.PhysicsUpdate(); //update physics in state
    }
}
