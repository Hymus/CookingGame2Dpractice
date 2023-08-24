using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEatFoodState : EntityRestaurantState
{
    protected EntityEatFoodStateData _stateData;

    protected float _eatingTime; //time that will eat
    protected bool _isEated; //check if done eating

    protected FoodData _foodData; //store food that current eat

    public EntityEatFoodState(Entity entity, EntityFiniteStateMachine stateMachine, string animBoolName, EntityEatFoodStateData stateData) : base(entity, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _soundCoreComponent.InitAudio(_stateData._stateAudio);

        _eatingTime = Random.Range(_stateData._minMaxEatingTime.x, _stateData._minMaxEatingTime.y); //random eat time from state data

        _restaurantCore._foodTable._foodDataOntable.UpdateFoodQuality(); //Update food quality before eat food so will check that if food is good or bad when pay money
        _restaurantCore.InitEatingFood(_eatingTime); //init eating in restaurant core

        _restaurantCore._foodTable.FinishWaitForFood(); //set to mark that finish wait now will disable bubbleOrder and waitingBar
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _isEated = _restaurantCore._isEated; //check if it done eat or not
    }

    public void SetFoodData(FoodData foodData) //set food data when start eat
    {
        this._foodData = foodData;
    }
}
