using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData/GeneralData")]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public float _moveSpeed { get; private set; }
    [field: SerializeField] public float _jumpForce { get; private set; }
    [field: SerializeField] public PlayerStateAudiosData _audioData { get; private set; }
}
