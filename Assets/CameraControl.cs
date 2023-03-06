using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Vector2 PositionOffset = Vector2.zero;
    public Transform LeftBorder;
    public Transform RightBorder;
    public float LerpSpeed = 5f;

    protected float targetXPos = 0f;
    
    protected Vector2 _initialOffset = Vector2.zero;
    GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_player == null)
            return;

        float xPos = Mathf.Clamp(_player.transform.position.x, LeftBorder.position.x+8, RightBorder.position.x-8);
        targetXPos = Mathf.Lerp(targetXPos, xPos, Time.deltaTime * LerpSpeed);
        
        transform.position = new Vector3( targetXPos, 0, -10f);
    }
}
