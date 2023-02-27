using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage = 10f;
    public float Speed = 10f;
    public float PushForce = 10f;
    public Cooldown Lifetime;
    public LayerMask TargetLayerMask;
    
    private Rigidbody2D _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
        Lifetime.StartCooldown();
        _rigidbody.AddRelativeForce(new Vector2(Speed,0f));

    }

    void Update()
    {
        if (Lifetime.CurrentProgress != Cooldown.Progress.Finished)
            return;

        Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (!((TargetLayerMask.value & (1 << col.gameObject.layer)) > 0))
                return;

        Rigidbody2D targetRigidbody = col.gameObject.GetComponent<Rigidbody2D>();

        if (targetRigidbody != null)
        {
            targetRigidbody.AddForce((col.transform.position - transform.position).normalized * PushForce);
        }

        Health targetHealth = col.gameObject.GetComponent<Health>();

        if (targetHealth != null)
        {
            targetHealth.Damage(Damage, gameObject);
        }
        
        Die();
    }
}
