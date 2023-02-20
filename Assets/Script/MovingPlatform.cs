using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float Speed = 5f;
    public Vector2 StartPosition = Vector2.zero;
    public Vector2 EndPosition = Vector2.zero;
    
    private bool _moving = false;
    private bool _pingPong = false;
    private Rigidbody2D _rigidbody;


    public float multiplier = 1f;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
        transform.position = StartPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        if (_moving)
            return;

        if (_pingPong)
        {
            StartCoroutine(MoveToPosition(StartPosition));
        }
        else
        {
            StartCoroutine(MoveToPosition(EndPosition));
        }
        

    }

    IEnumerator MoveToPosition(Vector3 pos)
    {
        _moving = true;
        Vector2 startPos = transform.position;
        Vector2 targetpos = pos;
        float elapsedTime = 0f;

        while (_rigidbody.position != targetpos)
        {
            // transform.position = Vector3.Lerp(startPos, targetpos, elapsedTime / Duration);
            
            _rigidbody.MovePosition(Vector2.Lerp(startPos, targetpos, (elapsedTime/Vector2.Distance(startPos, targetpos))*Speed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetpos;

        _moving = false;
        _pingPong = !_pingPong;
    }

}
