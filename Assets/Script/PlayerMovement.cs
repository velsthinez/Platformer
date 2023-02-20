using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    private bool _inputDown = false;
    private bool _canFallThrough = false;
    private RaycastHit2D fallthroughPlatformHit;
    private Collider2D targetPlatform;
    
    protected override void HandleInput()
    {
        InputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (InputDirection.y < 0)
            _inputDown = true;
        else
            _inputDown = false;
        
        if (Input.GetButtonDown("Jump"))
        {
            if (_inputDown && _canFallThrough)
            {
                FallthroughPlatform();
            }
            else
            {
                DoJump();
            }
            
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        CheckFallthroughPlatform();
    }

    private void FallthroughPlatform()
    {
        if(_canFallThrough )
            targetPlatform = fallthroughPlatformHit.collider;
        
        Physics2D.IgnoreCollision(_collider2D, targetPlatform, true);
        Invoke("ReactivateFallthroughCollission", 0.2f);
    }

    private void ReactivateFallthroughCollission()
    {
        Physics2D.IgnoreCollision(_collider2D, targetPlatform, false);

    }

    private void CheckFallthroughPlatform()
    {
        fallthroughPlatformHit = Physics2D.Raycast(GroundCheck.position, Vector2.down, 1f, GroundLayerMask);

        if (fallthroughPlatformHit == null)
        {
            _canFallThrough = false;
            return;
        }

        if (fallthroughPlatformHit.collider == null)
        {
            _canFallThrough = false;
            return;
        }
        
        _canFallThrough = fallthroughPlatformHit.collider.CompareTag("Fallthrough");
    }
}
