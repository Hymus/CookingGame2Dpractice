using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EntityData/StateData/WaitForFoodState")]
public class EntityWaitForFoodStateData : EntityData
{
    [field: SerializeField] public FoodData[] _likedFood { get; private set; } //STORE food that this npc liked this will random in enter wait for food state
    [field: SerializeField] public Vector2 _minMaxCanWaitTime { get; private set; } //store wait time that this npc can wait for food
}
