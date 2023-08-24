using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRestaurantState : EntityState
{
    protected MovementCoreComponent _moveMentCore; //ref core component that will use in this sub state
    protected CollisionSenseCoreComponent _collisionSenseCore;
    protected RestaurantCoreComponent _restaurantCore;

    protected RestaurantScript _restaurantScript; //ref script that control this restaurant in this scene use when giving star review

    protected bool _isDetectedWall; //check wall and stair
    protected bool _isDetectedStair;

    public EntityRestaurantState(Entity entity, EntityFiniteStateMachine stateMachine, string animBoolName) : base(entity, stateMachine, animBoolName)
    {
        _moveMentCore = _core.GetCoreComponent<MovementCoreComponent>(); //get core component from core of this entity
        _collisionSenseCore = _core.GetCoreComponent<CollisionSenseCoreComponent>();
        _restaurantCore = _core.GetCoreComponent<RestaurantCoreComponent>();

        _restaurantScript = RestaurantScript.instance;
    }

    public override void DoCheck()
    {
        base.DoCheck();

        _isDetectedWall = _collisionSenseCore._frontWallDetected; //check wall and stair
        _isDetectedStair = _collisionSenseCore._stairDetected;
    }
}
