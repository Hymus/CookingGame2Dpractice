using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRestaurant : MonoBehaviour //this must inherit from Player state if it has many script of player
{
    #region StateMachine

    private PlayerFiniteStateMachine stateMachine;

    public PlayerIdleState _idleState { get; private set; }
    public PlayerMoveState _moveState { get; private set; }
    public PlayerInAirState _inAirState { get; private set; }
    public PlayerLandState _landState { get; private set; }

    #endregion

    #region Component

    public static PlayerRestaurant Instance { get; private set; }
    public Core _core { get; private set; } //core that store thing that control this player such as movement checkCollision or take damage take status etc.

    public Animator _anim { get; private set; }
    public Rigidbody2D _rb { get; private set; }
    public Collider2D _contactCollider { get; private set; } // collider that check collision of player

    public PlayerInputHandler _inputHandler { get; private set; } //store input from player
    [field: SerializeField] public PlayerData _playerData { get; private set; }
    [field: SerializeField] public CarryFoodScript _carryFood { get; private set; } //script that control to carry food in back

    #endregion

    #region Unity CallBack

    private void Awake()
    {
        Instance = this;

        _core = GetComponentInChildren<Core>(); // core is children of player obj so get from child obj
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _contactCollider = GetComponent<Collider2D>();
        _inputHandler = GetComponent<PlayerInputHandler>();

        stateMachine = GetComponent<PlayerFiniteStateMachine>();

        _idleState = new PlayerIdleState(this, stateMachine, "Idle");
        _moveState = new PlayerMoveState(this, stateMachine, "Move");
        _inAirState = new PlayerInAirState(this, stateMachine, "InAir");
        _landState = new PlayerLandState(this, stateMachine, "Land");

        stateMachine.InitializedState(_idleState); //init first state to state machine
    }

    private void Update()
    {
        stateMachine._currentState.LogicUpdate(); //update logic in current state in normal Update
    }

    private void FixedUpdate()
    {
        stateMachine._currentState.PhysicsUpdate(); //update physics in FixedUpdate
    }
    #endregion

    #region Animation Trigger

    private void AnimationEnterTrigger() //check animation by finish trigger
    {
        stateMachine._currentState.AnimationEnterTrigger();
    }

    private void AnimationFinishTrigger()
    {
        stateMachine._currentState.AnimationEnterTrigger();
    }

    #endregion
}
