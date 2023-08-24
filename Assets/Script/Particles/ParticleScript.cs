using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    [SerializeField] protected bool _setDestroyInStart; //set to destroy in Start
    [SerializeField] protected float _setDestroyTime = 1; //set time that will destroy after awake

    protected virtual void Start()
    {
        if(_setDestroyInStart) Destroy(gameObject, _setDestroyTime);
    }

    protected virtual void AnimationFinishTrigger() { } //event call in animation
}
