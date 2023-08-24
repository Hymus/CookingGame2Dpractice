using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantNPCEatFoodState : EntityEatFoodState
{
    RestaurantNPC npc;
    public RestaurantNPCEatFoodState(Entity entity, EntityFiniteStateMachine stateMachine, string animBoolName, EntityEatFoodStateData stateData, RestaurantNPC npc) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.npc = npc;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(_isEated)
        {
            int starScore = 5; //set score first at 5
            switch(_foodData._foodQuality) ///check food quality
            {
                case FoodQuality.Good: //if good will give 3-5 star
                    starScore = Random.Range(3, 6);
                    break;
                case FoodQuality.Bad: //if bad will give 2-4 star
                    starScore = Random.Range(2, 5);
                    break;
            }
            _restaurantCore.GivingStarScore(starScore); //instantiate star score 
            _restaurantScript._restaurantData._restaurantStars.Add(starScore); //add star to value and Update overall star score
            _restaurantScript.UpdateAveragerStarScore(); //after add star will update avarage star to UI and data through this method

            _stateMachine.ChangeState(npc._backHomeState);
            return;
        }
    }
}
