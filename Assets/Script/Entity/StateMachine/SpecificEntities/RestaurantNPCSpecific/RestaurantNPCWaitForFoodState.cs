using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantNPCWaitForFoodState : EntityWaitForFoodState
{
    RestaurantNPC npc;
    public RestaurantNPCWaitForFoodState(Entity entity, EntityFiniteStateMachine stateMachine, string animBoolName, EntityWaitForFoodStateData stateData, RestaurantNPC npc) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.npc = npc;
    }

    public override void Enter()
    {
        base.Enter();
        
        if (_isFoodOnTable) //check if there is food on table or not
        {
            if (_foodTableScript.CheckOrder) //if it is food that this customer want will eat
            {
                _stateMachine.ChangeState(npc._eatFoodState);
            }
            else //but if not will put away food and eat
            {
                _foodTableScript.PutAwayFood(); //put away food
            }
            return;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time > (_startTime + _canWaitTime))
        {
            GivingOneStarScore(); //Giving 1 cuz wait too long
            _stateMachine.ChangeState(npc._backHomeState); //backHome
            return;
        }

        if(_isFoodOnTable)
        {
            if (_foodTableScript.CheckOrder)
            {
                npc._eatFoodState.SetFoodData(_foodTableScript._foodDataOntable); //give food data ontable to eatState
                _stateMachine.ChangeState(npc._eatFoodState); //if eat food will add star in Table state cuz food quality store in food data and foodData is in table state
            }
            else
            {
                GivingOneStarScore(); //giving 1 cuz order is not what customer want or rotten
                _stateMachine.ChangeState(npc._backHomeState);
            }
            return;
        }
    }

    void GivingOneStarScore() //giving one star when customer is not eat food\ or cant wait
    {
        int starScore = 1;
        _restaurantCore.GivingStarScore(starScore); //instantiate star score 
        _restaurantScript._restaurantData._restaurantStars.Add(starScore); //giving star review 1 cuz customer dicide to not eat this food may rotten or not the food that order
        _restaurantScript.UpdateAveragerStarScore();

    }
}
