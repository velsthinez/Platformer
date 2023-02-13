using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Acceleration = 10f;
    public float JumpForce = 50f;
    public LayerMask GroundLayerMask;

    public PhysicsMaterial2D Default;
    public PhysicsMaterial2D FullFriction;
    
    private float m_MovementSmoothing = .05f;	
    private Vector3 m_Velocity = Vector3.zero;
    public bool IsJumping
    {
        get { return _IsJumping; }
    }
    
    private bool _IsJumping = false;
    private Collider2D _groundHit;
    public bool IsGrounded = false;

    private float _slopeAngle = 0f;
    
    public Vector2 InputDirection
    {
        get { return _inputDirection; }
        set { _inputDirection = value; }
    }
    
    protected Vector2 _inputDirection;
    protected Rigidbody2D _rigidbody;

    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        HandleInput();
    }

    protected void FixedUpdate()
    {
        CheckGround();
        CheckSlope();
        
        HandleMovement();
    }

    protected virtual void HandleInput()
    {
    }
    
    protected virtual void HandleMovement()
    {
        if (_rigidbody == null)
            return;
        
        Vector3 targetVelocity = new Vector2(_inputDirection.x * (Acceleration), _rigidbody.velocity.y);
        _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity,ref m_Velocity, m_MovementSmoothing);
             
    }

    protected void CheckGround()
    {
        // _groundHit = Physics2D.Raycast(transform.position, Vector2.down, 1f, GroundLayerMask);
        _groundHit = Physics2D.OverlapCircle(transform.position, 1f, GroundLayerMask);
        
        if (_groundHit)
        {
            IsGrounded = true;

            // _slopeAngle = Vector2.Angle(Vector2.up, _groundHit.normal);
        }
        else
            IsGrounded = false;

    }

    protected void CheckSlope()
    {
        if (_slopeAngle != 0)
        {
            _rigidbody.sharedMaterial = FullFriction;
            
        }
        else
        {
            _rigidbody.sharedMaterial = Default;
        }
    }
    
    protected virtual void DoJump()
    {
        if (!IsGrounded)
            return;

        Debug.Log("jumping");
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpForce);
    }

}
