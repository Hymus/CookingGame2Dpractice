using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : ScriptableObject
{
    [field: SerializeField] public AudioClip _stateAudio { get; private set; }
}
