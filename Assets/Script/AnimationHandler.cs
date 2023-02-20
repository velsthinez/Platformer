using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator _animator;
    private Movement _movement;
    private Vector3 _initialScale = Vector3.zero;
    private Vector3 _flipScale = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = transform.parent.GetComponent<Movement>();
        
        _initialScale = transform.localScale;
        _flipScale = new Vector3(-_initialScale.x, _initialScale.y, _initialScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        HandleFlip();
        UpdateAnimator();
    }

    private void HandleFlip()
    {
        if (_movement == null)
            return;

        if (_movement.FlipAnim)
            transform.localScale = _flipScale;
        else
        {
            transform.localScale = _initialScale;
        }
    }

    private void UpdateAnimator()
    {
        if (_animator == null || _movement == null)
            return;
        
        _animator.SetBool("IsJumping", _movement.IsJumping);
        _animator.SetBool("IsFalling", _movement.IsFalling);
        _animator.SetBool("IsRunning", _movement.IsRunning);
    }
}
