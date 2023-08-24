using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenseCoreComponent : CoreComponent 
{
    [SerializeField] Transform groundCheckPos;
    [SerializeField] Transform wallCheckPos;
    [SerializeField] Transform stairCheckPos;
    [SerializeField] Transform ledgeCheckPos;

    [SerializeField] LayerMask whatIsGround;
    [SerializeField] LayerMask whatIsStair;

    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] float wallCheckRange = 0.5f;
    [SerializeField] float stairCheckRange = 0.5f;
    [SerializeField] float ledgeCheckRange = 1f;

    public bool _isGrounded { 
        get => Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround); 
        private set => _isGrounded = value; }

    public bool _frontWallDetected
    {
        get => Physics2D.Raycast(wallCheckPos.position, wallCheckPos.right, wallCheckRange, whatIsGround);
        private set => _frontWallDetected = value;
    }

    public bool _stairDetected
    {
        get => Physics2D.Raycast(stairCheckPos.position, stairCheckPos.right, stairCheckRange, whatIsStair);
        private set => _stairDetected = value;
    }

    public bool _ledgeDetected
    {
        get => Physics2D.Raycast(ledgeCheckPos.position, -ledgeCheckPos.up, ledgeCheckRange, whatIsGround);
        private set => _ledgeDetected = value;
    }
}
