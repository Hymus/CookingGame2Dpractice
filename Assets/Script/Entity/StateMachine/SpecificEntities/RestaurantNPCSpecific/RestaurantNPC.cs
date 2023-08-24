using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantNPC : Entity
{
    public RestaurantNPCIdleState _idleState { get; private set; } //states
    public RestaurantNPCFindTableState _findTableState { get; private set; }
    public RestaurantNPCWaitForFoodState _waitForFoodState { get; private set; }
    public RestaurantNPCEatFoodState _eatFoodState { get; private set; }
    public RestaurantNPCBackHomeState _backHomeState { get; private set; }

    [SerializeField] EntityIdleStateData idleStateData; //statesData
    [SerializeField] EntityMoveStateData findTableStateData;
    [SerializeField] EntityWaitForFoodStateData waitForFoodStateData;
    [SerializeField] EntityEatFoodStateData eatFoodStateData;
    [SerializeField] EntityBackHomeStateData backHomeStateData;

    protected override void Awake()
    {
        base.Awake();

        _idleState = new RestaurantNPCIdleState(this, _stateMachine, "Idle", idleStateData, this); //initialize each state in awake
        _findTableState = new RestaurantNPCFindTableState(this, _stateMachine, "Move", findTableStateData, this);
        _waitForFoodState = new RestaurantNPCWaitForFoodState(this, _stateMachine, "Sit", waitForFoodStateData, this);
        _eatFoodState = new RestaurantNPCEatFoodState(this, _stateMachine, "Eat", eatFoodStateData, this);
        _backHomeState = new RestaurantNPCBackHomeState(this, _stateMachine, "Move", backHomeStateData, this);

        _stateMachine.InitializedState(_idleState); //initialize stateMachine first state
    }
}
