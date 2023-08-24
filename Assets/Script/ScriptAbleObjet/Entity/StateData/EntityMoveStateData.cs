using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EntityData/StateData/MoveState")]
public class EntityMoveStateData : EntityData
{
    [field: SerializeField] public float _moveSpeed { get; private set; }
    [field: SerializeField] public float _jumpForce { get; private set; }
}
