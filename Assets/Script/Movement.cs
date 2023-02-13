using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Acceleration = 10f;
    public float JumpForce = 50f;
    public Transform GroundCheck;
    public float GoundCheckRadius = 1f;
    public float MaxSlopeAngle = 45f;
    
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
    private bool _canJump = true;
    
    private RaycastHit2D _slopeHit;
    private RaycastHit2D _slopeHitFront;
    private RaycastHit2D _slopeHitBack;
    public bool IsGrounded = false;
    public bool IsOnSlope = false;
    
    private float _slopeSideAngle = 0f;
    private float _slopeDownAngle = 0f;
    private float _lastSlopAngle = 0f;
    private bool _canWalkOnSlope = false;

    private Vector2 _slopeNormalPerpendicular = Vector2.zero;
    
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

        Vector3 targetVelocity = Vector3.zero;
        
        if (IsGrounded && !IsOnSlope && !IsJumping)
        {
            targetVelocity = new Vector2(_inputDirection.x * (Acceleration), 0f);
            _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity,ref m_Velocity, m_MovementSmoothing);

        }
        else if (IsGrounded && IsOnSlope && _canWalkOnSlope && !IsJumping)
        {
            targetVelocity = new Vector2(-_inputDirection.x * (Acceleration) * _slopeNormalPerpendicular.x,
                -_inputDirection.x * (Acceleration) * _slopeNormalPerpendicular.y);
            _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity,ref m_Velocity, m_MovementSmoothing);

        }
        else if (!IsGrounded)
        {
            targetVelocity = new Vector2(_inputDirection.x * (Acceleration), _rigidbody.velocity.y);
            _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity,ref m_Velocity, m_MovementSmoothing);

        }
        
             
    }

    protected void CheckGround()
    {

        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, GoundCheckRadius, GroundLayerMask);

        if (_rigidbody.velocity.y <= 0f)
        {
            _IsJumping = false;
        }

        if (IsGrounded && !IsJumping && _slopeDownAngle <= MaxSlopeAngle)
        {
            _canJump = true;
        }
    }

    protected void CheckSlope()
    {
        CheckSlopeHorizontal();
        CheckSlopeVertical();
    }
    
    protected void CheckSlopeHorizontal()
    {
        _slopeHitFront = Physics2D.Raycast(GroundCheck.position, Vector2.right, 1f, GroundLayerMask);
        _slopeHitBack = Physics2D.Raycast(GroundCheck.position, Vector2.left, 1f, GroundLayerMask);
        
        if (_slopeHitFront)
        {
            IsOnSlope = true;
            _slopeSideAngle = Vector2.Angle(Vector2.up, _slopeHitFront.normal);
        }
        else if (_slopeHitBack)
        {
            IsOnSlope = true;
            _slopeSideAngle = Vector2.Angle(Vector2.up, _slopeHitBack.normal);
        }
        else
        {
            _slopeSideAngle = 0f;
            IsOnSlope = false;
        }
        
    }

    protected void CheckSlopeVertical()
    {
        _slopeHit =  Physics2D.Raycast(GroundCheck.position, Vector2.down, 1f, GroundLayerMask);

        if (_slopeHit)
        {
            _slopeNormalPerpendicular = Vector2.Perpendicular(_slopeHit.normal).normalized;

            _slopeDownAngle = Vector2.Angle(_slopeHit.normal, Vector2.up);

            if (_slopeDownAngle != _lastSlopAngle)
            {
                IsOnSlope = true;
            }

            _lastSlopAngle = _slopeDownAngle;
            
            Debug.DrawRay(_slopeHit.point, _slopeNormalPerpendicular, Color.blue);
            Debug.DrawRay(_slopeHit.point, _slopeHit.normal, Color.green);
        }

        if (_slopeDownAngle > MaxSlopeAngle || _slopeSideAngle > MaxSlopeAngle)
        {
            _canWalkOnSlope = false;
        }
        else
        {
            _canWalkOnSlope = true;
        }

        if (IsOnSlope && _canWalkOnSlope && _inputDirection.x == 0)
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

        if (!_canJump)
            return;
        
        _canJump = false;
        _IsJumping = true;
        
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpForce);
    }

}
