using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EntityData/StateData/BackHomeState")]
public class EntityBackHomeStateData : EntityData
{
    [field: SerializeField] public float _moveSpeed { get; private set; }
    [field: SerializeField] public float _jumpForce { get; private set; }
}
