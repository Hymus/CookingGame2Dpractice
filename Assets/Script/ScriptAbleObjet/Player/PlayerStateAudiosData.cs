using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData/StateAudiosData")]
public class PlayerStateAudiosData : ScriptableObject
{
    [field: SerializeField] public AudioClip _idleStateAudio { get; private set; }
    [field: SerializeField] public AudioClip _moveStateAudio { get; private set; }
    [field: SerializeField] public AudioClip _inAirStateAudio { get; private set; }
    [field: SerializeField] public AudioClip _landStateAudio { get; private set; }
}
