using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void HitEvent(GameObject source);
    public HitEvent OnHit;
    
    public delegate void ResetEvent();
    public ResetEvent OnHitReset;
    
    public float CurrentHealth
    {
        get { return _currentHealth; }
    }
    
    public float MaxHealth = 10f;

    public Cooldown Invulnerable;
    
    
    private float _currentHealth = 10f;
    public bool _canDamage = true;

    private void Update()
    {
        ResetInvulnerable();
    }

    private void ResetInvulnerable()
    {
        if (_canDamage)
            return;

        if (Invulnerable.IsOnCooldown && _canDamage == false)
            return;

        _canDamage = true;
        OnHitReset?.Invoke();
        
    }


    public void Damage(float damage, GameObject source)
    {
        if (!_canDamage)
            return;
        
        _currentHealth -= damage;

        if (_currentHealth <= 0f)
        {
            _currentHealth = 0f;
            Die();
        }
        
        Invulnerable.StartCooldown();
        _canDamage = false;
        
        OnHit?.Invoke(source);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
