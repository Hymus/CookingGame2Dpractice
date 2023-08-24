using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCoreComponent : CoreComponent
{
    [SerializeField] Transform[] uiOBJtoFlip; //ui that will flip when this parent object flip
    public Rigidbody2D _rb { get; private set; }

    Vector2 workSpace; //set up velocity here before set to real rigidbody;

    public int _facingDirection { get; private set; } //direction that this obj facing

    protected override void Awake()
    {
        base.Awake();

        _facingDirection = 1; //set it facing right at first

        _rb = _core.transform.parent.GetComponent<Rigidbody2D>(); //fetch rigidbody from core parent
    }
    public void SetVelocityX(float velocity) //set velocity to x axis
    {
        if (_rb == null) //if there is no rigid will return
        {
            return;
        }

        workSpace.Set(velocity, _rb.velocity.y); //set it in workspace first
        SetFinalVelocity(); //call method to set workspace to rigidbody
    }

    public void SetVelocityY(float velocity) //set velocity in y axis use in jump or climbling
    {
        if (_rb == null) //if dont have velocity will return
        {
            return;
        }

        workSpace.Set(_rb.velocity.x, velocity); //set velocity to workspace first
        SetFinalVelocity();//call method to set workspace to rigidbody
    }

    private void SetFinalVelocity() //this method set workspace to velocity
    {
        _rb.velocity = workSpace;
    }

    public void CheckFlip(int xInput) //check if shoud flip or not use in state that alway move in x axis
    {
        if(xInput != 0 && xInput != _facingDirection) //if input x axis and that input not same as current facingdirection will flip
        {
            Flip();
        }
    }

    public void Flip()
    {
        _facingDirection *= -1; //it will get opposite value of current value
        _rb.transform.Rotate(0, 180, 0); //rb is from player obj so rotate from this

        foreach(Transform uiObj in uiOBJtoFlip)
        {
            uiObj.Rotate(0, 180, 0);
        }
    }
}
