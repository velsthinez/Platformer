using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    protected override void HandleInput()
    {
        InputDirection = new Vector2(Input.GetAxis("Horizontal"), 0f);
        
        if(Input.GetButtonDown("Jump"))
            DoJump();;
    }
}
