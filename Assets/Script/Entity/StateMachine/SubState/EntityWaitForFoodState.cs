using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityWaitForFoodState : EntityRestaurantState
{
    protected EntityWaitForFoodStateData _stateData;
    protected FoodData _orderedFoodData; //food that order

    protected bool _isFoodOnTable; //check if food ontable or not
    protected float _canWaitTime; //store time that will wait

    protected FoodTableScript _foodTableScript; //ref food table that have sit

    public EntityWaitForFoodState(Entity entity, EntityFiniteStateMachine stateMachine, string animBoolName, EntityWaitForFoodStateData stateData) : base(entity, stateMachine, animBoolName)
    {
        _stateMachine = stateMachine;
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _soundCoreComponent.InitAudio(_stateData._stateAudio); //play sound from entity of this state

        _moveMentCore.SetVelocityX(0f); //stop moving

        _orderedFoodData = _stateData._likedFood[Random.Range(0, _stateData._likedFood.Length)]; //randomfood from liked food data

        _foodTableScript = _restaurantCore._foodTable; //store food that restaurant core fetch
        _foodTableScript.TakeSeat(); //npc take seat of this table call when npc really sit there in this state
        _foodTableScript.OrderMenu(_orderedFoodData); //notify bubble ontable and store food data that npc has ordered

        _canWaitTime = Random.Range(_stateData._minMaxCanWaitTime.x, _stateData._minMaxCanWaitTime.y); //random time that this npc will wait
        _foodTableScript.InitWaitingtTime(_canWaitTime); //set up bar and time to show that waiting and what time left

        _isFoodOnTable = _foodTableScript._hasFood; //check when enter too
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _isFoodOnTable = _foodTableScript._hasFood; //check food on table
    }
}
