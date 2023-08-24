using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    protected Core _core; //ref core

    protected virtual void Awake()
    {
        _core = transform.parent.GetComponent<Core>();

        _core.AddCoreComponent(this); //add component to core at first
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }
}
