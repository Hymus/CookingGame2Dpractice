using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EntityData/StateData/IdleState")]
public class EntityIdleStateData : EntityData
{
    [field: SerializeField] public Vector2 _minMaxIdleTime = new Vector2(1, 3);
}
