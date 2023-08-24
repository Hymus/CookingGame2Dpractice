using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EntityData/StateData/EatFoodState")]
public class EntityEatFoodStateData : EntityData
{
    [field: SerializeField] public Vector2 _minMaxEatingTime;
}
